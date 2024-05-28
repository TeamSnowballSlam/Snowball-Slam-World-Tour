using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal.Internal;

public enum EnemyStates //The states of the enemy
{
    Idle,
    Moving,
    Rotating,
    Searching,
    TargetingPlayer,
    ThrowingSnowball,
    GettingNewLocation
}

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent; //The NavMeshAgent component
    public NavMeshSurface surface; //The NavMeshSurface component
    private Vector3 randomPosition; //The random position to move to

    [SerializeField]
    private EnemyStates state = EnemyStates.Idle; //The state of the enemy
    private float throwTime; //The time to throw the snowball
    private float delayTime = 1.5f; //The delay time between throws

    private int turretChance;
    private Animator animator;

    void Start()
    {
        turretChance = GetComponent<KangarooAbility>().turretSpawnChance;
        throwTime = Time.time; //Sets the throw time to the current time
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        animator = GetComponent<Animator>();
    }

    private void GetNewLocation()
    {
        if (state == EnemyStates.GettingNewLocation)
            return; //If the state is getting new location, return
        if (state == EnemyStates.TargetingPlayer)
            return; //If the state is targeting player, return
        if (state == EnemyStates.ThrowingSnowball)
            return; //If the state is throwing snowball, return
        state = EnemyStates.GettingNewLocation; //Sets the state
        state = EnemyStates.Idle; //Sets the state to moving
        int randomMultiplier = Random.Range(1, 15); //Randomizes the multiplier
        // Vector3 randomDir = Directions.directions[Random.Range(0, 8)]; //Randomizes the direction
        Bounds bounds = surface.navMeshData.sourceBounds; //Gets the bounds of the navmesh
        do
        {
            randomPosition = new Vector3( //Calculates the random position
                Random.Range((bounds.min.x + 2f), (bounds.max.x - 2f)),
                transform.position.y,
                Random.Range((bounds.min.z + 2f), (bounds.max.z - 2f))
            );
        } while ( //Checks if the random position is within the bounds
            randomPosition.x < (bounds.min.x + 2f)
            || randomPosition.x > (bounds.max.x - 2f)
            || randomPosition.z < (bounds.min.z + 2f)
            || randomPosition.z > (bounds.max.z - 2f)
        );

        GoToTarget(randomPosition); //Moves the agent to the random position

    }

    void Update()
    {

        animator.SetFloat("movementSpeed", agent.velocity.magnitude);

        if (GameSettings.currentGameState != GameStates.InGame)
        {
            agent.isStopped = true;
            return;
        }
        else if (GameSettings.currentGameState == GameStates.InGame && state == EnemyStates.Moving)
        {
            agent.isStopped = false;
        }

        if (state == EnemyStates.Idle)
        {
            if (
                Physics.Raycast(
                    transform.position,
                    (
                        new Vector3(
                            GetClosestPlayer().transform.position.x,
                            0,
                            GetClosestPlayer().transform.position.z
                        )
                    ),
                    out RaycastHit hit,
                    10f
                ) && hit.collider.CompareTag("Player")
            )
            {
                state = EnemyStates.TargetingPlayer;
            }
            else
            {
                GetNewLocation();
            }
        }
        else if (state == EnemyStates.Moving)
        {
            if (agent.remainingDistance <= 0.001) //Checks if the agent has reached the target within a certain distance
            {
                if (GetComponent<KangarooAbility>().canUseTurret) //Checks if the agent has the KangarooAbility component and does not have an active turret
                {
                    int random = Random.Range(1, 101); //Randomizes the number between 1 and the 100
                    if (random <= turretChance)
                    {
                        GetComponent<KangarooAbility>().PlaceTurret();
                        GetNewLocation();
                        return;
                    }
                    else
                    {
                        GetNewLocation(); //Gets a new location
                        return;
                    }
                }
                GetNewLocation(); //Gets a new location
                return;
            }
            else if (
                Physics.Raycast(
                    transform.position,
                    (
                        new Vector3(
                            GetClosestPlayer().transform.position.x,
                            0,
                            GetClosestPlayer().transform.position.z
                        ) - new Vector3(transform.position.x, 0, transform.position.z)
                    ).normalized,
                    out RaycastHit hit,
                    10f
                )
            )
            {
                Vector3 dir = (
                    new Vector3(
                        GetClosestPlayer().transform.position.x,
                        0,
                        GetClosestPlayer().transform.position.z
                    ) - new Vector3(transform.position.x, 0, transform.position.z)
                ).normalized;

                if (hit.collider.CompareTag("Player"))
                {
                    state = EnemyStates.TargetingPlayer;
                    Debug.DrawRay(transform.position, dir, Color.green);
                }
                else
                {
                    state = EnemyStates.Moving;
                    Debug.DrawRay(transform.position, dir, Color.red);
                }
            }
            //}
            if (agent.velocity.magnitude == 0.0f)
            {
                state = EnemyStates.Idle;
            }
        }
        else if (state == EnemyStates.TargetingPlayer)
        {
            agent.ResetPath(); //Resets the path of the agent
            agent.SetDestination(agent.transform.position); //Sets the destination of the agent to the current position
            agent.velocity = Vector3.zero; //Sets the velocity of the agent to zero

            transform.forward = (
                new Vector3(
                    GetClosestPlayer().transform.position.x,
                    0,
                    GetClosestPlayer().transform.position.z
                ) - new Vector3(transform.position.x, 0, transform.position.z)
            ).normalized; //Sets the forward direction of the agent to the direction to the player

            if (Time.time > throwTime + delayTime)
            {
                for (int i = 0; i < 1; i++)
                {
                    agent.isStopped = true; //Stops the agent
                    agent.velocity = Vector3.zero; //Sets the velocity of the agent to zero
                    GetComponent<ThrowSnowballs>().ThrowSnowball(); //Throws a snowball
                    throwTime = Time.time; //Sets the throw time to the current time
                }
                if (
                    Physics.Raycast(
                        transform.position,
                        (GetClosestPlayer().transform.position - transform.position),
                        out RaycastHit hit,
                        10f
                    )
                )
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        // state = EnemyStates.TargetingPlayer;
                        Debug.DrawRay(
                            transform.position,
                            (GetClosestPlayer().transform.position - transform.position),
                            Color.blue
                        );
                    }
                    else
                    {
                        // state = EnemyStates.Idle;
                        // GetNewLocation();
                        Debug.DrawRay(
                            transform.position,
                            (GetClosestPlayer().transform.position - transform.position),
                            Color.red
                        );
                        state = EnemyStates.Idle;
                        return;
                    }
                }
                else 
                {
                    state = EnemyStates.Idle;
                    return;
                }
            }
        }
        else if (state == EnemyStates.ThrowingSnowball)
        {
            agent.isStopped = true; //Stops the agent
            agent.velocity = Vector3.zero; //Sets the velocity of the agent to zero

            GetComponent<ThrowSnowballs>().ThrowSnowball(); //Throws a snowball
        }
    }

    private GameObject GetClosestPlayer()
    {
        // state = EnemyStates.Searching; //Sets the state to searching
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player"); //Gets all the players in the scene
        GameObject closestPlayer = null; //Initializes the closest player to null
        float closestDistance = Mathf.Infinity; //Initializes the closest distance to infinity
        foreach (GameObject player in players) //Loops through all the players
        {
            float distance = Vector3.Distance(transform.position, player.transform.position); //Gets the distance between the agent and the player
            if (distance < closestDistance) //Checks if the distance is less than the closest distance
            {
                closestDistance = distance; //Sets the closest distance to the distance
                closestPlayer = player; //Sets the closest player to the player
            }
        }
        return closestPlayer; //Returns the closest player
    }

    private Vector3 CheckForPlayerDirection() //WIP METHOD NOT CURRENTLY WORKING
    {
        if (GetClosestPlayer() == null)
            return Vector3.zero; //If there is no player, return zero vector
        Transform player = GetClosestPlayer().transform; //Gets the closest player

        Vector3 direction = player.position - agent.transform.position;
        direction = direction.normalized;

        direction.y = 0;

        // Check if player is aligned with any of the predefined directions
        foreach (Vector3 dir in Directions.directions)
        {
            if (Vector3.Dot(direction.normalized, dir.normalized) > 0.99f) //Check if the player is aligned with the predefined direction within a certain threshold 0.99
            {
                return dir;
            }
        }

        return Vector3.zero;
    }

    private void RotateToTarget(Vector3 targetDirection) //Rotates the agent to face the target given the target direction and speed
    {
        state = EnemyStates.Rotating; //Sets the state to rotating
        transform.forward = targetDirection; //Sets the forward direction of the agent to the target direction
    }

    private void GoToTarget(Vector3 target) //Sets the destination of the agent to the target position
    {
        RotateToTarget((target - transform.position).normalized); //Rotates the agent to face the target
        agent.SetDestination(target); //Starts moving the agent to the target position
        state = EnemyStates.Moving; //Sets the state to moving
    }
}
