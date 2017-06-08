using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour {

    public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey("w"))
        {
            transform.Translate(Vector3.forward * speed);
        }
        if (Input.GetKey("a"))
        {
            transform.Translate(-Vector3.right * speed);
        }
        if (Input.GetKey("s"))
        {
            transform.Translate(-Vector3.forward * speed);
        }
        if (Input.GetKey("d"))
        {
            transform.Translate(Vector3.right * speed);
        }
    }
}
