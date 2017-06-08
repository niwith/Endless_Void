using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UtilitiesUIManager : MonoBehaviour {

    public ShipSystems shipSystems;
    public Transform utilityUIPrefab;
    public DictionaryStorage IconLibaray;

    public List<Transform> currentUIUtilities = new List<Transform>();

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (currentUIUtilities.Count != shipSystems.weapons.Count)
        {
            UpdateUtilities();
        }
    }

    public void UpdateUtilities()
    {
        foreach (Transform UI_Group in currentUIUtilities)
        {
            Destroy(UI_Group.gameObject);
        }

        currentUIUtilities.Clear();

        foreach (Weapon piece in shipSystems.weapons)
        {
            currentUIUtilities.Add(Instantiate(utilityUIPrefab));
            currentUIUtilities[currentUIUtilities.Count - 1].transform.SetParent(transform, false);
            currentUIUtilities[currentUIUtilities.Count - 1].transform.localScale = new Vector3(1, 1, 1); // Some reason this is a bitch and will resize itself for no reason
            currentUIUtilities[currentUIUtilities.Count - 1].transform.FindChild("Text_Name").GetComponent<Text>().text = piece.name;
            currentUIUtilities[currentUIUtilities.Count - 1].transform.FindChild("Button_Activate").FindChild("Image_Target").GetComponent<Image>().sprite = IconLibaray.GetIcon(piece.GetType().ToString());
            //currentUIUtilities[currentUIUtilities.Count - 1].GetComponent<IndividualWeaponUIManager>().thisWeapon = piece;
        }
    }
}
