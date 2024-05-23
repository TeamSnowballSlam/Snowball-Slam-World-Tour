using System.Collections;
using System.Collections.Generic;
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
        state = EnemyStates.GettingNewLocation; //Sets the state
        Debug.Log("METHOD Getting new location");
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

        Debug.Log("Random " + randomPosition + "Current Position: " + transform.position);


    }

    void Update()
    {

        Debug.Log("Remaining distance: " + agent.remainingDistance);
        Debug.Log("Remaining distance less than: " + ((agent.remainingDistance <= 0.001) && state == EnemyStates.Moving));
        Debug.Log("State: " + state);
        animator.SetFloat("movementSpeed", agent.velocity.magnitude);
        Debug.Log("Target Position: " + agent.destination);
        Debug.Log("Current Position: " + agent.velocity.magnitude);

        if (GameSettings.currentGameState != GameStates.InGame)
        {
            agent.isStopped = true;
            return;
        }
        else if (GameSettings.currentGameState == GameStates.InGame)
        {
            agent.isStopped = false;
        }

        if (state == EnemyStates.Idle)
        {
            if (CheckForPlayerDirection() != Vector3.zero)
            {
                state = EnemyStates.TargetingPlayer;
            }
            else
            {

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
                    }
                    else
                    {
                        GetNewLocation(); //Gets a new location
                        return;
                    }
                }
                Debug.Log("Reached target should go to idle");
                state = EnemyStates.Idle; //Sets the state to Idle
                return;
            }
            else if (CheckForPlayerDirection() != Vector3.zero)
            {

                if (Physics.Raycast(transform.position, CheckForPlayerDirection(), out RaycastHit hit, 10f))
                {
                    if (hit.collider.CompareTag("Player"))
                    {

                        state = EnemyStates.TargetingPlayer;
                    }
                }
            }
            if (agent.velocity.magnitude == 0.0f)
            {
                Debug.Log("Agent velocity is 0");
                state = EnemyStates.Idle;
            }

        }
        else if (state == EnemyStates.TargetingPlayer)
        {
            agent.isStopped = true; //Stops the agent



            transform.forward = (
                new Vector3(
                    GetClosestPlayer().transform.position.x,
                    0,
                    GetClosestPlayer().transform.position.z
                ) - new Vector3(transform.position.x, 0, transform.position.z)
            ).normalized; //Sets the forward direction of the agent to the direction to the player
            if (Time.time > throwTime + delayTime)
            {
                agent.isStopped = true; //Stops the agent
                Debug.Log(agent.velocity.magnitude);
                agent.velocity = Vector3.zero; //Sets the velocity of the agent to zero
                GetComponent<ThrowSnowballs>().ThrowSnowball(); //Throws a snowball
                throwTime = Time.time; //Sets the throw time to the current time
            }
        }
        else if (state == EnemyStates.ThrowingSnowball)
        {
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
        state = EnemyStates.Moving;
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
        Debug.Log("METHOD Rotating to target");
    }

    private void GoToTarget(Vector3 target) //Sets the destination of the agent to the target position
    {
        Debug.Log("METHOD Going to target");
        RotateToTarget((target - transform.position).normalized); //Rotates the agent to face the target
        agent.SetDestination(target); //Starts moving the agent to the target position
        state = EnemyStates.Moving; //Sets the state to moving
    }
}
