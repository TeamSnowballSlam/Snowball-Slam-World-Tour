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
    ThrowingSnowball
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
                int randomMultiplier = Random.Range(1, 15); //Randomizes the multiplier
               // Vector3 randomDir = Directions.directions[Random.Range(0, 8)]; //Randomizes the direction
                Bounds bounds = surface.navMeshData.sourceBounds; //Gets the bounds of the navmesh
               randomPosition = new Vector3( //Calculates the random position
                    Random.Range(bounds.min.x, bounds.max.x),
                    transform.position.y,
                    Random.Range(bounds.min.z, bounds.max.z)
                );
                if ( //Checks if the random position is within the bounds
                    randomPosition.x > bounds.min.x
                    && randomPosition.x < bounds.max.x
                    && randomPosition.z > bounds.min.z
                    && randomPosition.z < bounds.max.z
                    && !Physics.CheckBox(randomPosition, new Vector3(0.5f, 0.5f, 0.5f))
                )
                {
                    GoToTarget(randomPosition); //Moves the agent to the random position
                    Debug.Log("Random position in bounds");
                    Debug.Log("Bounds min: " + bounds.min);
                    Debug.Log("Bounds Max: " + bounds.max);
                    Debug.Log("Random " + randomPosition);

                }
                else
                {
                    Debug.Log("Random position out of bounds");
                    Debug.Log(bounds.min);
                    Debug.Log(bounds.max);
                    Debug.Log(randomPosition);
                }
    }
    void Update()
    {
        Debug.Log("Remaining distance: " + agent.remainingDistance);
        Debug.Log("Remaining distance less than: " + ((agent.remainingDistance <= 0.001) && state == EnemyStates.Moving) );
        Debug.Log("State: " + state);
        animator.SetFloat("movementSpeed", agent.velocity.magnitude);
        
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
                GetNewLocation();




            }
        }
         if (state == EnemyStates.Moving)
        {
            if (agent.remainingDistance <= 0.001) //Checks if the agent has reached the target within a certain distance
            {
                if (GetComponent<KangarooAbility>().canUseTurret) //Checks if the agent has the KangarooAbility component and does not have an active turret
                {
                    int random = Random.Range(1, 101); //Randomizes the number between 1 and the 100
                    if ( random <= turretChance)
                    {
                        GetComponent<KangarooAbility>().PlaceTurret();
                    }
                    else
                    {
                        return;
                    }
                }
                    Debug.Log("Reached target should go to idle");
                    state = EnemyStates.Idle; //Sets the state to Idle
                    GetNewLocation(); //Gets a new location to move to
                    return; 
            }
            else if (CheckForPlayerDirection() != Vector3.zero)
            {

                if(Physics.Raycast(transform.position, CheckForPlayerDirection(), out RaycastHit hit, 10f))
                {
                    if(hit.collider.CompareTag("Player"))
                    {

                        state = EnemyStates.TargetingPlayer;
                    }
                }
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
    }

    private void GoToTarget(Vector3 target) //Sets the destination of the agent to the target position
    {
        RotateToTarget((target - transform.position).normalized); //Rotates the agent to face the target
        agent.SetDestination(target); //Starts moving the agent to the target position
        state = EnemyStates.Moving; //Sets the state to moving
    }
}
