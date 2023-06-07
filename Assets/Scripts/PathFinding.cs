using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    MyGrid grid;
   
    // Start is called before the first frame update
    void Start()
    {
        grid = GetComponent<MyGrid>();
    }

    private void Update() {
        FindPath(grid.player.position, grid.target.position);
    }
    
    /*
    *Tìm đường:
    *   Tạo 2 List: OpenSet: Chứa các Node cần đi, ClosedSet: Chứa các Node đã đi - 1 HashSet để đảm bảo tính duy nhất của phần tử
    *   Bắt đầu: Thêm startNode vào OpenSet
    *   While(openSet.Count > 0) currentNode = openSet[0]: 
    *       - Bắt đầu, gán currentNode bằng phần tử đầu tiên của openSet
    *       - Lấy currentNode bằng ô có chi phí f nhỏ nhất, hoặc bằng currentNode nhưng có h - chi phí tới đích nhỏ hơn
    *       - Xoá currentNode khỏi openSet, thêm vào closedSet
    *       - Nếu currentNode không phải là đích
    *       - Duyệt Neighbour của currentNode
    *       - Nếu neighbour chưa đi hoặc có chi phí di chuyển mới nhỏ hơn add vào openSet, cập nhật g, h, f parent cho neighbour
    *
    */
    void FindPath(Vector3 startPos, Vector3 targetPos){
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();

        openSet.Add(startNode);
        while(openSet.Count > 0){
            Node currentNode = openSet[0];
            for(int i = 1; i < openSet.Count; i++){
                //nếu tìm được ô tốt hơn, f nhỏ hơn -> ô hiện tại là ô đó
                // nếu 2 ô bằng nhau, đi theo khoảng cách h - từ ô đang xét tới đích, lấy ngắn hơn
                if(openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost){
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if(currentNode == targetNode){
                RetracePath(startNode, targetNode);
                return;
            }

            foreach(Node neighbour in currentNode.neighbors){
                if(neighbour.isObstacle || closedSet.Contains(neighbour)){
                    continue;
                }

                int newMovementCost = currentNode.gCost + GetDistanceBetweenNodes(neighbour, currentNode);
                if(newMovementCost < neighbour.gCost || !openSet.Contains(neighbour)){
                    neighbour.gCost = newMovementCost;
                    neighbour.hCost = GetDistanceBetweenNodes(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if(!openSet.Contains(neighbour)){
                        openSet.Add(neighbour);
                    }
                }

            }
        }
    }

    void RetracePath(Node startNode, Node endNode){
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while(currentNode != startNode){
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();

        grid.path = path;
    }
    int GetDistanceBetweenNodes(Node nodeA, Node nodeB){
        return Mathf.Abs(nodeA.gridX - nodeB.gridX)  + Mathf.Abs(nodeA.gridY - nodeB.gridY);
    }
}
