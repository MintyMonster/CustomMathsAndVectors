using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSpawning : MonoBehaviour
{

    public GameObject tank;
    public GameObject spawnPoint;
    private MathsCustomLibrary.Vector3 spawnPointPos;
    private float spawnTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        spawnPointPos = spawnPoint.transform.position;
        SpawnTank();
        InvokeRepeating("SpawnTank", spawnTime, spawnTime);
    }

    void SpawnTank()
    {
        var newTank = Instantiate(tank, spawnPointPos, tank.transform.rotation);
    }
}
