using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Parent class for all equipment pieces
public class Equipment {
    // Unique ID for all equips of this type for data storage
    public int equipmentID; 
    // My model and instance in world
    public Transform model;
    public Transform instanceInWorld;

    // Values
    public Transform hardPoint;
    public string name;
    public int size;
    public List<string> acceptedHardPointTypes = new List<string>();
    public int hitPointsMax;
    public int hitPointsCurrent;

    // Apply damage
    public void reduceHitPoints(int reducePointsBy)
    {
        hitPointsCurrent = hitPointsCurrent - reducePointsBy;
        if(hitPointsCurrent < 0)
        {
            hitPointsCurrent = 0;
        }
    }

    // Apply repairing
    public void addHitPoints(int increasePointsBy)
    {
        hitPointsCurrent = hitPointsCurrent + increasePointsBy;
        if(hitPointsCurrent > hitPointsMax)
        {
            hitPointsCurrent = hitPointsMax;
        }
    }

    // Get current hitpoints
    public int get_hitPointsCurrent()
    {
        return hitPointsCurrent;
    }

    // Set current hit points
    public void set_hitPointsCurrent(int setTo)
    {
        hitPointsCurrent = setTo;
        if (hitPointsCurrent > hitPointsMax)
        {
            hitPointsCurrent = hitPointsMax;
        }
        if (hitPointsCurrent < 0)
        {
            hitPointsCurrent = 0;
        }
    }

    // Try to set the hard point, if it cant return false
    public bool set_hardPoint(Transform pointToSet)
    {
        // Check the size and type of the hard point its going into
        if (pointToSet.GetComponent<HardPoint>().hardPointSize <= size && Utilities.stringInList(acceptedHardPointTypes, pointToSet.GetComponent<HardPoint>().hardPointType))
        {
            hardPoint = pointToSet;
            if (model == null)
            {
                DictionaryStorage storage = GameObject.Find("GameManager").GetComponent<DictionaryStorage>();
                model = storage.GetPrefab(equipmentID);
            }
            if(instanceInWorld == null)
            {
                instanceInWorld = Object.Instantiate(model);
            }
            instanceInWorld.transform.position = hardPoint.transform.position;
            instanceInWorld.transform.SetParent(hardPoint);
            return true;
        }
        return false; // Couldn't fit into the hard point requested
    }

    public bool unset_hardPoint()
    {
        hardPoint = null;
        return true;
    }

    public Transform get_hardPoint()
    {
        return hardPoint;
    }
}

public class Generator : Equipment
{
    public int powerOutputMax;
    public int powerOutputMod;

    public int get_powerOutput()
    {
        return powerOutputMax * (1 - powerOutputMod);
    }
}

// Parent class for all non generator components
public class PowerUser : Equipment
{
    public int powerUsageMaximum;
    public int powerUsageMinimum = 0; // Below this value it will not function, defults to 0
    public int powerUsageCurrent;

    public float modifyByCurrentPower(float input)
    {
        float powerModifier = (float)powerUsageCurrent / (float)powerUsageMaximum;
        return input * powerModifier;
    }

    public int get_PowerUsageCurrent()
    {
        return powerUsageCurrent;
    }

    public void set_PowerUsageCurrent(int setTo)
    {
        powerUsageCurrent = setTo;
        if(powerUsageCurrent > powerUsageMaximum)
        {
            Debug.Log("POWER USAGE OUT OF RANGE");
        }
    }
}

public class Shield : PowerUser
{

}

public class Thruster : PowerUser
{
    public float maxSpeedAtMaxPower;
    public float accelerationAtMaxPower;
    public float turnSpeedAtMaxPower;

    public float get_maxSpeedCurrent()
    {
        return modifyByCurrentPower(maxSpeedAtMaxPower);
    }

    public float get_accelerationCurrent()
    {
        return modifyByCurrentPower(accelerationAtMaxPower);
    }

    public float get_turnSpeedCurrent()
    {
        return modifyByCurrentPower(turnSpeedAtMaxPower);
    }
}

public class Utility : PowerUser
{

}

public class Cloak : Utility
{

}

public class Scanner : Utility
{

}

// Parent class for all weapons
public class Weapon : PowerUser
{
    Transform targetMarkerPrefab;

    Transform targetObject;
    float targetOffset;
    float targetRotation;

    Transform targetLocationMarker;
    Vector3 targetSpacePosition;

    public bool hasTarget;

    public void set_targetObject(Transform target, Vector2 hitLocation, bool showMarker = false)
    {
        targetObject = target;
        targetOffset = Vector2.Distance(target.position, hitLocation);
        Vector2 localPosition = (Vector2)target.position - hitLocation;
        float hitRotation = Mathf.Sin(localPosition.x/localPosition.y);
        // Q1, Q2
        if(localPosition.y > 0)
        {
            // Q1
            if(localPosition.x > 0)
            {

            }
            // Q2
            else
            {

            }
        }
        else
        {
            // Q4
            if(localPosition.x > 0)
            {

            }
            // Q3
            else
            {

            }
        }
    }

    public void set_targetSpace(Vector3 position, bool showMarker)
    {
        hasTarget = true;
        targetSpacePosition = position;
        if(targetLocationMarker != null)
        {
            Object.Destroy(targetLocationMarker.gameObject);
        }
        if(showMarker)
        {
            DictionaryStorage IconLibaray = GameObject.Find("GameManager").GetComponent<DictionaryStorage>();
            targetLocationMarker = Object.Instantiate(IconLibaray.GetMarker("Target"), position, Quaternion.Euler(0, 0, 0));
            targetLocationMarker.transform.SetParent(GameObject.Find("UI_Canvas").transform);
            targetLocationMarker.transform.SetAsFirstSibling();
        }
    }

    public void UpdateComponent()
    {
        if (targetLocationMarker != null)
        {
            if (targetObject != null)
            {

            }
            else
            {              
                targetLocationMarker.transform.position = Utilities.getScreenPosition(targetSpacePosition);
            }
        }
        else
        {
            
        }
        
        //else if()
    }
}

// Parent to the energy (non-ammo using) weapons
public class EnergyWeapon : Weapon
{

}

// Parent class to ammo using weapons
public class MassWeapon : Weapon
{

}

// Subweapon, instant hit, constant fire, high energy usage, no ammo usage
public class BeamWeapon : EnergyWeapon
{

}

// Subweapon, projectile, timed reload, very low energy usage, ammo usage
public class MassProjectileWeapon : MassWeapon
{

}

// Subweapon, projectile, timed reload, high energy usage, no ammo usage
public class EnergyProjectileWeapon : Weapon
{

}

// Subweapon, projectile, timed reload, very low energy usage, ammo usage
public class MissileWeapon : Weapon
{

}