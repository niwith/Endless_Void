using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponGroupManager : MonoBehaviour {

    public List<Weapon> groupWeapons = new List<Weapon>();

    public Toggle pointDefense;
    public Toggle fireMode;

    public Transform buttonImage;
    public Color[] fireStateColors;

    PlayerInteraction playerInteration;

    void Start()
    {
        playerInteration = GameObject.Find("PlayerManager").GetComponent<PlayerInteraction>();
    }

    void Update()
    {
        // Only check the weapons status if there are acually weapons in the group
        if (groupWeapons.Count > 0)
        {
            // Check if each weapon in the group has a target
            // Set to the active colour if all weapons do
            bool allHaveTarget = true;
            foreach (Weapon weapon in groupWeapons)
            {
                if (!weapon.hasTarget)
                {
                    allHaveTarget = false;
                    break; // Break the loop if a weapon doesnt have a target
                }
            }
            if (allHaveTarget)
            {
                buttonImage.GetComponent<Image>().color = fireStateColors[1];
            }
            else
            {
                buttonImage.GetComponent<Image>().color = fireStateColors[0];
            }
        }
        else
        {
            buttonImage.GetComponent<Image>().color = fireStateColors[2];
        }
	}

    public void PlayerSelectingTarget()
    {
        if (groupWeapons.Count > 0)
        {
            playerInteration.setAction("TargetWeapon");
            List<Equipment> weaponsAsEquipment = new List<Equipment>();
            foreach (Weapon weapon in groupWeapons)
            {
                weaponsAsEquipment.Add(weapon);
            }
            playerInteration.setCurrentEquipment(weaponsAsEquipment, true);
        }
    }
}
