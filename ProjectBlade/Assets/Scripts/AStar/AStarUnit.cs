using UnityEngine;
using System.Collections;

public class AStarUnit : MonoBehaviour {
    public float speed = 20;
    public Transform target;
    public BTMoveTowardsTarget moveTowardsTargetNode;

    private AStarGrid aStarGrid;
    private Vector3[] path;
    private int targetIndex;

    private void Start() {
        aStarGrid = GameObject.Find("A*").GetComponent<AStarGrid>();
    }

    public void RequestPath(Transform target) {
        AStarPathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
    }

    //Activated by the callback from the PathRequestManager when a path is found.
    public void OnPathFound(Vector3[] newPath, bool pathSuccessful) {
        if (pathSuccessful) {
            //Start following the path and tell that to the moveTowardsTargetNode.
            moveTowardsTargetNode.pathFound = 1;
            path = newPath;
            targetIndex = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
        else {
            //Tell the moveTowardsTargetNode we failed to find a path.
            moveTowardsTargetNode.pathFound = 2;
        }
    }

    IEnumerator FollowPath() {
        Vector3 currentWaypoint = path[0];
        //We don't want to change our y position because then we move through the floor.
        for (int i = 0; i < path.Length; i++) {
            path[i] = new Vector3(path[i].x, transform.position.y, path[i].z);
        }

        while (true) {
            //If we have reached our waypoint set the next waypoint as target.
            if (transform.position == currentWaypoint) {
                targetIndex++;
                if (targetIndex >= path.Length) {
                    moveTowardsTargetNode.goalReached = true;
                    yield break;
                }
                currentWaypoint = path[targetIndex];
            }
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
            yield return null;
        }
    }

    //Draw gizmos for our current path.
    public void OnDrawGizmos() {
        if (path != null) {
            for (int i = targetIndex; i < path.Length; i++) {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector3.one);
                if (i == targetIndex) {
                    Gizmos.DrawLine(transform.position, path[i]);
                }
                else {
                    Gizmos.DrawLine(path[i - 1], path[i]);
                }
            }
        }
    }
}