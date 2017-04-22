using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitBoard : MonoBehaviour {
     //Generic gameobject prefabs that can be modified.
     public int Num_Players;
     public GameObject Background_Prefab,
                       City_Prefab,
                       Capital_Prefab;
     private GameObject Background_Instance;
     void Awake() {
          Background_Instance = GameObject.Instantiate(Background_Prefab, transform.localPosition, transform.localRotation);
          Background_Instance.transform.parent = transform;
          CityInit();
          CapitalInit();
     }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

     Vector2 getRandomCityLocation() {
          return new Vector2(0.0f, 0.0f); //Dummy value
     }

     void CityInit() {

     }

     void CapitalInit() {
          List<GameObject> capitals = makeCapitals();
          List<Vector2> capital_locations = getCapitalLocations(capitals[0]);
          for(int c = 0; c < Num_Players; ++c) {
               capitals[c].transform.position = capital_locations[c];
          }
     }

     List<GameObject> makeCapitals () {
          List<GameObject> capitals = new List<GameObject>();
          for (int c = 0; c < Num_Players; ++c) {
               capitals.Add(GameObject.Instantiate(Capital_Prefab, Background_Instance.transform.position, Background_Instance.transform.rotation));
               capitals[c].transform.parent = Background_Instance.transform;
          }
          return capitals;
     }

     List<Vector2> getCapitalLocations(GameObject capital_instance) {
          List<Vector2> capital_locations = new List<Vector2>();
          Collider backgd_collider = Background_Instance.GetComponent<Collider>();
          Vector2 boardCenter = backgd_collider.bounds.center,
                  boardExtents = backgd_collider.bounds.extents,
                  capitalExtents = capital_instance.GetComponent<Collider>().bounds.extents;

                
          capital_locations.Add(new Vector2(boardCenter.x + boardExtents.x - capitalExtents.x, boardCenter.y + boardExtents.y - capitalExtents.y));
          capital_locations.Add(new Vector2(boardCenter.x - boardExtents.x + capitalExtents.x, boardCenter.y - boardExtents.y + capitalExtents.y));
          return capital_locations;
     }
}
