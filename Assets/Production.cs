using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Production : MonoBehaviour {
	//The people should own the means of production.
	//Long live the proletariat!
	public enum Resource {Food = 0, Lumber, Cement, Steel};

	public int[] income;
	public int ownerID;

	void Awake () {
		income = new int[System.Enum.GetNames (typeof(Resource)).Length];
	}
}
