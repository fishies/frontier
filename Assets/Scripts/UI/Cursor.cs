using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour {
    InputManager im;
    GameObject[] itemArray;

    void Start() {
        im = FindObjectOfType<InputManager>();
        itemArray = new GameObject[4] {GameObject.Find("Cursor"),
                                                GameObject.Find("RoadIcon"),
                                                GameObject.Find("RaiderIcon"),
                                                GameObject.Find("TowerIcon")};
    }

    public void ChangeActiveItem(int currentItem) {
        Vector3 currentItemPosition = itemArray[currentItem].transform.position;
        currentItemPosition.x += 15;
        currentItemPosition.y -= 20;
        gameObject.transform.position = currentItemPosition;
    }

    void Update() {
        if(Input.GetKeyDown("1") || Input.GetKeyDown("escape")) {
            ChangeActiveItem(0);
        }
        else if(Input.GetKeyDown("2")) {
            ChangeActiveItem(1);
        }
        else if(Input.GetKeyDown("3")) {
            ChangeActiveItem(2);
        }
        else if(Input.GetKeyDown("4")) {
            ChangeActiveItem(3);
        }
    }
}
