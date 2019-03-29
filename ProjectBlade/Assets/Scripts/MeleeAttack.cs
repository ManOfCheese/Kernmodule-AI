using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour {
    public int meleeAttackDamage;

    public void Attack() {
        //Use Overlapbox to check for the player in front of self. If found damage the player.
        Collider[] hits = Physics.OverlapBox(transform.position + transform.forward, transform.localScale);
        foreach (Collider hit in hits) {
            if (hit.GetComponent<Player>()) {
                hit.GetComponent<Player>().RecieveDamage(meleeAttackDamage);
            }
        }
    }
}
