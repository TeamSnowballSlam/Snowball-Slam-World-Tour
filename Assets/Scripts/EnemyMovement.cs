using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 randomPosition;
    private Vector3 fowardRight = new Vector3(1, 0, 1);
    private Vector3 fowardLeft = new Vector3(-1, 0, 1);
    private Vector3 backRight = new Vector3(1, 0, -1);
    private Vector3 backLeft = new Vector3(-1, 0, -1);
    private Vector3 forward = new Vector3(0, 0, 1);
    private Vector3 back = new Vector3(0, 0, -1);
    private Vector3 right = new Vector3(1, 0, 0);
    private Vector3 left = new Vector3(-1, 0, 0);
    private Vector3[] directions;
    public bool isMoving = false;
    public bool isRotating = false;

    void Start()
    {
        directions = new Vector3[8]
        {
            forward,
            back,
            right,
            left,
            fowardRight,
            fowardLeft,
            backRight,
            backLeft
        };
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space pressed");

            if (!isMoving)
            {
                 randomPosition =
                    new(Random.Range(-4, 4), gameObject.transform.position.y, Random.Range(-4, 4));
                // RotateToTarget((transform.position -randomPosition).normalized, 50f);
                GoToTarget(randomPosition);
            }
            else
            Debug.Log("Already moving");
        }
        if (Vector3.Distance(transform.position, randomPosition) < 0.01f)
            isMoving = false;
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
        transform.rotation = Quaternion.LookRotation(newDirection);
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
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
