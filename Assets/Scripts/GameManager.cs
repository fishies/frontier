using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private InputManager inputManager;

    public int currentPlayer;
	public int playerCount;

	// Use this for initialization
	void Start () {
        inputManager = GameObject.FindObjectOfType<InputManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	public void NextTurn () {
        inputManager.Mode = InputManager.Modes.SELECT;
        foreach (TowerBehavior tower in GameObject.FindObjectsOfType<TowerBehavior>())
        {
            tower.hasAttacked = false;
        }

        foreach (RaiderBehavior raider in GameObject.FindObjectsOfType<RaiderBehavior>())
        {
            raider.hasMoved = false;
        }

        currentPlayer %= playerCount;
		++currentPlayer;

        foreach (PlayerInfo pi in GameObject.FindObjectsOfType<PlayerInfo>())
        {
            if (pi.playerID == currentPlayer) {
                pi.CalculateIncome();
                break;
            }
        }
    }
}
