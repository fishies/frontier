using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehavior : MonoBehaviour {
    public bool selected = false;

    public float towerRange;
    
    RaycastHit2D hit;
    LineRenderer line;
    public float thetaScale;


    private GameObject enemySelected; // Need to also check if gameobject actually belongs to the enemy

    // Use this for initialization
    void Start () {
        line = GetComponent<LineRenderer>();
        line.positionCount = (int) ((2.0f * Mathf.PI) / thetaScale) + 1;
        line.useWorldSpace = false;
        line.material = new Material(Shader.Find("Particles/Additive"));
        line.startColor = new Color(1, 0, 0);
        line.endColor = new Color(1, 0, 0);
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
                    Debug.Log("Tower Attack");
                    selected = false;
                    Destroy(line);
                }
            }
        }
    }

    public void CreateCircle()
    {
        float theta = 0f;
        float deltaTheta = 2.0f * Mathf.PI * thetaScale;

        for (int i = 0; i < (int)((2.0f * Mathf.PI) / thetaScale) + 1; i++)
        {
            theta += deltaTheta;
            line.SetPosition(i, new Vector3(Mathf.Cos(theta) * towerRange, Mathf.Sin(theta) * towerRange, 0));
        }
    }
}
