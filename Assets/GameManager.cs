using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameObject objectSelected; //static is the stuff u get after u do ur laundry :^)

	public int currentPlayer;
	public int playerCount;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void NextTurn () {
		currentPlayer %= playerCount;
		++currentPlayer;
	}
}
