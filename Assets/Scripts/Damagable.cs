using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour {
    public int health;
	
	void Update() {
        if(health <= 0) {
            Destroy(gameObject);
        }
	}

    public void takeDamage(int attackStrength) {
        health = health - attackStrength;
    }
}
