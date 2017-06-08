using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalaxCameraMirror : MonoBehaviour {

    public Transform mainCamera;
    public float speedMultiplier;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 currentPosition = transform.position;
        currentPosition.x = mainCamera.position.x / speedMultiplier;
        currentPosition.y = mainCamera.position.y / speedMultiplier;
        currentPosition.z = mainCamera.position.z / speedMultiplier;
        transform.position = currentPosition;
    }
}
