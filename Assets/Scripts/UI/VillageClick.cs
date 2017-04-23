using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageClick : MonoBehaviour {

    private void OnMouseDown() {
        Placer p = GameObject.FindObjectOfType<Placer>();
        if(p != null) {
            p.VillagePlace(transform);
        }
    }

}
