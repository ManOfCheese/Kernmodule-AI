using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidAgent : MonoBehaviour {

    public bool debug;

    public float desiredSeparation;
    public float neighborRadius;

    public float alignmentWeight;
    public float cohesionWeight;
    public float separationWeight;

    private Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start() {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.velocity = new Vector3(Random.value * 2 - 1, Random.value * 2 - 1, Random.value * 2 - 1);
    }

    // Update is called once per frame
    void Update() {
        transform.rotation = Quaternion.LookRotation(rigidBody.velocity);

        //Get all nearby boids.
        List<BoidAgent> boids = GetBoidsInSphere();
        if (boids != null && rigidBody != null) {
            Vector3 alignment = Align(boids) * alignmentWeight * Time.deltaTime;
            Vector3 cohesion = Cohere(boids) * cohesionWeight * Time.deltaTime;
            Vector3 separation = Seperate(boids) * separationWeight * Time.deltaTime;

            rigidBody.velocity += (alignment + cohesion + separation);
        }
    }

    //This rule makes the boids align their velocity and thus direction.
    private Vector3 Align(List<BoidAgent> boids) {
        //Calculate the average velocity of surrounding boids and return the normalized.
        Vector3 velocity = Vector3.zero;
        foreach (BoidAgent boid in boids) {
            float distance = Vector3.Distance(transform.position, boid.transform.position);
            if (boid != this && distance > 0 && distance < boid.neighborRadius) {
                velocity += boid.rigidBody.velocity;
            }
        }
        return (velocity /= (boids.Count - 1)).normalized;
    }

    //This rule makes boids want to move towards other boids.
    private Vector3 Cohere(List<BoidAgent> boids) {
        //Calculate the average velocity of surrounding boids and return the normalized.
        Vector3 centerOfMass = Vector3.zero;
        foreach (BoidAgent boid in boids) {
            float distance = Vector3.Distance(transform.position, boid.transform.position);
            if (boid != this && distance > 0 && distance < boid.neighborRadius) {
                centerOfMass += boid.transform.position;
            }
        }
        return ((centerOfMass /= (boids.Count - 1)) - transform.position).normalized;
    }

    //This rule keeps boids from occupying the same space.
    private Vector3 Seperate(List<BoidAgent> boids) {
        //Calculate the average velocity of surrounding boids and return the normalized.
        Vector3 velocity = Vector3.zero;
        foreach (BoidAgent boid in boids) {
            float distance = Vector3.Distance(transform.position, boid.transform.position);
            if (boid != this && distance > 0 && distance < boid.neighborRadius) {
                velocity -= (boid.transform.position - transform.position).normalized / distance;
            }
        }
        return (velocity /= (boids.Count - 1)).normalized;
    }

    private List<BoidAgent> GetBoidsInSphere() {
        //Get surrounding gameObjects.
        Collider[] surroundingColliders = Physics.OverlapSphere(transform.position, 0.1f);
        //In this list we will store our surrounding boid agents.
        List<BoidAgent> boids = new List<BoidAgent>();
        foreach (Collider collider in surroundingColliders) {
            //If the object has a BoidAgent componenet add it to the list.
            if (collider.GetComponent<BoidAgent>()) {
                boids.Add(collider.gameObject.GetComponent<BoidAgent>());
            }
        }
        return boids;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == 9) {
            rigidBody.velocity *= -1;
        }
    }
}
