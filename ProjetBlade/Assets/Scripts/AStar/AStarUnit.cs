using UnityEngine;
using System.Collections;

public class AStarUnit : MonoBehaviour {

    public Transform target;
    public float speed = 20;
    private Vector3[] path;
    private int targetIndex;
    public BTMoveTowardsTarget moveTowardsTargetNode;

    public void RequestPath(Transform target) {
        AStarPathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful) {
        if (pathSuccessful) {
            moveTowardsTargetNode.pathFound = 1;
            path = newPath;
            targetIndex = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
        else {
            moveTowardsTargetNode.pathFound = 2;
        }
    }

    IEnumerator FollowPath() {
        if (path != null) {
            Vector3 currentWaypoint = path[0];
            for (int i = 0; i < path.Length; i++) {
                path[i] = new Vector3(path[i].x, transform.position.y, path[i].z);
            }

            while (true) {
                if (transform.position == currentWaypoint) {
                    targetIndex++;
                    if (targetIndex >= path.Length) {
                        moveTowardsTargetNode.goalReached = true;
                        yield break;
                    }
                    currentWaypoint = path[targetIndex];
                }
<<<<<<< HEAD
=======

>>>>>>> parent of 6a1702b... Bugfixes and making player and units unwalkable on the grid.
                transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
                yield return null;
            }
        }
        else {
            yield break;
        }
    }

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