using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform != transform.parent && (collision.CompareTag("SpawnPoint") || collision.CompareTag("Square")))
        {
            Destroy(collision.gameObject);
        }
    }
}
