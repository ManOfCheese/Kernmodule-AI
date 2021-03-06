﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The melee attack node will return runnign while attacking. Upon the completion of each attack it returns succes and otherwise it returns failure.
public class BTMeleeAttack : ABTNode {
    private bool initialized;

    public BTMeleeAttack(List<ABTNode> childNodes, Blackboard blackBoard, bool isLeafNode, bool isRootNode) {
        this.childNodes = childNodes;
        this.blackBoard = blackBoard;
        this.isLeafNode = isLeafNode;
        this.isRootNode = isRootNode;
    }

    public void Initialize() {
        blackBoard.animator.SetTrigger("MeleeAttack");
    }

    public override TaskState Tick() {
        if (!initialized) {
            Initialize();
            initialized = true;
        }
        blackBoard.animator.SetBool("Running", false);
        //If the animation has been completed return succes.
        if (blackBoard.animator.GetCurrentAnimatorStateInfo(0).length > blackBoard.animator.GetCurrentAnimatorStateInfo(0).normalizedTime) {
            initialized = false;
            return TaskState.Succes;
        }
        //If the animation is not completed by is playing return running.
        else if (blackBoard.animator.GetCurrentAnimatorStateInfo(0).IsName("GoblinMeleeAttack")) {
            blackBoard.rootNode.runningNode = this;
            return TaskState.Running;
        }
        blackBoard.animator.SetBool("MeleeAttack", false);
        initialized = false;
        return TaskState.Failure;
    }
}
