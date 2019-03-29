using UnityEngine;
using System.Collections;

public class AStarNode : IHeapItem<AStarNode> {
    public bool walkable;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;
    public int gCost;
    public int hCost;
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
    public AStarNode parent;

    private int heapIndex;

    public AStarNode(bool walkable, Vector3 worldPos, int gridX, int gridY) {
        this.walkable = walkable;
        this.worldPosition = worldPos;
        this.gridX = gridX;
        this.gridY = gridY;
    }

    public int CompareTo(AStarNode nodeToCompare) {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0) {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }
}