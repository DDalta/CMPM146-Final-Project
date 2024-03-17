using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;

    private float positionX;
    private float positionY;

    // Start is called before the first frame update
    void Start()
    {
        if (Random.Range(0f, 1f) > 0.7f)
        {
            positionX = transform.position.x;
            positionY = transform.position.y;
            GameObject newEnemy = Instantiate(Enemy, new Vector3(Random.Range(-3f + positionX, 3f + positionX), Random.Range(-3f + positionY, 3f + positionY), 0), Quaternion.identity);
        }
    }
}
