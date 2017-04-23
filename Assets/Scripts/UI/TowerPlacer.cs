using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacer : Placer {

    private void Update() {
        /* When mouse left clicks, spawn a tower there.
         */
        if (Input.GetMouseButtonDown(0)) {
            spawner.Spawn(Spawner.Objs.TOWER, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

}
