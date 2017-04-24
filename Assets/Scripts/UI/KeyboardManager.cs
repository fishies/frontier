﻿using UnityEngine;

public class KeyboardManager : MonoBehaviour {

    private InputManager im; //store the associated input manager;

    void Start() {
        im = GetComponent<InputManager>();
    }

    void Update() {

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            im.Mode = InputManager.Modes.SELECT;
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            im.removeSelection();
            im.Mode = InputManager.Modes.PLACE_ROAD;
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            im.removeSelection();
            im.Mode = InputManager.Modes.PLACE_RAIDER;
        } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            im.removeSelection();
            im.Mode = InputManager.Modes.PLACE_TOWER;
        } else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) {
            im.removeSelection();
            im.NextTurn();
        } else if (Input.GetKeyDown(KeyCode.Escape)) {
            im.removeSelection();
            im.Mode = InputManager.Modes.SELECT;
        }
        //panning 
        if (Input.GetKey(KeyCode.W)) {
            im.Pan(0, 1);
        }
        if (Input.GetKey(KeyCode.A)) {
            im.Pan(-1, 0);
        }
        if (Input.GetKey(KeyCode.S)) {
            im.Pan(0, -1);
        }
        if (Input.GetKey(KeyCode.D)) {
            im.Pan(1, 0);
        }

    }

}
