using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAgressiveBehaviorTree : MonoBehaviour {
    public bool treeActive = false;
    public Blackboard blackBoard;

    private BTRoot rootNode;
    private BTPrioritySelector prioritySelectorNode;
    //If i am not in range and not seen move towards target.
    private BTSequence sequenceNode;
    private BTInverter inverterNode;
    private BTAmInRange amInRangeNode;
    private BTInverter inverterNode2;
    private BTAmSeen amSeenNode;
    private BTMoveTowardsTarget moveTowardsTargetNode;
    //If i am not in range and seen make a ranged attack.
    private BTSequence sequenceNode2;
    private BTInverter inverterNode3;
    private BTAmInRange amInRangeNode2;
    private BTAmSeen amSeenNode2;
    private BTWeightedRandomSelector weightedRandomSelectorNode;
    private BTThrow throwNode;
    private BTThrow throwNode2;
    //If i am in range make a melee attack.
    private BTSequence sequenceNode3;
    private BTAmInRange amInRangeNode3;
    private BTMeleeAttack meleeAttackNode;

    private Goblin goblin;

    // Start is called before the first frame update
    void Awake() {
        goblin = this.GetComponent<Goblin>();

        //Instiate the nodes of the tree passing their children into the constructor.
        //Additionaly if a node has no children set isLeafNode to true.
        amInRangeNode = new BTAmInRange(null, blackBoard, true, false, "AttackTarget", "MeleeRange");
        inverterNode = new BTInverter(new List<ABTNode>() { amInRangeNode }, blackBoard, false, false);
        amSeenNode = new BTAmSeen(null, blackBoard, true, false, "AttackTarget");
        inverterNode2 = new BTInverter(new List<ABTNode>() { amSeenNode }, blackBoard, false, false);
        moveTowardsTargetNode = new BTMoveTowardsTarget(null, blackBoard, false, false, "AttackTarget");
        
        amInRangeNode2 = new BTAmInRange(null, blackBoard, true, false, "AttackTarget", "MeleeRange");
        inverterNode3 = new BTInverter(new List<ABTNode>() { amInRangeNode2 }, blackBoard, false, false);
        amSeenNode2 = new BTAmSeen(null, blackBoard, false, false, "AttackTarget");

        throwNode = new BTThrow(null, blackBoard, true, false, blackBoard.rock, "AttackTarget");
        throwNode2 = new BTThrow(null, blackBoard, true, false, blackBoard.dynamite, "AttackTarget");
        weightedRandomSelectorNode = new BTWeightedRandomSelector(new List<ABTNode>() { throwNode, throwNode2 }, blackBoard, false, false);

        amInRangeNode3 = new BTAmInRange(null, blackBoard, true, false, "AttackTarget", "MeleeRange");
        meleeAttackNode = new BTMeleeAttack(null, blackBoard, true, false);

        sequenceNode3 = new BTSequence(new List<ABTNode>() { amInRangeNode3, meleeAttackNode }, blackBoard, false, false);
        sequenceNode2 = new BTSequence(new List<ABTNode>() { inverterNode3, amSeenNode2, weightedRandomSelectorNode }, blackBoard, false, false);
        sequenceNode = new BTSequence(new List<ABTNode>() { inverterNode, inverterNode2, moveTowardsTargetNode }, blackBoard, false, false);

        prioritySelectorNode = new BTPrioritySelector(new List<ABTNode>() { sequenceNode, sequenceNode2, sequenceNode3 }, blackBoard, false, false);
        rootNode = new BTRoot(new List<ABTNode>() { prioritySelectorNode }, blackBoard, false, true);

        //Assign A* variables.
        blackBoard.unit.moveTowardsTargetNode = moveTowardsTargetNode;
        blackBoard.rootNode = rootNode;
    }

    public void StartBehaviorTree() {
        StartCoroutine(CheckBehaviorTree());
    }

    public void StopBehaviorTree() {
        StopCoroutine(CheckBehaviorTree());
    }

    public IEnumerator CheckBehaviorTree() {
        while (treeActive) {
            rootNode.StartBT();
            yield return new WaitForSeconds(blackBoard.behaviorTreeRecalculationDelay);
        }
    }
}
