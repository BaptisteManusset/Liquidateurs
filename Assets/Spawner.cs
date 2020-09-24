﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject dechet;
    [SerializeField] float radius;

    [SerializeField] int quantity = 10;

    [SerializeField] float delay = 1;

    void Start()
    {
        SpawnStack();

        InvokeRepeating(nameof(Spawn), 1, delay);
    }
    [ContextMenu("Spawn Stack")]
    private void SpawnStack()
    {
        for (int i = 0; i < quantity; i++)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        if (GameManager.ElementsToCollect.Count >= 20) return;

        Vector3 pos = new Vector3(Random.Range(-radius, radius), 0, Random.Range(-radius, radius));

        pos += transform.position;
        pos.y = 0;
        Instantiate(dechet, pos, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(transform.position, new Vector3(radius, 0, radius) * 2);
    }
}
