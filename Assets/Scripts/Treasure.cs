using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            AgentSensor agentSensor = collision.GetComponent<AgentSensor>();

            agentSensor.Objects.Clear();

            GlobalVariables.CurrentTreasure += 1;
            Destroy(gameObject);
        }
    }
}
