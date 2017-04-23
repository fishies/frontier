using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD_Controller : MonoBehaviour {

    InputManager im;

    void Start() {
        im = FindObjectOfType<InputManager>();
    }

    public void SetMode(int mode) {
        //need to take int and cast so works with Unity
        im.Mode = (InputManager.Modes)mode;
    }

    public void NextTurn() {
        im.NextTurn();
    }

}
