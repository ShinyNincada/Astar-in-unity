using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGrid : MonoBehaviour
{
    public Transform player;
    public Transform target;
     public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    Node[,] grid;

    public float nodeDiameter;
    public List<Node> path;
    public int gridSizeX  = 10, gridSizeY = 10;
  
    private void Start() {
        nodeDiameter = nodeRadius * 2;

        gridSizeX = Mathf.CeilToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.CeilToInt(gridWorldSize.y / nodeDiameter);

        CreateGrid();
    }

    void CreateGrid(){
        grid = new Node[gridSizeX, gridSizeY];

        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y /2;
        for(int x = 0; x < gridSizeX; x++){
           for(int y = 0; y < gridSizeY; y++){
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool unwalkable = Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask);
                grid[x, y] = new Node(unwalkable, worldPoint, x, y);
            } 
        }

        foreach(Node node in grid){
            GetNeighBours(node);
        }
    }
    
    public void GetNeighBours(Node node){
        for(int x = -1; x <= 1; x++){
            for(int y = -1; y <= 1; y++){
                if(x == 0 && y == 0)
                    continue;
                
                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if(checkX >= 0 && checkX <= gridSizeX - 1 && checkY >= 0 && checkY <= gridSizeY - 1){
                    node.neighbors.Add(grid[checkX, checkY]);
                }
            }
        }
    }
    public Node NodeFromWorldPoint(Vector3 worldPosition){
        float percentX = (worldPosition.x + gridWorldSize.x/2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y/2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX); 
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY); 
        return grid[x, y];
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 0,  gridWorldSize.y)); 
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y /2;
        Gizmos.DrawSphere(worldBottomLeft, nodeRadius);
        Gizmos.color = Color.red;        
   
        // for (int x = 0; x < 10; x++)
        // {
        //     for (int y = 0; y < 10; y++)
        //     {
        //         // Calculate the world position for the current grid cell
        //         Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.foward * (y * nodeDiameter + nodeRadius);
                
               
        //         // Draw the sphere at the calculated position
        //         Gizmos.DrawSphere(new Vector3(worldPoint.x, 0, worldPoint.y), 0.5f);
        //     }
        // } 

        if(grid != null){
            Node playerNode = NodeFromWorldPoint(player.position);
            foreach(Node cell in grid){
                Gizmos.color = cell.isObstacle?Color.red:Color.green;
                if(playerNode == cell){
                    Gizmos.color = Color.cyan;
                }
                if(path != null){
                    if(path.Contains(cell)){
                        Gizmos.color = Color.black;
                    }
                }
                Gizmos.DrawCube(cell.worldPosition, Vector3.one * (nodeDiameter-0.1f));
            }
        }

        
    }   
}
