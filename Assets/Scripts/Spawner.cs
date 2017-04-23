using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnException : System.Exception { }

public class Spawner : MonoBehaviour {

    /* A script for spawning roads, towers and raiders */

    public enum Objs { ROAD, RAIDER, TOWER };

    //NOTE: THE BELLOW MUST BE SET IN THE UNITY EDITOR! ON PAIN OF CRASHING!
    public GameObject road;
    public GameObject raider;
    public GameObject tower;

    private PlayerInfo pi;

    private static Production.Resource[] cost_types;
    private static int[] costs;

    void Start() {
        pi = GetComponent<PlayerInfo>();
        //init cost types
        cost_types = new Production.Resource[System.Enum.GetNames(typeof(Objs)).Length];
        cost_types[(int)Objs.ROAD] = Production.Resource.Cement;
        cost_types[(int)Objs.RAIDER] = Production.Resource.Steel;
        cost_types[(int)Objs.TOWER] = Production.Resource.Lumber;
        //init cost values
        costs = new int[System.Enum.GetNames(typeof(Objs)).Length];
        costs[(int)Objs.ROAD] = 5;
        costs[(int)Objs.RAIDER] = 7;
        costs[(int)Objs.TOWER] = 10;
    }

    public int PlayerID {
        get {
            return pi.playerID;
        }
    }

    private bool can_pay(Production.Resource r, int i) {
        return pi.stockpile[(int)r] >= i;
    }

    private void pay(Production.Resource r, int i) {
        pi.stockpile[(int)r] -= i;
    }

    public bool Spawn(Objs type, Vector3 pos) {
        /* Spawns a raider or tower;
         * Use other overload for road;
         */
        pos.z = 0;

        if(type == Objs.ROAD) {
            throw new SpawnException();
        }
        if(can_pay(cost_types[(int)type], costs[(int)type])){
            GameObject go = null;
            if(type == Objs.RAIDER) {
                go = Instantiate(raider, pos, new Quaternion(0, 0, 0, 0));
				go.GetComponent<Production>().income[(int) Production.Resource.Food] -= 1; //LOL MAGIC NUMBER :^)
            }else if(type == Objs.TOWER) {
                go = Instantiate(tower, pos, new Quaternion(0, 0, 0, 0));
            }
            pay(cost_types[(int)type], costs[(int)type]);
            go.GetComponent<Production>().ownerID = PlayerID;
            //go.transform.position = pos;
            return true;
        }else {
            return false;
        }
    }

    public static int GetRoadPrice(Transform t_one, Transform t_two, Objs type = Objs.ROAD) {
        /* returns the prices of a road between the two given transforms.
         */
        return (int)(Vector2.Distance(t_one.position, t_two.position) * costs[(int)type]);
    }

    public bool Spawn (Objs type, Transform t_one, Transform t_two) {
        /* handles the unique obstancles of spawning a road
         */ 
        if(type != Objs.ROAD) {
            throw new SpawnException();
        }
        int price = GetRoadPrice(t_one, t_two);
        //conditions for being allowed to build a road:
        if (can_pay(cost_types[(int)type], price)) {
            pay(cost_types[(int)type], price);
            GameObject go = Instantiate(road);
            go.GetComponent<RoadHandler>().setEndpoints(t_one, t_two);
            t_one.GetComponent<Production>().ownerID = PlayerID;
            t_two.GetComponent<Production>().ownerID = PlayerID;
            return true;
        } else {
            return false;
        }
    }

}
