using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public bool displayGridGizmos;
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public Node[,] grid;

    public float percentage = 0.9f;

    private float nodeDiameter;
    private int gridSizeX, gridSizeY;

    private void Awake() {
        nodeDiameter = nodeRadius * 2;                                  
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);   
        CreateGrid();                                                   
    }

    //Creates the grid.
    private void CreateGrid() {
        grid = new Node[gridSizeX, gridSizeY];                                                                                            
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;
        bool walkable;

        for (int x = 0; x < gridSizeX; x++) {  
            for (int y = 0; y < gridSizeY; y++) {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                if (Physics.OverlapSphere(worldPoint, nodeRadius, unwalkableMask).Length > 0) {
                    walkable = false;
                }
                else {
                    walkable = true;
                }
                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    //Gets the neighbors to the current node.
    public List<Node> GetNeighbors(Node node) {
        List<Node> neighbors = new List<Node>();

        for (int x = -1; x <= 1; x++) {                         
            for (int z = -1; z <= 1; z++) {
                if ((x == 0 && z == 0) || (x != 0 && z != 0))
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridZ + z;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
                    neighbors.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbors;
    }

    public Node NodeFromWorldPoint(Vector3 worldPos) {
        float percentX = (worldPos.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPos.y + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }

    public void FindPath(Vector3 destination) {
        Node targetNode = NodeFromWorldPoint(destination);
        List<Node> openList = new List<Node>();
        List<Node> closedList = new List<Node>();

        for (int x = 0; x < grid.GetLength(0) - 1; x++) {
            for (int y = 0; y < grid.GetLength(1) - 1; y++) {
                openList.Add(grid[x, y]);
            }
        }

        while (openList.Count > 0) {
            Node currentPos = openList[0];

            foreach (Node neighbor in GetNeighbors(currentPos)) {
                neighbor.cost *= percentage;
                if (!neighbor.walkable) {
                    neighbor.cost *= -1000000;
                }
                if (!closedList.Contains(neighbor)) {
                    openList.Add(neighbor);
                }
                closedList.Add(neighbor);
            }
            openList.RemoveAt(0);
        }
    }

    public Node GetNextNode(Vector3 currentPos) {
        Node currentNode = NodeFromWorldPoint(currentPos);
        Node nextNode = currentNode;

        foreach (Node neighbor in GetNeighbors(currentNode)) {
            if (nextNode == null) {
                nextNode = neighbor;
            }
            else if (nextNode != null) {
                if (neighbor.cost < nextNode.cost) {
                    nextNode = neighbor;
                }
            }
        }

        return nextNode;
    }

    //Draw Gizmos to display grid and wether or not a node is walkable.
    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 0, gridWorldSize.y));
        if (grid != null && displayGridGizmos == true) {                           
            foreach (Node n in grid) {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;
                Gizmos.DrawCube(n.worldPos, Vector3.one * (nodeDiameter - 0.1f));
            }
        }
    }

}
