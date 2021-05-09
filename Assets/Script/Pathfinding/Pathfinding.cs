using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public GameObject LocalPoints;
    public GameObject ARCoreDevice;
  

    GRID grid;
    private Node oldStartNode;
    private Vector3 oldPosVec;
    private bool checkDraw;
    private bool firstInit;
     void Awake()
    {
        grid = GetComponent<GRID>();
        firstInit = true;
        checkDraw = true;
    }

   

    void Update()
    {
        /*  if (StartDestination.RoomNumber.Initialize_Start==true && StartDestination.RoomNumber.Initialize_Destination==true) {
              FindPath(StartPos.transform.GetChild(StartDestination.RoomNumber.StartPoint).position, Destination.transform.GetChild(StartDestination.RoomNumber.DestinationPoint).position);
              //FindPath(seeker.transform.position, target.transform.position);
              grid.drawPathObject();
          }*/

        /*
        if (NavData.Room.index>-1 && NavData.Start.index>-1 && NavData.Device.setPosition==true)
        {
            FindPath(ARCoreDevice.transform.position, LocalPoints.transform.GetChild(NavData.Room.index).position);

            grid.drawPathObject();
        }*/

        if (NavData.Room.index > -1 && NavData.Start.index > -1 && NavData.Device.setPosition == true) 
          {

              checkDraw = FindPath(ARCoreDevice.transform.position, LocalPoints.transform.GetChild(NavData.Room.index).position);

              if (checkDraw)
              {
                  grid.destroyNavObj();
                  grid.drawPathObject();
              }

          }

      //  FindPath(ARCoreDevice.transform.position, LocalPoints.transform.GetChild(NavData.Room.index).position);

        // Debug.Log("Position: "+LocalPoints.transform.GetChild(1).position);
        //  Debug.Log("Winkel_Lokal: "+LocalPoints.transform.GetChild(1).localEulerAngles);
        //   Debug.Log("Winkel: " + LocalPoints.transform.GetChild(1).eulerAngles);

    }
    bool FindPath(Vector3 startPos, Vector3 targetPos)
    {

        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        //Debug.Log("Hallo");
        //Debug.Log("new: " + startNode.worldPosition);

        //Debug.Log("OldNode: "+oldStartNode.worldPosition+ "  NewNode: "+startNode.worldPosition);
        //checkPlayerInNewNode(oldPosVec, startNode.worldPosition);


        if (firstInit==false) 
        {
            if (checkPlayerInNewNode(oldPosVec, startNode.worldPosition) == false)
            {
                //Debug.Log("Nicht im neuen Knoten");
                return false;
            }
        }
      //  Debug.Log("Im neuen Knoten");

        oldPosVec = grid.NodeFromWorldPoint(startPos).worldPosition;
        firstInit = false;
        

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
                RetracePath(startNode,targetNode);
                return true;
            }

            foreach (Node neighbour in grid.GetNeighbours(currentNode))
            {
                if (!neighbour.walkable || closeSet.Contains(neighbour))
                {
                    continue;
                }

                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode,neighbour);

                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour,targetNode);
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

        while (currentNode !=startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;

        }
        path.Reverse();
        
        grid.path = path;
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int distanceX = Mathf.Abs(nodeA.gridX-nodeB.gridX);
        int distanceY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (distanceX>distanceY)
        {
            return 14 * distanceY + 10 * (distanceX - distanceY);
        }
        return 14 * distanceX + 10 * (distanceY - distanceX);
    }

     bool checkPlayerInNewNode(Vector3 OldPos, Vector3 NewPos)
    {
        //Debug.Log("Ergebnis: "+ (OldPos - NewPos));
        if (Vector3.SqrMagnitude(OldPos - NewPos) < 0.0001)
        {
            
            return false;
        }

    
        return true;
    }



}
