using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

    public float cost;
    public bool walkable;
    public Vector3 worldPos;
    public int gridX;
    public int gridZ;

    public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridZ) {
        walkable = _walkable;
        worldPos = _worldPos;
        gridX = _gridX;
        gridZ = _gridZ;
    }

}
