using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackboard : MonoBehaviour {
    //These functions must be predefined because they are immediately requested by the behavior trees.

    //Commander:
    public List<Goblin> goblins;
    public int healthDefenseThreshold;
    public int healthSwarmThreshold;

    //Goblin:
    public float moveToCommanderRange;
    public int suicidalThreshold;
    public GameObject moveTarget;
    public BoidAgent boidUnit;

    //Bopth:
    public float behaviorTreeRecalculationDelay;
    public int health;
    public int meleeAttackDamage;
    public float MeleeRange;
    public float viewAngle;
    public List<float> chanceWeights;
    public AStarUnit unit;
    public Animator animator;
    public GameObject self;
    public GameObject attackTarget;
    public GameObject rock;
    public GameObject dynamite;
    [HideInInspector] public BTRoot rootNode;
    [HideInInspector] public GameObject target;
    [HideInInspector] public float range;

    //Switch our current target based wether a node needs to attack the player or defend the commander.
    public void SetTarget(string targetType) {
        if (targetType == "MoveTarget") {
            target = moveTarget;
        }
        else if (targetType == "AttackTarget") {
            target = attackTarget;
        }
    }

    //Switch range depening on wether a node needs to check for range of melee.
    public void SetRange(string rangeType) {
        if (rangeType == "MeleeRange") {
            range = MeleeRange;
        }
        else if (rangeType == "MoveRange") {
            range = moveToCommanderRange;
        }
    }
}
