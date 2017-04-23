using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Placer : MonoBehaviour {

    protected Spawner spawner{
        get { //returns the spawner to be used
            int currentPlayer = FindObjectOfType<GameManager>().currentPlayer;
            foreach (Spawner s in FindObjectsOfType<Spawner>()) {
                if (s.PlayerID == currentPlayer) {
                    return s;
                }
            }
            return null;
        }
    }

    public virtual void VillagePlace(Transform t) { }

}
