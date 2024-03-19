using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    private NavMeshAgent agent;

    public float speed;
    private Vector3 targetPos = new Vector3(0, 0, 0);

    private void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = speed;
    }

    private void Update()
    {
        Vector3 rotation = (agent.steeringTarget - transform.position).normalized;

        float rot_z = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rot_z);
    }

    /*
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.x = mouse_position.x;
            targetPos.y = mouse_position.y;
            agent.SetDestination(targetPos);
        } 

        Vector3 rotation = (agent.steeringTarget - transform.position).normalized;

        float rot_z = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rot_z);


    }
    */
}
