using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class matchPosition : MonoBehaviour {

    public Transform matchObject;
    public bool matchY;
    public bool moveable;
    public float speed;
    Vector3 posToMatch;
    Vector3 offsetPos = new Vector3(0, 0, 0);

	// Update is called once per frame
	void Update () {
		if(matchY == true)
        {
           transform.position = matchObject.transform.position;
        }
        else
        {
            if (moveable)
            {
                posToMatch = new Vector3(matchObject.transform.position.x, transform.position.y, matchObject.transform.position.z);
                
                if (Input.GetKey(KeyCode.W))
                {
                    offsetPos.z += speed * Time.deltaTime * TimeManager.hardTimeSpeedModifier * 100;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    offsetPos.z -= speed * Time.deltaTime * TimeManager.hardTimeSpeedModifier * 100;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    offsetPos.x -= speed * Time.deltaTime * TimeManager.hardTimeSpeedModifier * 100;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    offsetPos.x += speed * Time.deltaTime * TimeManager.hardTimeSpeedModifier * 100;
                }

                posToMatch = posToMatch + offsetPos;
                transform.position = posToMatch;
            }
            else
            {
                transform.position = new Vector3(matchObject.transform.position.x, transform.position.y, matchObject.transform.position.z);
            }
        }
	}
}
