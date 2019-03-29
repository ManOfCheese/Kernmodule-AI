using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AStarPathRequestManager : MonoBehaviour {
    private bool isProcessingPath;
    private AStarPathfinding pathfinding;
    private Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    private PathRequest currentPathRequest;
    private static AStarPathRequestManager instance;

    void Awake() {
        instance = this;
        pathfinding = GetComponent<AStarPathfinding>();
    }

    //Requested paths are added to the queue to be processed.
    public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback) {
        PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
        instance.pathRequestQueue.Enqueue(newRequest);
        instance.TryProcessNext();
    }

    void TryProcessNext() {
        //We check paths one at a time using this queue to reduce overhead.
        if (!isProcessingPath && pathRequestQueue.Count > 0) {
            currentPathRequest = pathRequestQueue.Dequeue();
            isProcessingPath = true;
            pathfinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
        }
    }

    //Activates the callback and tries to process the next path.
    public void FinishedProcessingPath(Vector3[] path, bool success) {
        currentPathRequest.callback(path, success);
        isProcessingPath = false;
        TryProcessNext();
    }

    //Struct containing all necessary info for a path request.
    struct PathRequest {
        public Vector3 pathStart;
        public Vector3 pathEnd;
        public Action<Vector3[], bool> callback;

        public PathRequest(Vector3 start, Vector3 end, Action<Vector3[], bool> callback) {
            this.pathStart = start;
            this.pathEnd = end;
            this.callback = callback;
        }
    }
}