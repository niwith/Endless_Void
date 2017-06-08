using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryStorage : MonoBehaviour {

    public string[] icon_names;
    public Sprite[] icon_sprites;

    public int[] component_id;
    public Transform[] component_prefab;

    public string[] marker_type;
    public Transform[] marker_prefab;

    public Sprite GetIcon(string icon_name)
    {
        for(int i = 0; i < icon_names.Length; i++)
        {
            if(icon_name == icon_names[i])
            {
                return icon_sprites[i];
            }
        }
        Debug.Log("Icon not found");
        return null;
    }

    public Transform GetPrefab(int ID)
    {
        for (int i = 0; i < component_id.Length; i++)
        {
            if (ID == component_id[i])
            {
                return component_prefab[i];
            }
        }
        Debug.Log("Prefab not found");
        return null;
    }

    public Transform GetMarker(string type)
    {
        for (int i = 0; i < marker_type.Length; i++)
        {
            if (type == marker_type[i])
            {
                return marker_prefab[i];
            }
        }
        Debug.Log("Marker not found");
        return null;
    }
}
