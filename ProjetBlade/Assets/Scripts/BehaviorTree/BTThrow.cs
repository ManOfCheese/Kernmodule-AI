using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Returns running while throwing and succes after every thrown rock allowing reevaluation after every rock.
public class BTThrow : ABTNode {

    public GameObject target;
    public GameObject rock;
    public Animator animator;

    private bool rockThrown;
    private bool initialized;


    public void Initialize() {
        animator.SetTrigger("RangedAttack");
        GameObject newRock = Instantiate(rock, transform.position + transform.forward, Quaternion.identity);
        newRock.GetComponent<Rigidbody>().velocity = BallisticVel(target.transform);
    }

    public override TaskState Tick() {
        if (!initialized) {
            Initialize();
            initialized = true;
        }
        //If the animation is not completed by is playing return running.
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("GoblinRangedAttack")) {
            //Debug.Log("Throw || Running");
            return TaskState.Running;
        }
        //If the animation has been completed return succes.
        else if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f) {
            initialized = false;
            //Debug.Log("Throw || Succes");
            return TaskState.Succes;
        }
        initialized = false;
        Debug.Log("Throw || Failure");
        return TaskState.Failure;
    }

    Vector3 BallisticVel(Transform target) {
        Vector3 dir = target.position - transform.position; // get target direction
        float h = dir.y;  // get height difference
        dir.y = 0;  // retain only the horizontal direction
        float dist = dir.magnitude;  // get horizontal distance
        dir.y = dist;  // set elevation to 45 degrees
        dist += h;  // correct for different heights
        float vel = Mathf.Sqrt(dist * Physics.gravity.magnitude);
        return vel * dir.normalized;  // returns Vector3 velocity
    }
}
