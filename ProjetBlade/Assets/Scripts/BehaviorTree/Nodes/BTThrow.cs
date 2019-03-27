using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Returns running while throwing and succes after every thrown rock allowing reevaluation after every rock.
public class BTThrow : ABTNode {
    private GameObject projectile;
    private bool rockThrown;
    private bool initialized;
    private string targetKeyword;

    public BTThrow(List<ABTNode> childNodes, Blackboard blackBoard, bool isLeafNode, bool isRootNode, GameObject projectile, string targetKeyword) {
        this.childNodes = childNodes;
        this.blackBoard = blackBoard;
        this.isLeafNode = isLeafNode;
        this.isRootNode = isRootNode;
        this.projectile = projectile;
        this.targetKeyword = targetKeyword;
    }

    public void Initialize() {
        blackBoard.animator.SetTrigger("RangedAttack");
        blackBoard.self.GetComponent<Goblin>().ThrowRock(projectile, blackBoard.target.transform);
    }

    public override TaskState Tick() {
        blackBoard.SetTarget(targetKeyword);

        if (!initialized) {
            Initialize();
            initialized = true;
        }
        //If the animation is not completed by is playing return running.
        if (blackBoard.animator.GetCurrentAnimatorStateInfo(0).IsName("GoblinRangedAttack")) {
            blackBoard.rootNode.runningNode = this;
            return TaskState.Running;
        }
        //If the animation has been completed return succes.
        else if (blackBoard.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f) {
            initialized = false;
            return TaskState.Succes;
        }
        initialized = false;
        return TaskState.Failure;
    }
}
