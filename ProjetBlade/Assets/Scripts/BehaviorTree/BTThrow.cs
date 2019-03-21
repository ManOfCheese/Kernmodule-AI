using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Returns running while throwing and succes after every thrown rock allowing reevaluation after every rock.
public class BTThrow : ABTNode {
    private GameObject projectile;
    private bool rockThrown;
    private bool initialized;

    public BTThrow(List<ABTNode> childNodes, BlackBoard blackBoard, bool isLeafNode, bool isRootNode, GameObject projectile) {
        this.childNodes = childNodes;
        this.blackBoard = blackBoard;
        this.isLeafNode = isLeafNode;
        this.isRootNode = isRootNode;
        this.projectile = projectile;
    }

    public void Initialize() {
        blackBoard.animator.SetTrigger("RangedAttack");
        blackBoard.self.GetComponent<Goblin>().ThrowRock(projectile, blackBoard.target.transform);
    }

    public override TaskState Tick() {
        if (!initialized) {
            Initialize();
            initialized = true;
        }
        //If the animation is not completed by is playing return running.
        if (blackBoard.animator.GetCurrentAnimatorStateInfo(0).IsName("GoblinRangedAttack")) {
            //Debug.Log("Throw || Running");
            return TaskState.Running;
        }
        //If the animation has been completed return succes.
        else if (blackBoard.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f) {
            initialized = false;
            //Debug.Log("Throw || Succes");
            return TaskState.Succes;
        }
        initialized = false;
        Debug.Log("Throw || Failure");
        return TaskState.Failure;
    }
}
