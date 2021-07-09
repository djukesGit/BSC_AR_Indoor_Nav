using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GRID : MonoBehaviour
{
    Node[,] grid;
    public GameObject NavigationObjectPrefab;
    public GameObject DestinationObject_Prefab;

    public Transform AllNavObjects;

    //public Transform Player;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public LayerMask unwalkableMask;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        //      NavigationObjectPrefab.SetActive(true);
        //NavigationObjectPrefab.transform.position = Vector3.zero;

        CreateGrid();
    }

    public List<Node> path;
    public List<GameObject> NaviObj;

    public void drawPathObject()
    {
       
        if (grid != null)
        {
            foreach (Node n in grid)
            { 
                if (path != null)
                {
                    if (path.Contains(n))
                    {

                        if (!n.drawObjects) {
                            NaviObj.Add(Instantiate(NavigationObjectPrefab, new Vector3(n.worldPosition.x, 1f, n.worldPosition.z), Quaternion.identity, AllNavObjects));
                            n.drawObjects = true;
                        }
                    }
                }
            }
        }
        Vector3 tempPos;
        tempPos = NaviObj[NaviObj.Count - 1].transform.position;
       // Debug.Log(NaviObj.Count +"   tempPos: "+ tempPos);
        
        Destroy(NaviObj[(NaviObj.Count-1)]);

        NaviObj.Add(Instantiate(DestinationObject_Prefab, tempPos, Quaternion.identity, AllNavObjects));
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position,new Vector3(gridWorldSize.x,1,gridWorldSize.y));


        
        if (grid != null)
        {
           // Node playerNode = NodeFromWorldPoint(Player.position);

            foreach(Node n in grid)
            {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;

                if (path != null)
                {
                    if (path.Contains(n))
                    {
                        //Debug.Log("");
                        Gizmos.color = Color.cyan;
                       /* 
                        if (n.isObjectonPath==false)
                        {
                            Instantiate(NavigationObjectPrefab, n.worldPosition, Quaternion.identity);
                            
                            n.isObjectonPath=true;

                        }*/
                        
                        // NavigationObjectPrefab.transform.localPosition = n.worldPosition;
                        // NavigationObjectPrefab.SetActive(true);
                    }
                }
                

               /* if (playerNode == n)
                {
                    Gizmos.color = Color.cyan;
                }*/
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX,gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for(int x=0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius,unwalkableMask));
                grid[x, y] = new Node(walkable,worldPoint,x,y);
            }
        }
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.FloorToInt((gridSizeX - 1) * percentX);
        int y = Mathf.FloorToInt((gridSizeY - 1) * percentY);

        return grid[x, y];
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x<=1; x++)
        {
            for (int y=-1;y<=1;y++)
            {
                if(x==0 && y == 0)
                
                    continue;

                    int checkX = node.gridX + x;
                    int checkY = node.gridY + y;

                    if (checkX>=0 && checkX < gridSizeX && checkY >=0 && checkY < gridSizeY)
                    {
                        neighbours.Add(grid[checkX,checkY]);
                    }
                
            }

        }
        return neighbours;

    }

    

    public void destroyNavObj()
    {
        if (NaviObj.Count <= 0)
        {
            return;
        }
        for(int i = 0; i < NaviObj.Count; i++)
        {
            Destroy(NaviObj[i]);
        }

        foreach (Node n in grid)
        {
            if (path != null)
            {
                n.drawObjects = false;
          
            }
        }

    }
    
}
