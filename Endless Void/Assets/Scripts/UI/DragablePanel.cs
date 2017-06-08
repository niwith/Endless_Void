using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragablePanel : MonoBehaviour {
    bool setPos = true;
    float offset_y;
    float offset_x;

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            setPos = true;
        }
    }

    public void OnDrag() {
        Vector3 mousePosition = Input.mousePosition;
        if(setPos)
        {
            Debug.Log("Doin the thing");
            offset_y = transform.position.y - mousePosition.y;
            offset_x = transform.position.x - mousePosition.x;
            setPos = false;
        }
        
        Vector3 newPosition = new Vector3(offset_x + mousePosition.x, offset_y + mousePosition.y);
        transform.position = newPosition;
    }
}
