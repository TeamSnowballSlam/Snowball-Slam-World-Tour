using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private List<GameObject> targets;
    private NavMeshAgent agent;
    public Transform currentTarget;

    void Start()
    {
        targets = new List<GameObject>();
        targets.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        agent = GetComponent<NavMeshAgent>();

        currentTarget = targets[Random.Range(0, targets.Count)].transform;
        if (currentTarget != null)
        {
            GoToTarget(currentTarget);
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
            currentTarget = targets[Random.Range(0, targets.Count)].transform;
            GoToTarget(currentTarget);
        }
    }

    private void GoToTarget(Transform target)
    {
        agent.SetDestination(target.position - new Vector3(0, 0, 5f));
        
    }
}
