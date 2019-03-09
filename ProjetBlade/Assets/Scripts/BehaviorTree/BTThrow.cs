using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Returns running while throwing and succes after every thrown rock allowing reevaluation after every rock.
public class BTThrow : ABTNode {

    public GameObject target;
    public GameObject rock;

    private Animator animator;
    private bool animationCompleted;
    private bool rockThrown;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    public override TaskState Tick() {
        GameObject newRock = Instantiate(rock, transform.position, Quaternion.identity);
        newRock.GetComponent<Rigidbody>().velocity = BallisticVel(target.transform);
        animator.SetTrigger("RangedAttack");
        //If the animation has been completed return succes.
        if (animationCompleted) {
            animationCompleted = false;
            return TaskState.Succes;
        }
        //If the animation is not completed by is playing return running.
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("RangedAttack")) {
            return TaskState.Running;
        }
        return TaskState.Failure;
    }

    public void AnimationComplete() {
        animationCompleted = true;
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
