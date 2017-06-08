using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CallUpdates : MonoBehaviour {

    public ShipSystems shipSystems;
    public Slider slider;
    public PowerUser me;

    public int lastValue;

    void Start()
    {
        shipSystems = GameObject.Find("Player_Ship").GetComponent<ShipSystems>();
        slider = GetComponent<Slider>();
    }

	public void UpdatePowerValues()
    {
        if(slider == null)
        {
            Destroy(this);
        }
        else
        {
            slider.value = shipSystems.updatePowerValue((int)slider.value, me);
        }
    } 
}
