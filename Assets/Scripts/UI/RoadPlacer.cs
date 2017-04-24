using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadPlacer : Placer {

    private Transform t_one;
    public Transform VillageOne {
        get {
            return t_one;
        }
        set
        {
            t_one = value;
        }
    }

    public override void VillagePlace(Transform t) {
        Debug.Log("Villages that are null: "+(t_one == null).ToString()+" "+(t == null).ToString());
        t.GetComponent<SpriteRenderer>().color = Color.red;
        if (t.GetComponent<Production>().ownerID == 0 ||
                t.GetComponent<Production>().ownerID == spawner.PlayerID)
        { //if it is not an enemy village
            if(t_one != null && 
                    (t.GetComponent<Production>().ownerID == spawner.PlayerID ||
                    t_one.GetComponent<Production>().ownerID == spawner.PlayerID) && t != t_one
            ) { //if the other village has been selected, and atleast one of them belongs to the active player
                //Debug.Log("Spawning road from " + t_one.ToString() + " to " + t.ToString());
                foreach (RoadHandler rh in FindObjectsOfType<RoadHandler>())
                {
                    if ((rh.village1.transform == t && rh.village2.transform == t_one) || (rh.village1.transform == t_one && rh.village2.transform == t)) {
                        t.GetComponent<SpriteRenderer>().color = Color.white;
                        t_one.GetComponent<SpriteRenderer>().color = Color.white;
                        t_one = null;
                        return;
                    }
                }
                t.GetComponent<SpriteRenderer>().color = Color.white;
                t_one.GetComponent<SpriteRenderer>().color = Color.white;
                spawner.Spawn(Spawner.Objs.ROAD, t_one, t);
                t_one = null;
            } else {
                if (t_one != null && t != t_one)
                {
                    t_one.GetComponent<SpriteRenderer>().color = Color.white;
                }
                t_one = t;
            }
        }
    }
}
