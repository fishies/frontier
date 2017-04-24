using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiderBehavior : MonoBehaviour {
    Animator anim;

    public bool selected = false;
    public float movementDuration;
    public int attackValue;

    private float timePassed;
    private bool running = false;
    private bool attacking = false;
    private GameObject enemySelected;

    private Vector3 target;
    private Vector3 originalPos;
    public float raiderRange;
    RaycastHit2D hit;
    LineRenderer line;
    public float thetaScale;

    public LayerMask layersToCheck;

    public bool hasMoved = false;

    private InputManager inputManager;
    private GameManager gameManager;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();

        inputManager = GameObject.FindObjectOfType<InputManager>();
        gameManager = GameObject.FindObjectOfType<GameManager>();

        line = GetComponentInChildren<LineRenderer>();
        line.useWorldSpace = false;
        line.material = new Material(Shader.Find("Particles/Additive"));
        line.startColor = new Color(1, 0, 0);
        line.endColor = new Color(1, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
		if (selected) {
            GetComponent<SpriteRenderer>().color = Color.red;
            CreateCircle();
            RaiderOptions();
        }
        else if (!selected && !hasMoved) {
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
        if(InputManager.Modes.SELECT == inputManager.Mode && GetComponent<Production>().ownerID == gameManager.currentPlayer && !running && !hasMoved)
        {
            selected = !selected;
        }
    }

    public void RaiderOptions()
    {
        if(!running && Input.GetMouseButton(1))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = 0;

            hit = Physics2D.Raycast(target, Vector2.zero, Mathf.Infinity, layersToCheck);

            if (hit && Vector3.Distance(target, transform.GetChild(0).position) <= raiderRange)
            {
                running = true;
                attacking = true;
                enemySelected = hit.collider.gameObject;
                originalPos = transform.position;

            } else if (Vector3.Distance(target, transform.GetChild(0).position) <= raiderRange)
            {
                running = true;
                originalPos = transform.position;
            }
        }

        if (running)
        {
            timePassed += Time.deltaTime;

            transform.position = Vector3.Lerp(originalPos, target, timePassed / movementDuration);

            if (timePassed > movementDuration)
            {
                if (attacking && GetComponent<Production>().ownerID != enemySelected.GetComponent<Production>().ownerID)
                {
                    enemySelected.GetComponent<Damagable>().takeDamage(attackValue);
                }
                timePassed = 0f;
                running = false;
                attacking = false;
                selected = false;
                hasMoved = true;
                GetComponent<SpriteRenderer>().color = Color.gray;
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
            line.SetPosition(i, new Vector3(Mathf.Cos(theta) * raiderRange, Mathf.Sin(theta) * raiderRange, 0));
        }
    }
}
