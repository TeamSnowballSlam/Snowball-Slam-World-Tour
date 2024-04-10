using System.Collections.Generic;
using System.Linq;
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
        agent.SetDestination(player.position);
    }

    // void ThrowSnowball(Vector3 direction)
    // {
    /*
    
    
    */
    // }
    void Update()
    {
        if (!isMoving)
        {
            print(agent.remainingDistance);

            float singleStep = 1f * Time.deltaTime;

            Vector3 randomPosition = new (
                Random.Range(-4, 4),
                gameObject.transform.position.y,
                Random.Range(-4, 4)
            );
            Vector3 targetDirection = (gameObject.transform.position - randomPosition).normalized;
            Vector3 newDirection = Vector3.RotateTowards(
                transform.forward,
                targetDirection,
                singleStep,
                0.0f
            );

            transform.rotation = Quaternion.LookRotation(newDirection);
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);

            GoToTarget(randomPosition);
        }
        else if (agent.remainingDistance < 1f)
        {
            isMoving = false;
        }
    }

    private void GoToTarget(Vector3 target)
    {
        agent.SetDestination(target);
        isMoving = true;
    }
}
