using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPScript : MonoBehaviour {
    void Awake()
    {
        GetComponent<MeshRenderer>().sortingLayerName = "Default";
        GetComponent<MeshRenderer>().sortingOrder = 5;
    }

    void Update()
    {
        if (GetComponent<TextMesh>().characterSize == 0.03f)
        {
            transform.position = transform.parent.GetChild(0).position;
        }
    }
}
