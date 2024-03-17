using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private bool[] _spawners;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void setSpawners(bool[] array)
    {
        _spawners = array;
    }

}
