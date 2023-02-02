using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackCubesSpawner : MonoBehaviour
{
    public GameObject stackObjs;
    public int numbersofStacks;

    public float minSpawnPosX, maxSpawnPosX;
    public float minSpawnPosZ, maxSpawnPosZ;

    private List<Vector3> usedPoints;

    void Start()
    {
        usedPoints = new List<Vector3>();
        GenerateStacks();
    }

    void GenerateObject(GameObject go, int amount)
    {
        if (go == null) return;

        for (int i = 0; i < amount; i++)
        {
            GameObject temp = Instantiate(go);
            temp.name = "StackObjs" + i;
            Vector3 randomPoint = GetRandomPoints();
            usedPoints.Add(randomPoint);
            temp.gameObject.transform.position = new Vector3(randomPoint.x, temp.gameObject.transform.position.y, randomPoint.z);
        }
    }

    private void GenerateStacks()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Stack");

        foreach(GameObject g in objects)
        {
            Destroy(g);
        }

        GenerateObject(stackObjs, numbersofStacks);
    }

    Vector3 GetRandomPoints()
    {
        int xRandom = 0;
        int zRandom = 0;

        xRandom = (int)UnityEngine.Random.Range(minSpawnPosX, maxSpawnPosX);
        zRandom = (int)UnityEngine.Random.Range(minSpawnPosZ, maxSpawnPosZ);

        Vector3 tempVector = new Vector3(xRandom, 0.0f, zRandom);

        if (usedPoints.Contains(tempVector)) return GetRandomPoints();

        return tempVector;
    }
}
