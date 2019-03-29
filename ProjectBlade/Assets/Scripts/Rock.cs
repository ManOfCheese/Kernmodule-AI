using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour {
    public int rockDamage;

    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Player>()) {
            other.GetComponent<Player>().RecieveDamage(rockDamage);
            Destroy(this.gameObject);
        }
        else {
            if (other.tag == "Floor") {
                Destroy(this.gameObject);
            }
        }
    }
}
