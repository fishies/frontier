using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour {
    InputManager im;
    GameObject[] itemArray;

    void Start() {
        im = FindObjectOfType<InputManager>();
        itemArray = new GameObject[4] {GameObject.Find("Cursor"),
                                                GameObject.Find("Road"),
                                                GameObject.Find("Raider"),
                                                GameObject.Find("Tower")};
    }

    public void ChangeActiveItem(int currentItem) {
        Vector3 currentItemPosition = itemArray[currentItem].transform.position;
        currentItemPosition.x += 15;
        currentItemPosition.y -= 20;
        gameObject.transform.position = currentItemPosition;
    }
}
