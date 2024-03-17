using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int speed;

    private GameObject target;
    private NavMeshAgent agent;
    private float distance;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, target.transform.position);
        Vector2 direction = target.transform.position - transform.position;

        if (distance < 4)
        {
            //transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            agent.SetDestination(target.transform.position);
        }
    }
}
