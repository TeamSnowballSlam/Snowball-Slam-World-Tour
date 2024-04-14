using System.Collections;
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
        //  if (Input.GetKeyDown(KeyCode.Space))
        //  {
        if (GameObject.Find("TARGET") != null) 
            Destroy(GameObject.Find("TARGET"));
        if (state == EnemyStates.Idle)
        {
            int randomMultiplier = Random.Range(1, 5); //Randomizes the multiplier
            Vector3 randomDir = Directions.directions[Random.Range(0, 8)]; //Randomizes the direction
            randomPosition = transform.position + (randomMultiplier * randomDir); //Calculates the random position
            Debug.Log("Random position: " + randomPosition);
            Debug.Log("Direction: " + randomDir);
            Debug.Log("Multiplier: " + randomMultiplier);
            Debug.Log("Direction * Multiplier: " + (randomMultiplier * randomDir));
            Bounds bounds = surface.navMeshData.sourceBounds; //Gets the bounds of the navmesh
            if ( //Checks if the random position is within the bounds
                randomPosition.x > bounds.min.x
                && randomPosition.x < bounds.max.x
                && randomPosition.z > bounds.min.z
                && randomPosition.z < bounds.max.z
            )
            {
                GoToTarget(randomPosition); //Moves the agent to the random position
                GameObject go = Instantiate(sphere, randomPosition, Quaternion.identity); //Instantiates a sphere at the random position
                go.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f); //Sets the scale of the sphere
                go.name = "TARGET"; //Sets the name of the sphere
            }
            else
            {
                Debug.LogError("Out of bounds"); //Logs that the random position is out of bounds
            } 
        }
        else
            Debug.Log("Already moving"); //Logs that the agent is already moving
            Debug.Log("Agent remaining distance: " + agent.remainingDistance);
        // }
        if (agent.remainingDistance <= 0.001f) //Checks if the agent has reached the target within a certain distance
        {
            state = EnemyStates.Idle; //Sets the state to idle
            //CheckForPlayerDirection(); //Checks if the player is in the 8 directions
            // if (state == EnemyStates.TargetingPlayer) 
            // {
            //     Debug.DrawLine(transform.position, GetClosestPlayer().transform.position, Color.red); //Draws a line to the player
            // }
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
        return closestPlayer; //Returns the closest player
    }

    private Vector3 CheckForPlayerDirection() //WIP METHOD NOT CURRENTLY WORKING
    {
        Transform player = GetClosestPlayer().transform; //Gets the closest player
        if (
            Directions.directions.Contains(
                Quaternion.LookRotation(player.position - transform.position).eulerAngles.normalized //Checks if the player is in 1 of the 8 directions
            )
        )
        {
            Debug.Log("Player can be targeted");
            state = EnemyStates.TargetingPlayer; //Sets the state to targeting player 
            return Quaternion.LookRotation(player.position - transform.position).eulerAngles.normalized; //Returns the direction to the player with a scale of 1
        }
        else
        {
            Debug.Log("Player is not near");
            state = EnemyStates.Idle; //Sets the state to idle
            return Vector3.zero; //Returns a zero vector
        }
    }

    private void RotateToTarget(Vector3 targetDirection) //Rotates the agent to face the target given the target direction and speed
    {
        Debug.Log("Rotating");
        state = EnemyStates.Rotating; //Sets the state to rotating
        transform.forward = targetDirection; //Sets the forward direction of the agent to the target direction
        // Vector3 newDirection = Vector3.RotateTowards( //Gets the direction to rotate to
        //     transform.forward,
        //     targetDirection,
        //     speed,
        //     0.0f
        // );
        // while (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(newDirection)) > 0.01) //Rotates the agent to face the target while the angle is greater than 0.01
        // {
        //     transform.rotation = Quaternion.RotateTowards( //Rotates the agent to face the target
        //         transform.rotation,
        //         Quaternion.LookRotation(newDirection),
        //         10f
        //     );
        //     transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0); //Locks the rotation on the x and z axis
        // }
        // if (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(newDirection)) < 0.01)
        // {
        //     state = EnemyStates.Idle; //Sets the state to idle
        //     yield return null;
        // }
    }

    private void GoToTarget(Vector3 target) //Sets the destination of the agent to the target position
    {
        RotateToTarget((target - transform.position).normalized); //Rotates the agent to face the target
        agent.SetDestination(target); //Starts moving the agent to the target position
        state = EnemyStates.Moving; //Sets the state to moving
    }
}
