using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public enum State { MAIN, CONTROLS }

    private GameObject menu;
    private GameObject controls;

    void Start() {
        //plug in components
        //NOTE: because uses Find, do not rename game objects
        menu = GameObject.Find("MainMenu");
        controls = GameObject.Find("HowToPlay");
        GotoState(State.MAIN, false);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            GotoState(State.MAIN);
        }
    }

    public void GotoState(int i) {
        //wrapper that allows GotoState to be called by Unity
        GotoState((State)i);
    }

    public void GotoState(State state, bool boop = true) {
        //change settings to display the given state
        menu.SetActive(state == State.MAIN);
        controls.SetActive(state == State.CONTROLS);
    }

    public void BeginGame() {
        SceneManager.LoadScene("Character Select");
    }

    public void ExitGame() {
        Application.Quit();
    }
    

}
