using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AStarPathfinding : MonoBehaviour {
    private AStarPathRequestManager requestManager;
    private AStarGrid grid;

    void Awake() {
        requestManager = GetComponent<AStarPathRequestManager>();
        grid = GetComponent<AStarGrid>();
    }

    public void StartFindPath(Vector3 startPos, Vector3 targetPos) {
        StartCoroutine(FindPath(startPos, targetPos));
    }

    IEnumerator FindPath(Vector3 startPos, Vector3 targetPos) {
        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;
        AStarNode startNode = grid.NodeFromWorldPoint(startPos);
        AStarNode targetNode = grid.NodeFromWorldPoint(targetPos);
        //If we can't reach the destination or move we don't bother.
        if (startNode.walkable && targetNode.walkable) {
            //The openset is a heap for efficiency.
            Heap<AStarNode> openSet = new Heap<AStarNode>(grid.MaxSize);
            HashSet<AStarNode> closedSet = new HashSet<AStarNode>();
            openSet.Add(startNode);
            while (openSet.Count > 0) {
                //Process the next node.
                AStarNode currentNode = openSet.RemoveFirst();
                closedSet.Add(currentNode);
                //If we have reached our destination we can break from the loop.
                if (currentNode == targetNode) {
                    pathSuccess = true;
                    break;
                }
                //Go through neigbors
                foreach (AStarNode neighbour in grid.GetNeighbours(currentNode)) {
                    //If it's not walkable or is has already been looked at we don't need to bother.
                    if (!neighbour.walkable || closedSet.Contains(neighbour)) {
                        continue;
                    }
                    //Recalculate values for node.
                    int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                    if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = currentNode;
                        //If the neighbor isn't in the open set we add it to keep looking for our path.
                        if (!openSet.Contains(neighbour)) {
                            openSet.Add(neighbour);
                        }
                    }
                }
            }
        }
        yield return null;
        //If we found a path retrace it so we can return it.
        if (pathSuccess) {
            waypoints = RetracePath(startNode, targetNode);
        }
        if (waypoints.Length <= 0) {
            pathSuccess = false;
        }
        //Tell the request manager we finished and wether or not we found a path.
        requestManager.FinishedProcessingPath(waypoints, pathSuccess);
    }

    //Retrace the path from end to start and reverse it so we have it from start to end.
    private Vector3[] RetracePath(AStarNode startNode, AStarNode endNode) {
        List<AStarNode> path = new List<AStarNode>();
        AStarNode currentNode = endNode;
        while (currentNode != startNode) {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;
    }

    //Simplify the path to only include waypoints where we need to change direction.
    private Vector3[] SimplifyPath(List<AStarNode> path) {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;
        //If the direction changes we add it to the waypoints.
        for (int i = 1; i < path.Count; i++) {
            Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if (directionNew != directionOld) {
                waypoints.Add(path[i].worldPosition);
            }
            directionOld = directionNew;
        }
        return waypoints.ToArray();
    }

    //Gets the distance between two nodes using the manhatten distance.
    private int GetDistance(AStarNode nodeA, AStarNode nodeB) {
        //Use abs to get the distance in positive numbers
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        //Apply manhatten distance heuristic.
        if (dstX > dstY) {
            return 14 * dstY + 10 * (dstX - dstY);
        }
        return 14 * dstX + 10 * (dstY - dstX);
    }
}