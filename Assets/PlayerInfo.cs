using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour {
	public int playerID;
	public int[] stockpile;

	void Start () {
		stockpile = new int[System.Enum.GetNames (typeof(Production.Resource)).Length];
		stockpile [Production.Resource.Food] = 6;
		stockpile [Production.Resource.Lumber] = 6;
		stockpile [Production.Resource.Cement] = 6;
		stockpile [Production.Resource.Steel] = 6;
	}

	void CalculateIncome () { //This should be called at the start of the player's turn
		//Find all GameObjects with Production script component and ownerID == playerID
		//Change stockpile according to values in Production
	}
}
