using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities {
	public static bool stringInList (List<string> inputList, string inputFind)
    {
        foreach(string item in inputList)
        {
            if (inputFind == item)
            {
                return true;
            }
        }
        return false;
    }

    public static void printAllInList<generic>(List<generic> inputList)
    {
        foreach(generic thisObject in inputList)
        {
            Debug.Log(thisObject);
        }
    }

    public static Vector3 getMousePosition(float ypos = 0)
    {
        Camera mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>() ;
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.y -= mainCamera.transform.position.y;
        worldPosition.y += ypos;
        return worldPosition;
    }

    public static Vector3 getScreenPosition(Vector3 objectPosition)
    {
        Camera mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        Vector3 screenPosition = mainCamera.GetComponent<Camera>().WorldToScreenPoint(objectPosition);
        return screenPosition;
    }
}
