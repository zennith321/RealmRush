﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Waypoint startWaypoint, endWaypoint;
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    Queue<Waypoint> queue = new Queue<Waypoint>();
    bool isRunning = true;
    Vector2Int[] directions = {
        Vector2Int.up, //Shorthand for Vector2Int(0,1) etc...
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };
    void Start()
    {
        LoadBlocks();
        ColorStartAndEnd();
        Pathfind();
        //ExploreNeighbours();
    }

    private void Pathfind()
    {
        queue.Enqueue(startWaypoint);

        while(queue.Count > 0)
        {
            var searchCenter = queue.Dequeue();
            print("Searching from: " + searchCenter); //TODO remove log
            HaltIfEndFound(searchCenter);
        }

        print("Finished pathfinding");
    }

    private void HaltIfEndFound(Waypoint searchCenter)
    {
        if(searchCenter == endWaypoint)
        {
            print("Searching from end node, therefore stopping"); //TODO remove log
            isRunning = false;
        }
    }

    private void ExploreNeighbours()
    {
        foreach (Vector2Int direction in directions)
        {
            Vector2Int explorationCoordinates = startWaypoint.GetGridPos() + direction;
            try
            {
                grid[explorationCoordinates].SetTopColor(Color.blue);
            }
            catch
            {
                //do nothing
            }
        }
    }

    private void ColorStartAndEnd()
    {
        startWaypoint.SetTopColor(Color.green);
        endWaypoint.SetTopColor(Color.red);
    }

    private void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            var gridPos = waypoint.GetGridPos();
            bool isOverlapping = grid.ContainsKey(gridPos);
            if (isOverlapping)
            {
                Debug.LogWarning("Skipping overlapping block " + waypoint);
            }
            else
            {
                grid.Add(gridPos, waypoint);
                waypoint.SetTopColor(Color.gray);
            }
        }
        //print("Loaded " + grid.Count + " blocks");
    }
}
