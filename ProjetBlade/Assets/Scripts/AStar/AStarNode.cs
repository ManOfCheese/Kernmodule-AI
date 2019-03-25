using UnityEngine;
using System.Collections;

public class AStarNode : IHeapItem<AStarNode> {

    public bool walkable;
    private bool unitOnNode = false;
    public bool UnitOnNode {
        get {
            return unitOnNode;
        }
        set {
            unitOnNode = value;
            if (unitOnNode == true) {
                aStarGrid.unitNodes.Add(this);
            }
            else if (unitOnNode == false) {
                aStarGrid.unitNodes.Remove(this);
            }
        }
    }
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;
    public AStarNode parent;
    private int heapIndex;
    private AStarGrid aStarGrid;

    public AStarNode(AStarGrid aStarGrid, bool walkable, Vector3 worldPos, int gridX, int gridY) {
        this.aStarGrid = aStarGrid;
        this.walkable = walkable;
        this.worldPosition = worldPos;
        this.gridX = gridX;
        this.gridY = gridY;
    }

    public int fCost {
        get {
            return gCost + hCost;
        }
    }

    public int HeapIndex {
        get {
            return heapIndex;
        }
        set {
            heapIndex = value;
        }
    }

    public int CompareTo(AStarNode nodeToCompare) {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0) {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }
}