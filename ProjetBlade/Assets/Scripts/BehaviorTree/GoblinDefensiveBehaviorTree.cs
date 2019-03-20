using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinDefensiveBehaviorTree : MonoBehaviour {

    public bool treeActive = false;

    public BTRoot rootNode;
    public BTPrioritySelector prioritySelectorNode;
    public BTSequence sequenceNode;
    public BTInverter inverterNode;
    public BTAmInRange amInRangeNode;
    public BTMoveTowardsTarget moveTowardsTargetNode;
    public BTSequence sequenceNode2;
    public BTAmInRange amInRangeNode2;
    public BTMeleeAttack meleeAttackNode;
    public BTWeightedRandomSelector weightedRandomSelectorNode;
    public BTThrow throwNode;
    public BTThrow throwNode2;

    private Goblin goblin;

    private void Start() {
        goblin = this.GetComponent<Goblin>();

        //Set children to create the tree structure.
        rootNode.childNodes.Add(prioritySelectorNode);
        rootNode.isRootNode = true;
        prioritySelectorNode.childNodes.Add(sequenceNode);
        sequenceNode.childNodes.Add(inverterNode);
        inverterNode.childNodes.Add(amInRangeNode);
        amInRangeNode.isLeafNode = true;
        sequenceNode.childNodes.Add(moveTowardsTargetNode);
        moveTowardsTargetNode.isLeafNode = true;
        prioritySelectorNode.childNodes.Add(sequenceNode2);
        sequenceNode2.childNodes.Add(amInRangeNode);
        amInRangeNode2.isLeafNode = true;
        sequenceNode2.childNodes.Add(meleeAttackNode);
        meleeAttackNode.isLeafNode = true;
        prioritySelectorNode.childNodes.Add(weightedRandomSelectorNode);
        weightedRandomSelectorNode.childNodes.Add(throwNode);
        throwNode.isLeafNode = true;
        weightedRandomSelectorNode.childNodes.Add(throwNode2);
        throwNode2.isLeafNode = true;

        //Set variables
        amInRangeNode.target = goblin.moveTarget;
        amInRangeNode.range = goblin.moveToCommanderRange;
        moveTowardsTargetNode.target = goblin.moveTarget;
        moveTowardsTargetNode.unit = goblin.unit;
        moveTowardsTargetNode.animator = goblin.animator;
        amInRangeNode2.target = goblin.attackTarget;
        amInRangeNode2.range = goblin.MeleeRange;
        meleeAttackNode.target = goblin.attackTarget;
        meleeAttackNode.animator = goblin.animator;
        weightedRandomSelectorNode.chanceWeights = goblin.chanceWeights;
        throwNode.projectile = goblin.rock;
        throwNode.target = goblin.attackTarget;
        throwNode.animator = goblin.animator;
        throwNode2.projectile = goblin.dynamite;
        throwNode2.target = goblin.attackTarget;
        throwNode2.animator = goblin.animator;

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
            Debug.Log("Checking Defensive Root");
            rootNode.StartBT();
            yield return new WaitForSeconds(goblin.behaviorTreeRecalculationDelay);
        }
    }
}
