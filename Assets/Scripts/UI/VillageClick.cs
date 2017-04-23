using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageClick : MonoBehaviour {

    private void OnMouseDown() {
        Placer p = null;
        foreach(Placer place in GameObject.FindObjectsOfType<Placer>())
        {
            if (place.gameObject.activeSelf)
            {
                p = place;
                break;
            }
        }

        if(p != null) {
            Debug.Log(p.ToString()+" "+transform.ToString());
            p.VillagePlace(transform);
        }
    }

}
