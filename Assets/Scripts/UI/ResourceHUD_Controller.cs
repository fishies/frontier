using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceHUD_Controller : MonoBehaviour {

    public int Player;
    private PlayerInfo pi;
    private Text[] disps;

    void Start() {
        //init the PlayerInfo
        foreach (PlayerInfo p in FindObjectsOfType<PlayerInfo>()) {
            if (p.playerID == Player) {
                pi = p;
                break;
            }
        }
        //init the text displays
        disps = new Text[4];
        initText(Production.Resource.Food, "Food");
        initText(Production.Resource.Steel, "Iron");
        initText(Production.Resource.Cement, "Stone");
        initText(Production.Resource.Lumber, "Lumber");
    }

    private void initText(Production.Resource r, string s) {
        disps[(int)r] = transform.FindChild(s).GetComponentInChildren<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        //update the texts to reflect the stockpile contents;
	    for(int i = 0; i < disps.Length; i++) {
            disps[i].text = pi.stockpile[i].ToString();
        }
	}
}
