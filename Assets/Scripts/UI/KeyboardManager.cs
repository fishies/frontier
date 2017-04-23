using UnityEngine;

public class KeyboardManager : MonoBehaviour {

    private InputManager im; //store the associated input manager;

    void Start() {
        im = GetComponent<InputManager>();
    }

    void Update() {

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Alpha1)) {
            im.Mode = InputManager.Modes.SELECT;
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            im.Mode = InputManager.Modes.PLACE_ROAD;
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            im.Mode = InputManager.Modes.PLACE_RAIDER;
        } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            im.Mode = InputManager.Modes.PLACE_TOWER;
        } else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)) {
            im.NextTurn();
        }
        //TODO: insert WASD pan, change turn

    }

}
