using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiderBehavior : MonoBehaviour {

    public bool selected = false;
    public float movementDuration;

    public float timePassed;
    public bool running = false; //this flag "running" refers to movement?

    public Vector3 target;
    public Vector3 originalPos;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		if(selected){ // ??????
            RaiderMove();
        }
	}

    public void OnMouseDown()
    {
        selected = !selected;
    }

    public void RaiderMove()
    {
        if(Input.GetMouseButton(1) && !running) // why have to right-click first??
        {
            running = true;
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = 0;
            originalPos = transform.position;
        }

        else if (running)
        {
            timePassed += Time.deltaTime;

            transform.position = Vector3.Lerp(originalPos, target, timePassed / movementDuration);

            if (timePassed > movementDuration)
            {
                timePassed = 0;
                running = false;
                selected = false;
            }
        }
    }
}
