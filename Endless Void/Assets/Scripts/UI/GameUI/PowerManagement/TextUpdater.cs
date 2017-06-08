using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUpdater : MonoBehaviour
{
    // Update is called once per frame
    public void UpdateValue(Slider relSlider)
    {
        GetComponent<Text>().text = relSlider.value.ToString();
    }
}
