using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int speed;

    private GameObject target;
    private NavMeshAgent agent;
    private float distanceFromAgent;
    private Vector3 origin;
    private float distanceFromOrigin;
    private Vector3 targetPosition;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        origin = transform.position;
        targetPosition = origin;
    }

    // Update is called once per frame
    void Update()
    {

        distanceFromAgent = Vector3.Distance(transform.position, target.transform.position);
        distanceFromOrigin = Vector3.Distance(transform.position, origin);


        if (distanceFromAgent < 2.55f)
        {
            if (distanceFromOrigin < 5f)
            {
                targetPosition = target.transform.position;
            }
        } else
        {
            targetPosition = origin;
        }

        if (Vector3.Distance(transform.position, targetPosition) > 1f)
        {
            agent.SetDestination(targetPosition);
        } else
        {
            agent.ResetPath();
        }
    }
}
