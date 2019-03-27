using UnityEngine;
using System.Collections;

public class AStarNode : IHeapItem<AStarNode> {

    public bool walkable;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;
    public AStarNode parent;
<<<<<<< HEAD
    private int heapIndex;

    public AStarNode(bool walkable, Vector3 worldPos, int gridX, int gridY) {
        this.walkable = walkable;
        this.worldPosition = worldPos;
        this.gridX = gridX;
        this.gridY = gridY;
=======
    int heapIndex;

    public AStarNode(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY) {
        walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
>>>>>>> parent of 6a1702b... Bugfixes and making player and units unwalkable on the grid.
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