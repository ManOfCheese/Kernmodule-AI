using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The melee attack node will return runnign while attacking. Upon the completion of each attack it returns succes and otherwise it returns failure.
public class BTMeleeAttack : ABTNode {

    public GameObject target;

    private Animator animator;
    private bool animationCompleted;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    public override TaskState Tick() {
        transform.LookAt(target.transform);
        animator.SetTrigger("MeleeAttack");
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
}
