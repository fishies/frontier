using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnDispController : MonoBehaviour {

    private GameManager gm;
    private Text t;
    
	void Start () {
        gm = FindObjectOfType<GameManager>();
        t = GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        t.text = "Player " + gm.currentPlayer.ToString();	
	}
}
