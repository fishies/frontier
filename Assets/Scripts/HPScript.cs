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
            Debug.Log(transform.parent.GetChild(0).position);
            Debug.Log(transform.position);
            transform.position = transform.parent.GetChild(0).position;
            Debug.Log("new");
            Debug.Log(transform.position);
        }
    }
}
