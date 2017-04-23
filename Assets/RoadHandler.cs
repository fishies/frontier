﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadHandler : MonoBehaviour {
    public GameObject village1;
    public GameObject village2;

	public void setEndpoints(Transform point1, Transform point2) {
        Vector3[] arg = new Vector3[] { point1.position, point2.position };
        gameObject.GetComponent<LineRenderer>().SetPositions(arg);
        gameObject.transform.position = new Vector3((point1.position.x + point2.position.x) / 2,
                                                    (point1.position.y + point2.position.y) / 2,
                                                    0);

        float angle = Mathf.Atan(Mathf.Abs(point2.position.y - point1.position.y) /
                                                                 Mathf.Abs(point2.position.x - point1.position.x)) * Mathf.Rad2Deg;
        Quaternion currentRotation = transform.rotation;
        currentRotation = currentRotation * Quaternion.AngleAxis(angle, Vector3.forward);
        gameObject.transform.rotation = currentRotation;
    }

    public GameObject[] getEndpoints() {
        return (new GameObject[] {village1, village2});
    }
}
