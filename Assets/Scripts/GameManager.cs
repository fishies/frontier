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
        foreach(TowerBehavior tower in GameObject.FindObjectsOfType<TowerBehavior>())
        {
            tower.hasAttacked = false;
        }

        foreach (RaiderBehavior raider in GameObject.FindObjectsOfType<RaiderBehavior>())
        {
            raider.hasMoved = false;
        }

        currentPlayer %= playerCount;
		++currentPlayer;
        
	}
}
