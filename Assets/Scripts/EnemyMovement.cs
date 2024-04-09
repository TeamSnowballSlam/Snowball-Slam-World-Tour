using System.Collections.Generic;
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


    void Start()
    {
        directions = new Vector3[8] {forward, back, right, left, fowardRight, fowardLeft, backRight, backLeft};

        agent = GetComponent<NavMeshAgent>();

         randomPosition = new Vector3(Random.Range(-4, 4), 1.26f, Random.Range(-4, 4));
        if (randomPosition != null)
        {
            GoToTarget(randomPosition);
        }
    }


// void ThrowSnowball(Vector3 direction)
// {
/*


*/
// }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
         randomPosition = new Vector3(Random.Range(-4, 4), 1.26f, Random.Range(-4, 4));
            GoToTarget(randomPosition);
        }
    }

    private void GoToTarget(Vector3 target)
    {
        agent.SetDestination(target);
    }
}
