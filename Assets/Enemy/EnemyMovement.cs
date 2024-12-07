using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    NavMeshAgent enemyAgent;

    private void Start() 
    {
        enemyAgent = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        EnemyMove();
    }

    void EnemyMove()
    {
        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            enemyAgent.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
        }
    }
    
}
