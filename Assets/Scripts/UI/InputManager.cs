using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

/* A script to act as a common interface for all I/O devices interacting with the game
 */


public class InputManager : MonoBehaviour {

    public enum Modes { SELECT, PLACE_ROAD, PLACE_RAIDER, PLACE_TOWER };

    private Modes mode; //stores the current input mode

    private RoadPlacer rop;
    private RaiderPlacer rap;
    private TowerPlacer tp;

    void Start() {
        rop = GetComponentInChildren<RoadPlacer>();
        rap = GetComponentInChildren<RaiderPlacer>();
        tp = GetComponentInChildren<TowerPlacer>();
        update_mode();
    }

    public Modes Mode {
        get {
            return mode;
        }
        set {   
            Modes old_mode = mode;
            mode = value;
            update_mode();
        }
    }

    private void update_mode() {
        /* To be called when the mode changes,
         * applies associated changes
         */
        rop.gameObject.SetActive(mode == Modes.PLACE_ROAD);
        rap.gameObject.SetActive(mode == Modes.PLACE_RAIDER);
        tp.gameObject.SetActive(mode == Modes.PLACE_TOWER);
    } 

    public void removeSelection()
    {
        foreach (TowerBehavior tower in GameObject.FindObjectsOfType<TowerBehavior>())
        {
            tower.selected = false;
        }

        foreach (RaiderBehavior raider in GameObject.FindObjectsOfType<RaiderBehavior>())
        {
            raider.selected = false;
        }
    }

    public void NextTurn() {
        /*To be called to end the turn*/
        FindObjectOfType<GameManager>().NextTurn();
    }

    public void Pan(int x_dir, int y_dir) {
        /*be be called to pan the camera*/
    }

}
