using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadHandler : MonoBehaviour {
    public GameObject point1;
    public GameObject point2;

	public void setEndpoints(Transform point1, Transform point2) {
        Vector3[] arg = new Vector3[] { point1.position, point2.position };
        gameObject.GetComponent<LineRenderer>().SetPositions(arg);
    }

    public GameObject[] getEndpoints() {
        return (new GameObject[] {point1, point2});
    }
}
