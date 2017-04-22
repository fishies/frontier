using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehavior : MonoBehaviour {
    public bool selected = false;

    public float towerRange;
    
    RaycastHit2D hit;
    LineRenderer line;
    public int segments;
    public float setAngle;


    private GameObject enemySelected; // Need to also check if gameobject actually belongs to the enemy

    // Use this for initialization
    void Start () {
        line = GetComponent<LineRenderer>();
        line.positionCount = (segments + 1);
        line.useWorldSpace = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (selected)
        {
            CreateCircle();
            TowerOptions();
        }
	}

    public void OnMouseDown()
    {
        selected = !selected;
    }

    public void TowerOptions()
    {
        if (Input.GetMouseButtonDown(1))
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit)
            {
                enemySelected = hit.collider.gameObject;

                Debug.Log(Vector3.Distance(enemySelected.transform.position, transform.position));
                if (enemySelected.tag == "Raider" && Vector3.Distance(enemySelected.transform.position, transform.position) <= towerRange)
                {
                    Debug.Log("Attack");
                    selected = false;
                }
            }
        }
    }

    public void CreateCircle()
    {
        Debug.Log("works");
        float angle = setAngle;

        for (int i = 0; i < (segments + 1); i++)
        {
            line.SetPosition(i, new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle) * towerRange, Mathf.Cos(Mathf.Deg2Rad * angle) * towerRange, 0));

            angle += (360f / segments);
        }
    }
}
