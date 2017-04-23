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
        if (tag == "Capital") {
            GetComponent<PlayerInfo>().stockpile[(int)Production.Resource.Food] -= attackStrength;
        }
        else
        {
            health = health - attackStrength;
        }
    }
}
