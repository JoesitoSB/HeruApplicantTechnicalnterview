using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject robotPrefab = null;
    [SerializeField] private Transform robotsParent;
    [Tooltip("The number of robots should be odd. If is not, the script will add 1 to make it odd")]
    [SerializeField] private int robotsToSpawn = 0;
    [SerializeField] [Range(0.0f, 7.0f)] private float distanceBetweenObjects = 0.0f;
    [SerializeField] private SpawnableAxis axisWhereToSpawnRobots = SpawnableAxis.X;
    [SerializeField] [Range(-200.0f, 200.0f)] private float turnSpeed = 0f;
    
    private Color oddRobotsColor = Color.blue;
    private Color evenRobotsColor = Color.red;
    
    private List<Robot> spawnedRobots= new List<Robot>();

    [ExecuteInEditMode]
    private void OnValidate()
    {
        if (robotsToSpawn <= 0)
            robotsToSpawn = 1;
        else if (robotsToSpawn % 2 == 0)
            robotsToSpawn++;
    }

    private void Awake()
    {
        SpawnAndInitRobots();
    }

    private void SpawnAndInitRobots()
    {
        int totalPairs = (robotsToSpawn - 1) / 2; // Is not possible to be float because the main number is an odd
        float selectedAxisPosition = (totalPairs * distanceBetweenObjects) * -1;
        Vector3 position = Vector3.zero;

        for (int i = 0; i < robotsToSpawn; i++)
        {
            bool isOdd = i % 2 == 0;
            
            switch (axisWhereToSpawnRobots)
            {
                case SpawnableAxis.X:
                    position.Set(selectedAxisPosition, position.y, position.z);
                    break;
                case SpawnableAxis.Z:
                    position.Set(position.x, position.y, selectedAxisPosition);
                    break;
            }
                
            var robot = Instantiate(robotPrefab, robotsParent).GetComponent<Robot>();
            robot.Init(isOdd ? oddRobotsColor: evenRobotsColor, isOdd, position);   
            spawnedRobots.Add(robot);
            selectedAxisPosition += distanceBetweenObjects;
        }
    }

    private void Update()
    {
        robotsParent.Rotate(0f, turnSpeed * Time.deltaTime,0f);
    }
}
