using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public List<Node> neighbors;
    public bool isObstacle;
    public Vector3 worldPosition;
    public Node parent = null;
    public int gridX;
    public int gridY;
    public int gCost;
    public int hCost;
    public int fCost{
        get{
            return hCost + gCost;
        }
    }

    public Node(bool isObstacle, Vector3 worldPosition, int _gridX, int _gridY){
        this.isObstacle = isObstacle;
        this.worldPosition = worldPosition;
        neighbors = new List<Node>();
        gridX = _gridX;
        gridY = _gridY;
    }



}
