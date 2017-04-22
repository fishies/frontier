using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour {
	public int playerID;
	public int[] stockpile;

	void Start () {
		stockpile = new int[System.Enum.GetNames (typeof(Production.Resource)).Length];
		stockpile [(int)Production.Resource.Food] = 6;
		stockpile [(int)Production.Resource.Lumber] = 6;
		stockpile [(int)Production.Resource.Cement] = 6;
		stockpile [(int)Production.Resource.Steel] = 6;
	}

	void CalculateIncome () { //This should be called at the start of the player's turn
		foreach(Production p in Object.FindObjectsOfType<Production>())
		{
			if (p.ownerID == playerID)
			{
				foreach(int resource in System.Enum.GetValues(typeof(Production.Resource)))
				{
					stockpile [resource] += p.income [resource];
				}
			}
		}
	}
}
