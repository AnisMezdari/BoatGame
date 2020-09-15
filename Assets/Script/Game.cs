﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] asteroid;

    public GameObject cloud;

    void Start()
    {
        Camera.main.aspect = 2960f / 1440f;

        InvokeRepeating("SpawnAsteroid", 1f, 5f);
        InvokeRepeating("SpawnCloud", 0f, 2f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnAsteroid()
    {
        GameObject newAsteroid = Instantiate(asteroid[Random.Range(0, 6)]);
        float y_rand = Random.Range(-6.5f, 3.65f);
        newAsteroid.transform.Translate(new Vector3(10, y_rand));
    }

    void SpawnCloud()
    {
        GameObject newCloud = Instantiate(cloud);
        float y_rand = Random.Range(-6.5f, 3.65f);
        newCloud.transform.Translate(new Vector3(10, y_rand));
    }
}