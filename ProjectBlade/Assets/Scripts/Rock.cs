using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour {
    public int rockDamage;

    private void OnTriggerEnter(Collider other) {
        //If a player enter the trigger damage the player.
        if (other.GetComponent<Player>()) {
            other.GetComponent<Player>().RecieveDamage(rockDamage);
            Destroy(this.gameObject);
        }
        //If it hits the floor detroy self.
        else {
            if (other.tag == "Floor") {
                Destroy(this.gameObject);
            }
        }
    }
}
