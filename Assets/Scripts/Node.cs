using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool isObstacle;
    public Vector3 worldPosition;

    public Node(bool isObstacle, Vector3 worldPosition){
        this.isObstacle = isObstacle;
        this.worldPosition = worldPosition;
    }

}
