using System.Collections;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    public NavMeshSurface surface;
    private Vector3 randomPosition;

    public bool isMoving = false;
    public bool isRotating = false;
    public GameObject sphere;

    void Start()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;

        agent = GetComponent<NavMeshAgent>();
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
        if (!isMoving)
        {
            int randomMultiplier = Random.Range(1, 5);
            Vector3 randomDir = Directions.directions[Random.Range(0, 8)];
            randomPosition = transform.position + (randomMultiplier * randomDir);
            Debug.Log("Random position: " + randomPosition);
            Debug.Log("Direction: " + randomDir);
            Debug.Log("Multiplier: " + randomMultiplier);
            Debug.Log("Direction * Multiplier: " + (randomMultiplier * randomDir));
            Bounds bounds = surface.navMeshData.sourceBounds;
            if (
                randomPosition.x > bounds.min.x
                && randomPosition.x < bounds.max.x
                && randomPosition.z > bounds.min.z
                && randomPosition.z < bounds.max.z
            )
            {
                GoToTarget(randomPosition);
                GameObject go = Instantiate(sphere, randomPosition, Quaternion.identity);
                go.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
                go.name = "TARGET";
            }
            else
            {
                Debug.Log("Out of bounds");
            }
        }
        else
            Debug.Log("Already moving");
        // }
        if (agent.remainingDistance == 0.01f)
        {
            isMoving = false;
        }

    }

    private GameObject GetClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject closestPlayer = null;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPlayer = player;
            }
        }
        return closestPlayer;
    }

    private Vector3 CheckForPlayerDirection()
    {
        Transform player = GetClosestPlayer().transform;
        if (
            Directions.directions.Contains(
                Quaternion.LookRotation(player.position - transform.position).eulerAngles.normalized
            )
        )
        {
            Debug.Log("Player can be targeted");
            return Quaternion.LookRotation(player.position - transform.position).eulerAngles.normalized;
        }
        else
        {
            Debug.Log("Player is not near");
            return Vector3.zero;
        }
    }

    private IEnumerator RotateToTarget(Vector3 targetDirection, float speed)
    {
        Debug.Log("Rotating");
        isRotating = true;

        Vector3 newDirection = Vector3.RotateTowards(
            transform.forward,
            targetDirection,
            speed,
            0.0f
        );
        // transform.rotation = Quaternion.LookRotation(newDirection);
        while (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(newDirection)) > 0.01)
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.LookRotation(newDirection),
                10f
            );
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
        }
        if (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(newDirection)) < 0.01)
        {
            isRotating = false;
            yield return null;
        }
    }

    private void GoToTarget(Vector3 target)
    {
        StartCoroutine(RotateToTarget((target - transform.position).normalized, 50f));
        agent.SetDestination(target);
        isMoving = true;
    }
}
