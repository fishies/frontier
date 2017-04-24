using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterGameInit : MonoBehaviour {

    public Sprite farmImg;
    public Sprite quarryImg;
    public Sprite lumberImg;
    public Sprite ironImg;


	public GameObject villagePrefab; // U MUST SET THIS THX
	public GameObject capitalPrefab; // USE THE EDITOR THX

	private const int boardX = 10;
	private const int boardY = 7;

	private Vector2 topLeft = new Vector2 (-15.0f, 10.5f);

	private const int halfSize = (boardX * boardY) / 2;

	void Awake() {generateBoard ();}

    public void generateBoard() {
        Vector2 boardSize = new Vector2((-topLeft.x) - topLeft.x, topLeft.y - (-topLeft.y));
        Vector2 cellSize = new Vector2(boardSize.x / boardX, boardSize.y / boardY);

        List<Vector2> positionWithinCell = new List<Vector2>();
        for (int i = 0; i < halfSize; ++i) {
            positionWithinCell.Add(new Vector2(Random.value * cellSize.x, Random.value * cellSize.y));
        }
        positionWithinCell[27] = new Vector2(0.5f * cellSize.x, 0.5f * cellSize.y);

        List<int> villageSize = new List<int>();
        List<Production.Resource> villageType = new List<Production.Resource>();

        // generate non-farm villages
        List<int> sizeBucket = new List<int> { 3, 2, 2, 2, 1, 1, 1, 1, 1 }; // bucket of sizes to be placed
        List<Production.Resource> typeBucket = new List<Production.Resource>
        {Production.Resource.Cement,Production.Resource.Lumber,Production.Resource.Steel}; // bucket of types to be placed: this will be refilled in the loop when empty

        for (int i = 0; i < halfSize; ++i) {
            int currSize = 0;
            Production.Resource currType = Production.Resource.Food; // i really should define Production.Resource.None == -1 tbh
            if (i != 27) { // branchpredictor-san, gomen...
                if (!(i == 28 && villageSize[20] > 0)) { // i really hope the compiler does manual loop unrolling lol
                                                         // probability of placing something should depend heavily on remaining elements in bucket
                                                         // it should also approach 1 as i approaches halfSize
                    if (Random.value > ((1.0f * (i + 0.125f) * sizeBucket.Count) / (halfSize - 1.625f))) {
                        int indexHit;


                        if (sizeBucket.Count > 0) {
                            indexHit = Random.Range(0, sizeBucket.Count);
                            currSize = sizeBucket[indexHit];
                            sizeBucket.RemoveAt(indexHit);
                        }

                        if (typeBucket.Count < 1) { // refill the bucket!
                            typeBucket.Add(Production.Resource.Cement);
                            typeBucket.Add(Production.Resource.Lumber);
                            typeBucket.Add(Production.Resource.Steel);
                        }

                        indexHit = Random.Range(0, typeBucket.Count);
                        currType = typeBucket[indexHit];
                        typeBucket.RemoveAt(indexHit);
                    }
                }

            }
            villageSize.Add(currSize);
            villageType.Add(currType);
        }

        // Debug.Assert (sizeBucket.Count == 0);
        sizeBucket.Add(2);
        sizeBucket.Add(1);
        sizeBucket.Add(1);
        HashSet<int> innerIndices = new HashSet<int> { 27, 28, 29, 20, 21, 14 }; // game jam stratz

        // place outer farms
        for (int i = 0; sizeBucket.Count > 0; i = (i + 1) % halfSize) {
            if (villageSize[i] > 0 || innerIndices.Contains(i))
                continue;
            if (Random.value > 0.875f) {
                int indexHit = Random.Range(0, sizeBucket.Count);
                villageSize[i] = sizeBucket[indexHit];
                sizeBucket.RemoveAt(indexHit);
            }
        }

        // place starting farm: the only big farm as well as the only "inner" farm
        int fIndex = 0;
        if (villageSize[20] > 0) {
            villageSize[28] = 3;
            fIndex = 28;
        } else if (villageSize[28] > 0) {
            villageSize[20] = 3;
            fIndex = 20;
        } else {
            fIndex = (Random.value > 0.5f) ? 20 : 28;
            villageSize[fIndex] = 3;
        }

        // actual instantiation step
        for (int row = 0, i = 0; row < boardY; ++row) {
            for (int col = 0; col < row + 2; ++col, ++i) {
                if (villageSize[i] > 0) {
                    //do math with row/col num to get screen coords
                    Vector3 spawnLoc = Vector3.Reflect(
                        new Vector3(cellSize.x * col + positionWithinCell[i].x - Mathf.Abs(topLeft.x),
                        (cellSize.y * row + positionWithinCell[i].y - Mathf.Abs(topLeft.y)), 0.0f),
                        Vector3.up);
                    //instantiate based on production type
                    GameObject village = Instantiate(villagePrefab, spawnLoc, Quaternion.identity);
                    Production p = village.GetComponent<Production>();
                    SpriteRenderer s = village.GetComponent<SpriteRenderer>();
                    TextMesh t = village.GetComponentInChildren<TextMesh>();
                    p.ownerID = 0;
                    // i need to set: village income, sprite, and text number indicating size
                    t.text = villageSize[i].ToString();
                    switch (villageType[i]) {
                        case Production.Resource.Food:
                            p.income[(int)Production.Resource.Food] = 2 + villageSize[i];
                            s.sprite = farmImg;
                            break;
                        case Production.Resource.Cement:
                            p.income[(int)Production.Resource.Food] = -villageSize[i];
                            p.income[(int)Production.Resource.Cement] = villageSize[i];
                            s.sprite = quarryImg;
                            break;
                        case Production.Resource.Lumber:
                            p.income[(int)Production.Resource.Food] = -villageSize[i];
                            p.income[(int)Production.Resource.Cement] = 1 + villageSize[i];
                            s.sprite = lumberImg;
                            break;
                        case Production.Resource.Steel:
                            p.income[(int)Production.Resource.Food] = -villageSize[i];
                            p.income[(int)Production.Resource.Cement] = 1 + villageSize[i];
                            s.sprite = ironImg;
                            break;
                        default:
                            break;
                    }
                    if(i == fIndex) {
                        makeCap(1, new Vector3(topLeft.x , topLeft.y * -1, 0),  village.transform);
                    }
                }
            }
        }

        // mirror mirror on the wall, which kingdom be the edgiest of them all?
        //positionWithinCell = reversedList (positionWithinCell);
        //villageSize = reversedList (villageSize);
        //villageType = reversedList (villageType);

        // yes i literally copypasted but i gotta change some shit too
        for (int row = 0, i = halfSize - 1; row < boardY; ++row) {
            for (int col = row + 2; col < boardX; ++col, --i) {
                if (villageSize[i] > 0) {
                    //do math with row/col num to get screen coords
                    /*Vector3 spawnLoc = Vector3.Reflect(Vector3.Reflect(Vector3.Reflect(
                        new Vector3(cellSize.x * col + positionWithinCell[i].x - Mathf.Abs(topLeft.x),
                            (cellSize.y * row + positionWithinCell[i].y - Mathf.Abs(topLeft.y)), 0.0f),
                        Vector3.up), Vector3.up), Vector3.up);*/

                    Vector3 spawnLoc = Vector3.Reflect(
                        new Vector3(cellSize.x * col - positionWithinCell[i].x - Mathf.Abs(topLeft.x),
                            (cellSize.y * row - positionWithinCell[i].y - Mathf.Abs(topLeft.y)), 0.0f),
                        Vector3.up);
                    //instantiate based on production type
                    GameObject village = Instantiate(villagePrefab, spawnLoc, Quaternion.identity);
                    Production p = village.GetComponent<Production>();
                    SpriteRenderer s = village.GetComponent<SpriteRenderer>();
                    TextMesh t = village.GetComponentInChildren<TextMesh>();
                    p.ownerID = 0;
                    // i need to set: village income, sprite, and text number indicating size
                    t.text = villageSize[i].ToString();
                    switch (villageType[i]) {
                        case Production.Resource.Food:
                            p.income[(int)Production.Resource.Food] = 2 + villageSize[i];
                            s.sprite = farmImg;
                            break;
                        case Production.Resource.Cement:
                            p.income[(int)Production.Resource.Food] = -villageSize[i];
                            p.income[(int)Production.Resource.Cement] = villageSize[i];
                            s.sprite = quarryImg;
                            break;
                        case Production.Resource.Lumber:
                            p.income[(int)Production.Resource.Food] = -villageSize[i];
                            p.income[(int)Production.Resource.Cement] = 1 + villageSize[i];
                            s.sprite = lumberImg;
                            break;
                        case Production.Resource.Steel:
                            p.income[(int)Production.Resource.Food] = -villageSize[i];
                            p.income[(int)Production.Resource.Cement] = 1 + villageSize[i];
                            s.sprite = ironImg;
                            break;
                        default:
                            break;
                    }
                    if(i == fIndex) {
                        makeCap(2, new Vector3((topLeft.x + 3) * -1, (topLeft.y + 3), 0),  village.transform);
                    }
                }
            }
        }
    }

    private void makeCap(int player, Vector3 pos, Transform farm) {
        /*creates a captial, at the given position, for the given player, with the given starting farm*/
        GameObject cap = GameObject.Instantiate(capitalPrefab);
        cap.transform.position = pos;
        PlayerInfo pi = cap.GetComponent<PlayerInfo>();
        pi.playerID = player;
        Production p = cap.GetComponent<Production>();
        p.income[(int)Production.Resource.Food] = -1;
        p.income[(int)Production.Resource.Cement] = 7;
        p.income[(int)Production.Resource.Steel] = 5;
        p.income[(int)Production.Resource.Lumber] = 5;
        p.ownerID = player;
        cap.GetComponent<Spawner>().SpawnFirstRoad(Spawner.Objs.ROAD, cap.transform, farm, player);
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