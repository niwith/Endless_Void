using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipMovement : MonoBehaviour {

    public List<Vector3> navigationPoint = new List<Vector3>();

    float maxForwardSpeed = 2;
    float currentForwardSpeed;
    float forwardAcceleration = 0.1f;
    public float thrust = 0;

    float turnSpeed;

    public ShipSystems shipSystemsScript;

    // Use this for initialization
    void Start () {
        shipSystemsScript = GetComponent<ShipSystems>();
    }
	
	// Update is called once per frame
	void Update () {
        turnSpeed = shipSystemsScript.get_currentTurnSpeed();
        forwardAcceleration = shipSystemsScript.get_currentAcceleration();
        maxForwardSpeed = shipSystemsScript.get_currentMaxSpeed();
        if (navigationPoint.Count > 0)
        {
            navigateTo();
        }
        shipMovement();
    }

    void navigateTo()
    {
        //if engines and pilot
        if (Vector3.Distance(navigationPoint[0], transform.position) < 50)
        {
            navigationPoint.Remove(navigationPoint[0]);
        }
        else
        {
            Vector3 targetDir = navigationPoint[0] - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, turnSpeed * Time.deltaTime * TimeManager.softTimeSpeedModifier, 0.0F);
            transform.rotation = Quaternion.LookRotation(newDir);
        }
    }

    void shipMovement()
    {
        if (currentForwardSpeed < thrust * maxForwardSpeed)
        {
            currentForwardSpeed += forwardAcceleration * Time.deltaTime * TimeManager.softTimeSpeedModifier;
            if (currentForwardSpeed > maxForwardSpeed * thrust)
            {
                currentForwardSpeed = maxForwardSpeed * thrust;
            }
        }
        else if (currentForwardSpeed > thrust * maxForwardSpeed)
        {
            currentForwardSpeed -= forwardAcceleration * Time.deltaTime * TimeManager.softTimeSpeedModifier;
            if (currentForwardSpeed < 0)
            {
                currentForwardSpeed = 0;
            }
        }

        if (navigationPoint.Count > 0)
        {
            navigateTo();
        }

        transform.Translate(Vector3.forward * currentForwardSpeed * Time.deltaTime * TimeManager.softTimeSpeedModifier * 100);
    }
}
