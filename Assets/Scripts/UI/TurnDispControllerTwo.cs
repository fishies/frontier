using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnDispControllerTwo : MonoBehaviour {

    public GameObject glow1;
    public GameObject glow2;
    public GameManager gm;

	void Start () {
        glow1 = transform.FindChild("Glow1").gameObject;
        glow2 = transform.FindChild("Glow2").gameObject;
        gm = FindObjectOfType<GameManager>();
	}
	
	void Update () {
        glow1.SetActive(gm.currentPlayer == 1);
        glow2.SetActive(gm.currentPlayer == 2);
	}

}
