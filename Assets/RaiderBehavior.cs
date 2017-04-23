using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiderBehavior : MonoBehaviour {

    public bool selected = false;
    public float movementDuration;

    private float timePassed;
    private bool running = false;
    private bool attacking = false;

    private Vector3 target;
    private Vector3 originalPos;

    public float raiderRange;
    RaycastHit2D hit;
    LineRenderer line;
    public float thetaScale;

    // Use this for initialization
    void Start () {
        line = GetComponent<LineRenderer>();
        line.positionCount = (int)((2.0f * Mathf.PI) / thetaScale) + 1;
        line.useWorldSpace = false;
        line.material = new Material(Shader.Find("Particles/Additive"));
        line.startColor = new Color(1, 0, 0);
        line.endColor = new Color(1, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
		if (selected){
            CreateCircle();
            RaiderOptions();
        }
	}

    public void OnMouseDown()
    {
        if(!running)
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

            hit = Physics2D.Raycast(target, Vector2.zero);

            if (hit)
            {
                running = true;
                attacking = true;
                originalPos = transform.position;

            } else
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
                if (attacking)
                {
                    Debug.Log("Raider Attack");
                }
                timePassed = 0;
                running = false;
                selected = false;
                Destroy(line);
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
            line.SetPosition(i, new Vector3(Mathf.Cos(theta) * raiderRange, Mathf.Sin(theta) * raiderRange, 0));
        }
    }
}
