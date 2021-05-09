using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    // Start is called before the first frame update
    public bool walkable;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;
    public int gCost, hCost;
    public Node parent;
    public Boolean drawObjects;
   // public GameObject NavObjectPrefab;

    public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY, Boolean _drawObjects = false) {

        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
        drawObjects = _drawObjects;
    }
    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
        
    }


}
