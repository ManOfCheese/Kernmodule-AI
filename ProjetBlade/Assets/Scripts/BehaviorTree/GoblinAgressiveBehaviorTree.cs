using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAgressiveBehaviorTree : MonoBehaviour {

    public bool treeActive = false;

    public BTRoot rootNode;
    public BTRepeat repeatNode;
    public BTPrioritySelector prioritySelectorNode;
    public BTSequence sequenceNode;
    public BTInverter inverterNode;
    public BTAmInRange amInRangeNode;
    public BTInverter inverterNode2;
    public BTAmSeen amSeenNode;
    public BTMoveTowardsTarget moveTowardsTargetNode;
    public BTSequence sequenceNode2;
    public BTInverter inverterNode3;
    public BTAmInRange amInRangeNode2;
    public BTAmSeen amSeenNode2;
    public BTWeightedRandomSelector weightedRandomSelectorNode;
    public BTThrow throwNode;
    public BTThrow throwNode2;
    public BTSequence sequenceNode3;
    public BTAmInRange amInRangeNode3;
    public BTMeleeAttack meleeAttackNode;

    private Goblin goblin;

    // Start is called before the first frame update
    void Start() {
        goblin = this.GetComponent<Goblin>();

        //Parent the nodes to eachother to form the trees structure.
        rootNode.childNodes.Add(prioritySelectorNode);
        rootNode.isRootNode = true;
        //repeatNode.childNodes.Add(prioritySelectorNode);
        prioritySelectorNode.childNodes.Add(sequenceNode);
        sequenceNode.childNodes.Add(inverterNode);
        inverterNode.childNodes.Add(amInRangeNode);
        amInRangeNode.isLeafNode = true;
        sequenceNode.childNodes.Add(inverterNode2);
        inverterNode2.childNodes.Add(amSeenNode);
        amSeenNode.isRootNode = true;
        sequenceNode.childNodes.Add(moveTowardsTargetNode);
        prioritySelectorNode.childNodes.Add(sequenceNode2);
        sequenceNode2.childNodes.Add(inverterNode3);
        inverterNode3.childNodes.Add(amInRangeNode2);
        amInRangeNode2.isLeafNode = true;
        sequenceNode2.childNodes.Add(amSeenNode2);
        sequenceNode2.childNodes.Add(weightedRandomSelectorNode);
        weightedRandomSelectorNode.childNodes.Add(throwNode);
        weightedRandomSelectorNode.childNodes.Add(throwNode2);
        prioritySelectorNode.childNodes.Add(sequenceNode3);
        sequenceNode3.childNodes.Add(amInRangeNode3);
        amInRangeNode3.isLeafNode = true;
        sequenceNode3.childNodes.Add(meleeAttackNode);
        meleeAttackNode.isLeafNode = true;

        //Assign behavior tree variables.
        amInRangeNode.target = goblin.attackTarget;
        amInRangeNode.range = goblin.MeleeRange;
        amInRangeNode2.target = goblin.attackTarget;
        amInRangeNode2.range = goblin.MeleeRange;
        amInRangeNode3.target = goblin.attackTarget;
        amInRangeNode3.range = goblin.MeleeRange;
        amSeenNode.target = goblin.attackTarget;
        amSeenNode.angle = goblin.viewAngle;
        amSeenNode2.target = goblin.attackTarget;
        amSeenNode2.angle = goblin.viewAngle;
        weightedRandomSelectorNode.chanceWeights = goblin.chanceWeights;
        throwNode.projectile = goblin.rock;
        throwNode.target = goblin.attackTarget;
        throwNode.animator = goblin.animator;
        throwNode2.projectile = goblin.dynamite;
        throwNode2.target = goblin.attackTarget;
        throwNode2.animator = goblin.animator;
        moveTowardsTargetNode.target = goblin.attackTarget;
        moveTowardsTargetNode.unit = goblin.unit;
        moveTowardsTargetNode.animator = goblin.animator;
        meleeAttackNode.target = goblin.attackTarget;
        meleeAttackNode.animator = goblin.animator;

        //Assign A* variables.
        goblin.unit.moveTowardsTargetNode = moveTowardsTargetNode;
    }

    public void StartBehaviorTree() {
        StartCoroutine(CheckBehaviorTree());
    }

    public void StopBehaviorTree() {
        StopCoroutine(CheckBehaviorTree());
    }

    public IEnumerator CheckBehaviorTree() {
        while (treeActive) {
            Debug.Log("Checking Agressive Root");
            rootNode.StartBT();
            yield return new WaitForSeconds(goblin.behaviorTreeRecalculationDelay);
        }
    }
}
