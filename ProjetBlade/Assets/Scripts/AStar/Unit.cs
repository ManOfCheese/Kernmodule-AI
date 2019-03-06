using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

    public Transform target;
    public float speed = 20;
    private Vector3[] path;
    private int targetIndex;

    void Start() {
        StartCoroutine(CheckPath());
    }

    IEnumerator CheckPath() {
        while (true) {
            PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
            yield return new WaitForSeconds(1);
        }
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful) {
        if (pathSuccessful) {
            path = newPath;
            targetIndex = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath() {
        if (path != null) {
            Vector3 currentWaypoint = path[0];

            while (true) {
                if (transform.position == currentWaypoint) {
                    targetIndex++;
                    if (targetIndex >= path.Length) {
                        yield break;
                    }
                    currentWaypoint = path[targetIndex];
                }

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