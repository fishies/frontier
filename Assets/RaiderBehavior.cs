using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiderBehavior : MonoBehaviour {

    public bool selected = false;
    public float movementDuration;

    private float timePassed;
    private bool running = false;

    private Vector3 target;
    private Vector3 originalPos;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		if (selected){
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
            running = true;
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = 0;
            originalPos = transform.position;
        }

        if (running)
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
