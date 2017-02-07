using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnOnTimer : MonoBehaviour
{
    private script_spawner spawner;

    private void Start()
    {
        spawner = GetComponent<script_spawner>();
    }

    float timer = 0.0f;
    private void Update()
    {
        timer += Time.deltaTime;

        if (spawner && timer > 2.0f)
        {
            spawner.Spawn(10);
            timer = 0.0f;
        }
    }
}
