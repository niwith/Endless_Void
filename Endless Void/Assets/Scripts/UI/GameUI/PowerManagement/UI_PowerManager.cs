using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_PowerManager : MonoBehaviour {

    public ShipSystems shipSystems;
    public Transform equipmentPiecePrefab;
    public DictionaryStorage IconLibaray;

    public Text powerLevelText;
    string powerLevelMessage = "Power Remaining: ";

    public List<Transform> currentEquipmentPieces = new List<Transform>();

	// Use this for initialization
	void Start () {
        
    }

	// Update is called once per frame
	void Update () {
        if (currentEquipmentPieces.Count != shipSystems.powerUsers.Count)
        {
            UpdateEquipment();
        }
        powerLevelText.text = powerLevelMessage + shipSystems.get_PowerRemaining().ToString();
    }

    public void UpdateEquipment()
    {
        foreach (Transform UI_Group in currentEquipmentPieces)
        {
            Destroy(UI_Group.gameObject);
        }

        currentEquipmentPieces.Clear();

        int index = 0;
        foreach (PowerUser piece in shipSystems.powerUsers)
        {
            currentEquipmentPieces.Add(Instantiate(equipmentPiecePrefab));
            currentEquipmentPieces[currentEquipmentPieces.Count - 1].transform.SetParent(transform, false);
            currentEquipmentPieces[currentEquipmentPieces.Count - 1].transform.localScale = new Vector3(1, 1, 1); // Some reason this is a bitch and will resize itself for no reason
            currentEquipmentPieces[currentEquipmentPieces.Count - 1].transform.FindChild("Text_Name").GetComponent<Text>().text = piece.name;
            currentEquipmentPieces[currentEquipmentPieces.Count - 1].transform.FindChild("SF Slider").GetComponent<Slider>().maxValue = piece.powerUsageMaximum;
            currentEquipmentPieces[currentEquipmentPieces.Count - 1].transform.FindChild("SF Slider").GetComponent<Slider>().value = piece.get_PowerUsageCurrent();
            currentEquipmentPieces[currentEquipmentPieces.Count - 1].transform.FindChild("Icon_Type").GetComponent<Image>().sprite = IconLibaray.GetIcon(piece.GetType().ToString());
            currentEquipmentPieces[currentEquipmentPieces.Count - 1].transform.FindChild("SF Slider").GetComponent<CallUpdates>().me = piece;
            index++;
        }
    }
}
