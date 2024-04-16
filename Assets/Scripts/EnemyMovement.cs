using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

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
    public GameObject sphere; //The sphere prefab

    [SerializeField]
    private EnemyStates state = EnemyStates.Idle; //The state of the enemy

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
    }

    // void ThrowSnowball(Vector3 direction)
    // {
    /*
    
    
    */
    // }
    void Update()
    {
        // if (CheckForPlayerDirection() != Vector3.zero)
        // {
        //     print("DOING THIS SECTION 1");
        //     Debug.DrawLine(
        //         transform.position,
        //         GetClosestPlayer().transform.position,
        //         Color.blue,
        //         1f
        //     ); //Draws a line to the player
        //     transform.forward = (
        //         new Vector3(
        //             GetClosestPlayer().transform.position.x,
        //             0,
        //             GetClosestPlayer().transform.position.z
        //         ) - new Vector3(transform.position.x, 0, transform.position.z)
        //     ).normalized; //Sets the forward direction of the agent to the direction to the player
        //     state = EnemyStates.TargetingPlayer;
        // }


        // else
        {
            if (state == EnemyStates.Idle)
            {
                if (CheckForPlayerDirection() != Vector3.zero)
                {
                    state = EnemyStates.TargetingPlayer;
                }
                else
                {
                    // int randomMultiplier = Random.Range(1, 5); //Randomizes the multiplier
                    // Vector3 randomDir = Directions.directions[Random.Range(0, 8)]; //Randomizes the direction
                    // randomPosition = transform.position + (randomMultiplier * randomDir); //Calculates the random position
                    // Debug.Log("Random position: " + randomPosition);
                    // Debug.Log("Direction: " + randomDir);
                    // Debug.Log("Multiplier: " + randomMultiplier);
                    // Debug.Log("Direction * Multiplier: " + (randomMultiplier * randomDir));
                    // Bounds bounds = surface.navMeshData.sourceBounds; //Gets the bounds of the navmesh
                    // if ( //Checks if the random position is within the bounds
                    //     randomPosition.x > bounds.min.x
                    //     && randomPosition.x < bounds.max.x
                    //     && randomPosition.z > bounds.min.z
                    //     && randomPosition.z < bounds.max.z
                    // )
                    // {
                    //     GoToTarget(randomPosition); //Moves the agent to the random position
                    // }
                }
            }
            else if (state == EnemyStates.Moving)
            {
                Debug.Log("Agent remaining distance: " + agent.remainingDistance);
                if (
                    agent.remainingDistance <= 0.001f
                    
                ) //Checks if the agent has reached the target within a certain distance
                {
                    state = EnemyStates.Idle; //Sets the state to idle
                }
                else if (CheckForPlayerDirection() != Vector3.zero)
                {
                    state = EnemyStates.TargetingPlayer;
                }
            }
            else if (state == EnemyStates.TargetingPlayer)
            {
                Debug.Log("Targeting player");
                Debug.DrawLine(
                    transform.position,
                    GetClosestPlayer().transform.position,
                    Color.blue,
                    1f
                ); //Draws a line to the player
                transform.forward = (
                    new Vector3(
                        GetClosestPlayer().transform.position.x,
                        0,
                        GetClosestPlayer().transform.position.z
                    ) - new Vector3(transform.position.x, 0, transform.position.z)
                ).normalized; //Sets the forward direction of the agent to the direction to the player
            }
        }
    }

    private GameObject GetClosestPlayer()
    {
        state = EnemyStates.Searching; //Sets the state to searching
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
        state = EnemyStates.Moving; //Sets the state to idle
        return closestPlayer; //Returns the closest player
    }

    private Vector3 CheckForPlayerDirection() //WIP METHOD NOT CURRENTLY WORKING
    {
        Transform player = GetClosestPlayer().transform; //Gets the closest player


        //foreach (Vector3 pb in playerBuffer) //Loops through all the positions surrounding the player
        // {

       Vector3 direction = (player.position - agent.transform.position);
       Debug.Log("Direction: " + direction);
direction = direction.normalized;
        Debug.Log("Direction: " + direction);

       direction.y = 0;

        // Check if player is aligned with any of the predefined directions
        foreach (Vector3 dir in Directions.directions)
        {
           if (Vector3.Dot(direction.normalized, dir.normalized) > 0.99f)
            {
                return dir;
            }

        }

        // If player is not aligned with any predefined direction
        Debug.Log("Player is not aligned with any predefined direction");
        return Vector3.zero;
    //}

        // Determine the direction

        // if (
        //     Directions.directions.Contains(
        //         Quaternion.LookRotation(pb - transform.position).eulerAngles.normalized //Checks if the player is in 1 of the 8 directions
        //     )
        // )
        // {
        //     Debug.Log("Player can be targeted");
        //     state = EnemyStates.TargetingPlayer; //Sets the state to targeting player

        //     //  Debug.DrawLine(transform.position, player.position, Color.red, 5f); //Draws a line to the player

        //     return Quaternion
        //         .LookRotation(player.position - transform.position)
        //         .eulerAngles.normalized; //Returns the direction to the player with a scale of 1
        // }
        // }
        
        // {
        //     Debug.Log("Player is not near");
        //     return Vector3.zero; //Returns a zero vector
        // }
    }

    
    

    private void RotateToTarget(Vector3 targetDirection) //Rotates the agent to face the target given the target direction and speed
    {
        Debug.Log("Rotating");
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
