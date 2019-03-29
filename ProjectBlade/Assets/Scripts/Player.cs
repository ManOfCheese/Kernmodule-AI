using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public int damageAmount;
    [HideInInspector] public Animator swordAnimator;

    [SerializeField] private int health;

    private void Awake() {
        swordAnimator = GetComponent<Animator>();
    }

    private void Update() {
        if (Input.GetMouseButton(0)) {
            swordAnimator.SetTrigger("Attack");
        }
    }

    private void OnTriggerEnter(Collider other) {
        //If we are in the swing animation damage any Goblins we hit (I don't currently have a common class or interface for this so we'll have to do it this way)
        if (swordAnimator.GetCurrentAnimatorStateInfo(0).IsName("SwordSwing")) {
            if (other.gameObject.GetComponent<Goblin>()) {
                other.gameObject.GetComponent<Goblin>().RecieveDamage(damageAmount);
            }
            if (other.gameObject.GetComponent<GoblinCommander>()) {
                other.gameObject.GetComponent<GoblinCommander>().RecieveDamage(damageAmount);
            }
        }
    }

    //Take damage equal to amount
    public void RecieveDamage(int amount) {
        health -= amount;
        //If we are dead freeze the game because no game over screen exists.
        if (health <= 0) {
            Time.timeScale = 0;
        }
    }
}
