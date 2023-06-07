# Astar-in-unity
Astar pathfinding project for pratice purpose
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
    *       - Nếu currentNode == targetNode -> Dò lại đường đi theo parent của currentNode. -> vẽ lại trên Gizmos
    */
