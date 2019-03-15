using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float sensitivity;
    public SphereCollider groundCheck;
    public Rigidbody rigidBody;
    public Camera gameCamera;

    private Vector2 dir;
    private float rotationX;
    private float rotationY;

    private void Update() {
        //Get the direction from the Horizontal and Vertical axes and normalize them.
        dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        //Set the positon to the direction multiplied by speed.
        transform.position += new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized * Time.deltaTime * speed;

    }
}
