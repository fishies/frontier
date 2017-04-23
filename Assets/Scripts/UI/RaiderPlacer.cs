using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiderPlacer : Placer {

    public override void VillagePlace(Transform t) {
        /* When a village is clicked on,
         * if this script is active and it is owned by the active player,
         * place a raider there.
         */
        
        if (t.GetComponent<Production>().ownerID == spawner.PlayerID) { 
            spawner.Spawn(Spawner.Objs.RAIDER, t.position);
        }
    }
}
