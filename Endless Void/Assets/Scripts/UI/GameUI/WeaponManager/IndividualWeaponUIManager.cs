using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndividualWeaponUIManager : MonoBehaviour {

    public Weapon thisWeapon;

    public List<Toggle> groupToggles = new List<Toggle>();
    public List<WeaponGroupManager> weaponGroups = new List<WeaponGroupManager>();
    public Toggle pointDefense;
    public Toggle fireMode;

    public Transform buttonImage;
    public Color[] fireStateColors;

    PlayerInteraction playerInteration;

	// Use this for initialization
	void Start () {
        playerInteration = GameObject.Find("PlayerManager").GetComponent<PlayerInteraction>();
        for (int i = 1; i <= 4; i++)
        {
            string find = "Weapon_Group_" + i.ToString();
            weaponGroups.Add(GameObject.Find(find).GetComponent<WeaponGroupManager>());
        }
    }
	
	// Update is called once per frame
	void Update () {
        // Change color to indicate the current weapon targeting status
        if(thisWeapon.hasTarget)
        {
            buttonImage.GetComponent<Image>().color = fireStateColors[1];
        }
        else
        {
            buttonImage.GetComponent<Image>().color = fireStateColors[0];
        }
    }

    public void PlayerSelectingTarget() 
    {
        playerInteration.setAction("TargetWeapon");
        playerInteration.setCurrentEquipment(new List<Equipment> { thisWeapon }, true);
    }

    public void groupToggleChanged(int toggleNumber)
    {
        toggleNumber = toggleNumber - 1; // Reduce by 1 to account for numbering differences
        if(groupToggles[toggleNumber].isOn)
        {
            weaponGroups[toggleNumber].groupWeapons.Add(thisWeapon);
        }
        else
        {
            weaponGroups[toggleNumber].groupWeapons.Remove(thisWeapon);
        }
        
    }
}
