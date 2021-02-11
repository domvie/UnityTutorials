using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform playSpawnPoints;
    public bool reSpawn = false;

    private Transform[] spawnPoints;
    private bool lastToggle = false;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = playSpawnPoints.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lastToggle != reSpawn)
        {
            Respawn();
            reSpawn = false;
        } else
        {
            lastToggle = reSpawn;
        }
    }

    private void Respawn()
    {
        int i = Random.Range(1, spawnPoints.Length);
        transform.position = spawnPoints[i].transform.position;
    }
}
