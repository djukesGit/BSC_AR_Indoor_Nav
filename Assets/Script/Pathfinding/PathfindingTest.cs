﻿using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingTest : MonoBehaviour
{ 

    public GameObject Start;
    public GameObject Desitination;
    public GameObject DestinationScreen;
    public GameObject Astar;

    GRID grid;
    private Node oldStartNode;
    private Vector3 oldPosVec;
    private bool checkDraw;
    private bool firstInit;
    void OnEnable()
    {
        grid = GetComponent<GRID>();
        firstInit = true;

    }



    void Update()
    {
        if (firstInit)
        {
            oldPosVec = Start.transform.position;
            firstInit = false;
            FindPath(Start.transform.position, Desitination.transform.position);
            grid.drawPathObject();
            return;
        }

        

        if (changePosition(oldPosVec, Start.transform.position))
        {
            checkUseronDestination(Start.transform.position, Desitination.transform.position);

            grid.destroyNavObj();
            FindPath(Start.transform.position, Desitination.transform.position);
            grid.drawPathObject();
        }


        //  FindPath(LocalPoints.transform.GetChild(1).position, LocalPoints.transform.GetChild(2).position);
        //    grid.drawPathObject();

    }

    bool checkUseronDestination(Vector3 currentPosition,Vector3 DestinationPosition)
    {
        Node currentPos = grid.NodeFromWorldPoint(currentPosition);
        Node destinationPos = grid.NodeFromWorldPoint(DestinationPosition);

        if(currentPos.gridX == destinationPos.gridX && currentPos.gridY == destinationPos.gridY)
        {
            DestinationScreen.SetActive(true);
            Astar.SetActive(false);
            Debug.Log("User is on Destination");
            

            return true;
        }

        Debug.Log("UserPos: "+currentPos.gridX+" "+currentPos.gridY+"   DestiPos: "+destinationPos.gridX +" "+ destinationPos.gridY);
        return false;
    }
    bool changePosition(Vector3 oldPosition, Vector3 currentPosition)
    {


        Node oldPos = grid.NodeFromWorldPoint(oldPosition);
        Node currentPos = grid.NodeFromWorldPoint(currentPosition);
        


        if (oldPos.gridX == currentPos.gridX && oldPos.gridY == currentPos.gridY)
        {
            return false;
        }

        oldPosVec = currentPosition;
        return true;
    }
    bool FindPath(Vector3 startPos, Vector3 targetPos)
    {


        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);


        List<Node> openSet = new List<Node>();
        HashSet<Node> closeSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 00)
        {
            Node currentNode = openSet[0];

            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }
            openSet.Remove(currentNode);
            closeSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                RetracePath(startNode, targetNode);
                return true;
            }

            foreach (Node neighbour in grid.GetNeighbours(currentNode))
            {
                if (!neighbour.walkable || closeSet.Contains(neighbour))
                {
                    continue;
                }

                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);

                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }

        }
        return true;
    }

    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;

        }
        path.Reverse();

        grid.path = path;
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int distanceY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (distanceX > distanceY)
        {
            return 14 * distanceY + 10 * (distanceX - distanceY);
        }
        return 14 * distanceX + 10 * (distanceY - distanceX);
    }






}

