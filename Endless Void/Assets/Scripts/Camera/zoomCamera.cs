using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class zoomCamera : MonoBehaviour {
    public GameObject me;
    public float speed = 5;
    public float currentFov = 10;
    public float maxFov;
    public float minFov;

    void Update() {
        float movementupdown = Input.GetAxis("Mouse ScrollWheel") * speed * (currentFov/100) * Time.deltaTime;
        if(!EventSystem.current.IsPointerOverGameObject())
        {
            currentFov += movementupdown;
        }
        if(currentFov > maxFov)
        {
            currentFov = maxFov;
        }
        if(currentFov < minFov && !EventSystem.current.IsPointerOverGameObject())
        {
            currentFov = minFov;
        }
        me.GetComponent<Camera>().orthographicSize = currentFov;
    }
}