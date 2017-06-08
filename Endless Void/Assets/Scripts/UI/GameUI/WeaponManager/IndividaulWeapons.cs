using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Dont ask what is up with the naming conventions on this, but basically this is the script that creates the individual weapon UI objects
/// </summary>
public class IndividaulWeapons : MonoBehaviour {

    public ShipSystems shipSystems;
    public Transform weaponUIPrefab;
    public DictionaryStorage IconLibaray;

    public List<Transform> currentUIWeapons = new List<Transform>();

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(currentUIWeapons.Count != shipSystems.weapons.Count)
        {
            UpdateWeapons();
        }
	}

    public void UpdateWeapons()
    {
        foreach (Transform UI_Group in currentUIWeapons)
        {
            Destroy(UI_Group.gameObject);
        }

        currentUIWeapons.Clear();

        foreach (Weapon piece in shipSystems.weapons)
        {
            currentUIWeapons.Add(Instantiate(weaponUIPrefab));
            currentUIWeapons[currentUIWeapons.Count - 1].transform.SetParent(transform, false);
            currentUIWeapons[currentUIWeapons.Count - 1].transform.localScale = new Vector3(1, 1, 1); // Some reason this is a bitch and will resize itself for no reason
            currentUIWeapons[currentUIWeapons.Count - 1].transform.FindChild("Text_Name").GetComponent<Text>().text = piece.name;
            currentUIWeapons[currentUIWeapons.Count - 1].transform.FindChild("Icon_Type").GetComponent<Image>().sprite = IconLibaray.GetIcon(piece.GetType().ToString());
            currentUIWeapons[currentUIWeapons.Count - 1].GetComponent<IndividualWeaponUIManager>().thisWeapon = piece;
        }
    }
}
