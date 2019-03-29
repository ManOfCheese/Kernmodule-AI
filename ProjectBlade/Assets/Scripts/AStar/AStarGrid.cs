using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AStarGrid : MonoBehaviour {
    public bool displayGridGizmos;
    public float nodeRadius;
    public int MaxSize {
        get {
            return gridSizeX * gridSizeY;
        }
    }
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;

    private float nodeDiameter;
    private int gridSizeX, gridSizeY;
    private AStarNode[,] grid;

    private void Awake() {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    public List<AStarNode> GetNeighbours(AStarNode node) {
        List<AStarNode> neighbours = new List<AStarNode>();
        //Loop through all surrounding nodes.
        for (int x = -1; x <= 1; x++) {
            for (int y = -1; y <= 1; y++) {
                if (x == 0 && y == 0) {
                    continue;
                }
                //x and y of our current neigbor.
                int checkX = node.gridX + x;
                int checkY = node.gridY + y;
                //Check if the neighbor is on the grid and not this node.
                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }
        return neighbours;
    }

    //Gets a node from a position in the world
    public AStarNode NodeFromWorldPoint(Vector3 worldPosition) {
        //Convert position into a percentage of how far into the grid it is.
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
        //Clamp it giving us a percentage in decimal points (between 0 and 1).
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);
        //Multiply it by the grid size and round it to an int to get the position of the nearest node to that exact position.
        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }

    private void CreateGrid() {
        grid = new AStarNode[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;
        //Loop through grid.
        for (int x = 0; x < gridSizeX; x++) {
            for (int y = 0; y < gridSizeY; y++) {
                //Node position in world space.
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                //Check for unwalkable objects to determine if this node is walkable.
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                grid[x, y] = new AStarNode(walkable, worldPoint, x, y);
            }
        }
    }

    //Draw gizmos for the grid and pathfinding.
    private void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
        if (grid != null && displayGridGizmos) {
            foreach (AStarNode n in grid) {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }
}