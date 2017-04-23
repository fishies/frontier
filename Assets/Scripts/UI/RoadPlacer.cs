using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadPlacer : Placer {

    private Transform t_one;
    public Transform Start {
        get {
            return t_one;
        }
    }

    public override void VillagePlace(Transform t) {
        if(t.GetComponent<Production>().ownerID == 0 ||
                t.GetComponent<Production>().ownerID == spawner.PlayerID)
        { //if it is not an enemy village
            if(t_one != null && 
                    (t.GetComponent<Production>().ownerID == spawner.PlayerID ||
                    t_one.GetComponent<Production>().ownerID == spawner.PlayerID)
            ) { //if the other village has been selected, and atleast one of them belongs to the active player
                spawner.Spawn(Spawner.Objs.TOWER, t_one, t);
                t_one = null;
            }else {
                t_one = t;
            }
        }
    }
}
