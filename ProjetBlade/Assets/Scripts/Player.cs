using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private int health;
    [HideInInspector] public Animator swordAnimator;
    public int damageAmount;

    private void Awake() {
        swordAnimator = transform.Find("Sword").GetComponent<Animator>();
    }

    private void Update() {
        if (Input.GetMouseButton(0)) {
            swordAnimator.SetTrigger("Attack");
            Collider[] hits = Physics.OverlapBox(transform.position + transform.forward, transform.localScale);
            foreach (Collider hit in hits) {
                if (hit.GetComponent<Goblin>()) {
                    hit.GetComponent<Goblin>().RecieveDamage(damageAmount);
                    Debug.Log("Dealing Damage To Goblin");
                }
                if (hit.GetComponent<GoblinCommander>()) {
                    hit.GetComponent<GoblinCommander>().RecieveDamage(damageAmount);
                    Debug.Log("Dealing Damage To Goblin Commander");
                }
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
