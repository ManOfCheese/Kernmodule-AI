﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBoard : MonoBehaviour {
    //These functions must be predefined because they are immediately requested by the behavior trees.
    public float behaviorTreeRecalculationDelay;
    public float MeleeRange;
    public float moveToCommanderRange;
    public float viewAngle;

    public int health;
    public int suicidalThreshold;

    public List<float> chanceWeights;
    public AStarUnit unit;
    public BoidAgent boidUnit;
    public Animator animator;
    public GameObject self;
    public GameObject moveTarget;
    public GameObject attackTarget;
    public GameObject rock;
    public GameObject dynamite;

    [HideInInspector] public GameObject target;
    [HideInInspector] public float range;
}
