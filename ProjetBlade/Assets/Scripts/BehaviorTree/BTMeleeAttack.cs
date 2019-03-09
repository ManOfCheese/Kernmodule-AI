using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The melee attack node will return runnign while attacking. Upon the completion of each attack it returns succes and otherwise it returns failure.
public class BTMeleeAttack : ABTNode {

    public GameObject target;
    public Animator animator;

    private bool initialized;

    public void Initialize() {
        animator.SetTrigger("MeleeAttack");
    }

    public override TaskState Tick() {
        if (!initialized) {
            Initialize();
            initialized = true;
        }
        animator.SetBool("Running", false);
        //If the animation has been completed return succes.
        if (animator.GetCurrentAnimatorStateInfo(0).length > animator.GetCurrentAnimatorStateInfo(0).normalizedTime) {
            initialized = false;
            //Debug.Log("MeleeAttack || Succes");
            return TaskState.Succes;
        }
        //If the animation is not completed by is playing return running.
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("GoblinMeleeAttack")) {
            //Debug.Log("MeleeAttack || Running");
            return TaskState.Running;
        }
        //Debug.Log("MeleeAttack || Failure");
        animator.SetBool("MeleeAttack", false);
        initialized = false;
        return TaskState.Failure;
    }
}
