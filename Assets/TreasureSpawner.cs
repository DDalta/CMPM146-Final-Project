using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureSpawner : MonoBehaviour
{
    public GameObject Treasure;

    private float positionX;
    private float positionY;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 4; i++)
        {
            if (Random.Range(0f, 1f) > 0.8f)
            {
                positionX = transform.position.x;
                positionY = transform.position.y;
                GameObject newEnemy = Instantiate(Treasure, new Vector3(Random.Range(-3f + positionX, 3f + positionX), Random.Range(-3f + positionY, 3f + positionY), 0), Quaternion.identity);
                GlobalVariables.TotalTreasure += 1;
            }
        }
    }
        
}
