using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterGameInit : MonoBehaviour {

	public GameObject villagePrefab; // U MUST SET THIS THX
	public GameObject capitalPrefab; // USE THE EDITOR THX

	private const int boardX = 10;
	private const int boardY = 7;
	private const int uiPxTop = 150;
	private const int uiPxBot = 150;

	private const int halfSize = (boardX * boardY) / 2;

	//void Awake() {generateBoard ();}

	public void generateBoard () {
		Vector2 boardSizeOnScreen = new Vector2 (Screen.width, Screen.height - uiPxBot - uiPxTop); // kore wa geemu jamu desu
		Vector2 cellSizeOnScreen = new Vector2 (boardSizeOnScreen.x / boardX, boardSizeOnScreen.y / boardY);

		List<Vector2> positionWithinCell = new List<Vector2>();
		for (int i = 0; i < halfSize; ++i) {
			positionWithinCell.Add(new Vector2(Random.value * cellSizeOnScreen.x, Random.value * cellSizeOnScreen.y));
		}
		positionWithinCell [27] = new Vector2 (0.5f * cellSizeOnScreen.x, 0.5f * cellSizeOnScreen.y);

		List<int> villageSize = new List<int>();
		List<Production.Resource> villageType = new List<Production.Resource>();

		// generate non-farm villages
		List<int> sizeBucket = new List<int>{3,2,2,2,1,1,1,1,1}; // bucket of sizes to be placed
		List<Production.Resource> typeBucket = new List<Production.Resource>
		{Production.Resource.Cement,Production.Resource.Lumber,Production.Resource.Steel}; // bucket of types to be placed: this will be refilled in the loop when empty

		for (int i = 0; i < halfSize; ++i) {
			int currSize = 0;
			Production.Resource currType = Production.Resource.Food; // i really should define Production.Resource.None == -1 tbh
			if (i != 27) { // branchpredictor-san, gomen...
				if (!(i == 28 && villageSize[20] > 0)) { // i really hope the compiler does manual loop unrolling lol
					// probability of placing something should depend heavily on remaining elements in bucket
					// it should also approach 1 as i approaches halfSize
					if (Random.value > ((1.0f * (i + 0.625f) * sizeBucket.Count) / (halfSize - 1.125f))) {
						int indexHit = Random.Range (0, sizeBucket.Count);
						currSize = sizeBucket[indexHit];
						sizeBucket.RemoveAt (indexHit);

						if (typeBucket.Count < 1) { // refill the bucket!
							typeBucket.Add (Production.Resource.Cement);
							typeBucket.Add (Production.Resource.Lumber);
							typeBucket.Add (Production.Resource.Steel);
						}

						indexHit = Random.Range (0, typeBucket.Count);
						currType = typeBucket [indexHit];
						typeBucket.RemoveAt (indexHit);
					}
				}
			}
			villageSize.Add (currSize);
			villageType.Add (currType);
		}

		// Debug.Assert (sizeBucket.Count == 0);
		sizeBucket.Add (2);
		sizeBucket.Add (1);
		sizeBucket.Add (1);
		HashSet<int> innerIndices = new HashSet<int>{27,28,29,30,20,21,22,14,15,9}; // game jam stratz

		// place outer farms
		for (int i = 0; sizeBucket.Count > 0; i = (i+1) % halfSize) {
			if (villageSize[i] > 0 || innerIndices.Contains(i))
				continue;
			if (Random.value > 0.875f) {
				int indexHit = Random.Range (0, sizeBucket.Count);
				villageSize [i] = sizeBucket [indexHit];
				sizeBucket.RemoveAt (indexHit);
			}
		}

		// place starting farm: the only big farm as well as the only "inner" farm
		if (villageSize [20] > 0) {
			villageSize [28] = 3;
		} else if (villageSize [28] > 0) {
			villageSize [20] = 3;
		} else {
			villageSize [(Random.value > 0.5f) ? 20 : 28] = 3;
		}

		// actual instantiation step
		for (int row = 0, i = 0; row < boardY; ++row) {
			for (int col = 0; col < row + 2; ++col, ++i) {
				if (villageSize[i] > 0) {
				//do math with row/col num to get screen coords
					Vector3 spawnLoc = Camera.main.ScreenToWorldPoint(new Vector3
							(cellSizeOnScreen.x * col + positionWithinCell[i].x,
							(Screen.height) - (cellSizeOnScreen.y * row + positionWithinCell[i].y + uiPxTop), 1.0f));
				//instantiate based on production type
					GameObject village = Instantiate(villagePrefab, spawnLoc, Quaternion.identity);
					Production p = village.GetComponent<Production> ();
					p.ownerID = 1;
					// i need to set: village income, sprite, and text number indicating size
					switch (villageType[i]) {
					case Production.Resource.Food:
						p.income [(int)Production.Resource.Food] = 2 + villageSize [i];
						// TODO: set sprite and text number :^) for each of deez
						break;
					case Production.Resource.Cement:
						p.income [(int)Production.Resource.Food] = - villageSize [i];
						p.income [(int)Production.Resource.Cement] = villageSize [i];
						break;
					case Production.Resource.Lumber:
						p.income [(int)Production.Resource.Food] = - villageSize [i];
						p.income [(int)Production.Resource.Cement] = 1 + villageSize [i];
						break;
					case Production.Resource.Steel:
						p.income [(int)Production.Resource.Food] = - villageSize [i];
						p.income [(int)Production.Resource.Cement] = 1 + villageSize [i];
						break;
					default:
						break;
					}
				}
			}
		}

		// mirror mirror on the wall, which kingdom be the edgiest of them all?
		//positionWithinCell = reversedList (positionWithinCell);
		//villageSize = reversedList (villageSize);
		//villageType = reversedList (villageType);

		// yes i literally copypasted but i gotta change some shit too
		for (int row = 0, i = 0; row < boardY; ++row) {
			for (int col = row + 2; col < boardX; ++col, ++i) {
				if (villageSize[i] > 0) {
					//do math with row/col num to get screen coords
					Vector3 spawnLoc = Camera.main.ScreenToWorldPoint(new Vector3
						(cellSizeOnScreen.x * col + positionWithinCell[i].x,
							(Screen.height) - (cellSizeOnScreen.y * row + positionWithinCell[i].y + uiPxTop), 1.0f));
					//instantiate based on production type
					GameObject village = Instantiate(villagePrefab, spawnLoc, Quaternion.identity);
					Production p = village.GetComponent<Production> ();
					p.ownerID = 2;
					// i need to set: village income, sprite, and text number indicating size
					switch (villageType[i]) {
					case Production.Resource.Food:
						p.income [(int)Production.Resource.Food] = 2 + villageSize [i];
						// TODO: set sprite and text number :^) for each of deez
						break;
					case Production.Resource.Cement:
						p.income [(int)Production.Resource.Food] = - villageSize [i];
						p.income [(int)Production.Resource.Cement] = villageSize [i];
						break;
					case Production.Resource.Lumber:
						p.income [(int)Production.Resource.Food] = - villageSize [i];
						p.income [(int)Production.Resource.Cement] = 1 + villageSize [i];
						break;
					case Production.Resource.Steel:
						p.income [(int)Production.Resource.Food] = - villageSize [i];
						p.income [(int)Production.Resource.Cement] = 1 + villageSize [i];
						break;
					default:
						break;
					}
				}
			}
		}

		// TODO: place capitals and connect them to their starter farms

	}

	/*
	private List<T> reversedList<T>(List<T> list) {
		List<T> output = new List<T>();
		for (int i = list.Count-1; i >= 0; ++i) {
			output.Add (list [i]);
		}
		return output;
	}
	*/
}