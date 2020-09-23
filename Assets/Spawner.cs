﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject dechet;
    [SerializeField] float radius;

    [SerializeField] int quantity = 10;

    void Start()
    {

        for (int i = 0; i < quantity; i++)
        {
            Vector3 pos = new Vector3(Random.Range(0, radius), 0, Random.Range(0, radius));

            pos += transform.position;
            Instantiate(dechet, pos, Quaternion.identity);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(transform.position, new Vector3(radius,0, radius) * 2);
    }
}