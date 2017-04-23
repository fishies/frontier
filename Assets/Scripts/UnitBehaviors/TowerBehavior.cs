using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehavior : MonoBehaviour {
    public bool selected = false;

    public float towerRange;
    public int attackValue;
    
    RaycastHit2D hit;
    LineRenderer line;
    public float thetaScale;

    private GameObject enemySelected; // Need to also check if gameobject actually belongs to the enemy

    public LayerMask layersToCheck;

    private bool hasAttacked = false;

    // Use this for initialization
    void Start () {
        line = GetComponentInChildren<LineRenderer>();
        line.useWorldSpace = false;
        line.material = new Material(Shader.Find("Particles/Additive"));
        line.startColor = new Color(1, 0, 0);
        line.endColor = new Color(1, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
        if (selected)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            CreateCircle();
            TowerOptions();
        }
        else if (!selected && !hasAttacked)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            line.positionCount = 0;
        }
        else
        {
            line.positionCount = 0;
        }
	}

    public void OnMouseDown()
    {
        if (!hasAttacked)
        {
            selected = !selected;
        }
    }

    public void TowerOptions()
    {
        if (Input.GetMouseButtonDown(1))
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, layersToCheck);

            if (hit)
            {
                enemySelected = hit.collider.gameObject;

                if (GetComponent<Production>().ownerID != enemySelected.GetComponent<Production>().ownerID && Vector3.Distance(enemySelected.transform.GetChild(0).position, transform.GetChild(0).position) <= towerRange)
                {
                    enemySelected.GetComponent<Damagable>().takeDamage(attackValue);
                    selected = false;
                    hasAttacked = true;
                    GetComponent<SpriteRenderer>().color = Color.gray;
                }
            }
        }
    }

    public void CreateCircle()
    {
        line.positionCount = (int)((2.0f * Mathf.PI) / thetaScale) + 1;
        float theta = 0f;
        float deltaTheta = 2.0f * Mathf.PI * thetaScale;

        for (int i = 0; i < (int)((2.0f * Mathf.PI) / thetaScale) + 1; i++)
        {
            theta += deltaTheta;
            line.SetPosition(i, new Vector3(Mathf.Cos(theta) * towerRange, Mathf.Sin(theta) * towerRange, 0));
        }
    }
}
