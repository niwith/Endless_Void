using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class External_Wall : MonoBehaviour {

    public int hitPoints = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (hitPoints == 0)
        {
            Destroy(gameObject);
        }
	}
}
