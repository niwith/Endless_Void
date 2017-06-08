using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameManager : MonoBehaviour {

    public List<Transform> saveObjects = new List<Transform>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SaveGame()
    {
        foreach(Transform saveObject in saveObjects)
        {
            saveObject.SendMessage("save");
        }
    }

    public void LoadGame()
    {
        //Load game stuff
    }
}
