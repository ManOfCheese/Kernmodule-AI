using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : MonoBehaviour {
    public int dynamiteDamage;
    public float dynamiteRadius;

    //If we hit the player explode and damage all in range.
    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Player>()) {
            Collider[] hits = Physics.OverlapSphere(transform.position, dynamiteRadius);
            foreach (Collider hit in hits) {
                //Will later be replaced by an IDamagable interface.
                if (other.GetComponent<Player>()) {
                    other.GetComponent<Player>().RecieveDamage(dynamiteDamage);
                }
                if (other.GetComponent<Goblin>()) {
                    other.GetComponent<Goblin>().RecieveDamage(dynamiteDamage);
                }
                if (other.GetComponent<GoblinCommander>()) {
                    other.GetComponent<GoblinCommander>().RecieveDamage(dynamiteDamage);
                }
            }
            Destroy(this.gameObject);
        }
        //If the dynamite hits the floor explode.
        else {
            if (other.tag == "Floor") {
                Destroy(this.gameObject);
            }
        }
    }
}
