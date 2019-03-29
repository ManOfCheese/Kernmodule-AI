using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private int health;
    [HideInInspector] public Animator swordAnimator;
    public int damageAmount;

    private void Awake() {
        swordAnimator = GetComponent<Animator>();
    }

    private void Update() {
        if (Input.GetMouseButton(0)) {
            swordAnimator.SetTrigger("Attack");
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (swordAnimator.GetCurrentAnimatorStateInfo(0).IsName("SwordSwing")) {
            if (other.gameObject.GetComponent<Goblin>()) {
                other.gameObject.GetComponent<Goblin>().RecieveDamage(damageAmount);
            }
            if (other.gameObject.GetComponent<GoblinCommander>()) {
                other.gameObject.GetComponent<GoblinCommander>().RecieveDamage(damageAmount);
            }
        }
    }


    public void RecieveDamage(int amount) {
        health -= amount;
        if (health <= 0) {
            Time.timeScale = 0;
        }
    }
}
