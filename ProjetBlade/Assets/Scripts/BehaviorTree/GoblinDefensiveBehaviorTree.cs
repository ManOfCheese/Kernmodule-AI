using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinDefensiveBehaviorTree : MonoBehaviour {
    public bool treeActive = false;
    public BlackBoard blackBoard;

    private BTRoot rootNode;
    private BTPrioritySelector prioritySelectorNode;
    //If i am not in range of commander go to commander.
    private BTSequence sequenceNode;
    private BTInverter inverterNode;
    private BTAmInRange amInRangeNode;
    private BTMoveTowardsTarget moveTowardsTargetNode;
    //If i am in range of enemy make a melee attack.
    private BTSequence sequenceNode2;
    private BTAmInRange amInRangeNode2;
    private BTMeleeAttack meleeAttackNode;
    //If i am not in range of enemy make a ranged attack.
    private BTSequence sequenceNode3;
    private BTInverter inverterNode2;
    private BTAmInRange amInRangeNode3;
    private BTWeightedRandomSelector weightedRandomSelectorNode;
    private BTThrow throwNode;
    private BTThrow throwNode2;

    private Goblin goblin;

    private void Start() {
        goblin = this.GetComponent<Goblin>();

        //Instiate the nodes of the tree passing their children into the constructor.
        //Additionaly if a node has no children set isLeafNode to true.
        rootNode = new BTRoot(new List<ABTNode>() { prioritySelectorNode }, blackBoard, false, true);
        prioritySelectorNode = new BTPrioritySelector(new List<ABTNode>() { sequenceNode, sequenceNode2, sequenceNode3 }, blackBoard, false, false);
        sequenceNode = new BTSequence(new List<ABTNode>() { inverterNode, moveTowardsTargetNode }, blackBoard, false, false);
        inverterNode = new BTInverter(new List<ABTNode>() { amInRangeNode }, blackBoard, false, false);
        amInRangeNode = new BTAmInRange(null, blackBoard, true, false);
        moveTowardsTargetNode = new BTMoveTowardsTarget(null, blackBoard, true, false);
        sequenceNode2 = new BTSequence(new List<ABTNode>() { amInRangeNode2, meleeAttackNode }, blackBoard, false, false);
        amInRangeNode2 = new BTAmInRange(null, blackBoard, true, false);
        meleeAttackNode = new BTMeleeAttack(null, blackBoard, true, false);
        sequenceNode3 = new BTSequence(new List<ABTNode>() { inverterNode2, weightedRandomSelectorNode }, blackBoard, false, false);
        inverterNode2 = new BTInverter(new List<ABTNode>() { amInRangeNode3 }, blackBoard, false, false);
        amInRangeNode3 = new BTAmInRange(null, blackBoard, true, false);
        weightedRandomSelectorNode = new BTWeightedRandomSelector(new List<ABTNode>() { throwNode, throwNode2 }, blackBoard, false, false);
        throwNode = new BTThrow(null, blackBoard, true, false, blackBoard.rock);
        throwNode2 = new BTThrow(null, blackBoard, true, false, blackBoard.dynamite);

        //Assign A* variables.
        blackBoard.unit.moveTowardsTargetNode = moveTowardsTargetNode;
    }

    public void StartBehaviorTree() {
        StartCoroutine(CheckBehaviorTree());
    }

    public void StopBehaviorTree() {
        StopCoroutine(CheckBehaviorTree());
    }

    public IEnumerator CheckBehaviorTree() {
        while (treeActive) {
            Debug.Log("Checking Defensive Root");
            rootNode.StartBT();
            yield return new WaitForSeconds(blackBoard.behaviorTreeRecalculationDelay);
        }
    }
}
