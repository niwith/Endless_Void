using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerInteraction : MonoBehaviour {

    public Transform mainCamera;
    public Transform playerShip;
    public ShipMovement shipMovementScript;
    public Transform navMarker;
    public Transform uicanvas;
    Slider thrustSlider;
    ShipMovement shipMovement;
    public Texture2D[] cursors;

    public string defaultAction = "ShipMove";
    public string actionSelected = "ShipMove";
    
    public List<Transform> activeNavMarkers = new List<Transform>();

    public List<Equipment> activeEquipment = new List<Equipment>();

    void Start()
    {
        thrustSlider = GameObject.Find("Slider_Thrust").GetComponent<Slider>();
        shipMovement = GameObject.Find("Player_Ship").GetComponent<ShipMovement>();
    }

    // Update is called once per frame
    void Update () {
        shipMovement.thrust = thrustSlider.value;
        if (Input.GetButtonDown("Freeze Action"))
        {
            if(TimeManager.softTimeSpeedModifier == 0)
            {
                TimeManager.softTimeSpeedModifier = 1;
            } 
            else
            {
                TimeManager.softTimeSpeedModifier = 0;
            }
        }
        if (actionSelected == "ShipMove") 
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                if(Input.GetKey(KeyCode.LeftShift))
                {
                    Vector3 mousePosition = Utilities.getMousePosition(0.5f);
                    shipMovementScript.navigationPoint.Add(mousePosition);
                    activeNavMarkers.Add(Instantiate(navMarker, mousePosition, Quaternion.Euler(0, 0, 0)));
                    activeNavMarkers[activeNavMarkers.Count - 1].transform.SetParent(uicanvas.transform);
                    activeNavMarkers[activeNavMarkers.Count - 1].transform.SetAsFirstSibling();
                }
                else
                {
                    shipMovementScript.navigationPoint.Clear();
                    foreach(Transform marker in activeNavMarkers)
                    {
                        Destroy(marker.gameObject);
                    }
                    activeNavMarkers.Clear();
                    Vector3 mousePosition = Utilities.getMousePosition(0.5f);
                    shipMovementScript.navigationPoint.Add(mousePosition);
                    activeNavMarkers.Add(Instantiate(navMarker, mousePosition, Quaternion.Euler(0, 0, 0)));
                    activeNavMarkers[activeNavMarkers.Count - 1].transform.SetParent(uicanvas.transform);
                }
            }
        }

        while (activeNavMarkers.Count > shipMovementScript.navigationPoint.Count)
        {
            Destroy(activeNavMarkers[0].gameObject);
            activeNavMarkers.Remove(activeNavMarkers[0]);
        }

        for (int i = 0; i < (activeNavMarkers.Count); i++)
        {
            activeNavMarkers[i].position = Utilities.getScreenPosition(shipMovementScript.navigationPoint[i]);
        }

        if(actionSelected == "TargetWeapon")
        {
            // Exit out of target selection
            if(Input.GetMouseButtonDown(1))
            {
                actionSelected = defaultAction;
                Cursor.SetCursor(cursors[0], Vector2.zero, CursorMode.Auto);
            }


            if(Input.GetMouseButtonDown(0))
            {
                // Exit out of target selection is player clicks on UI
                if(EventSystem.current.IsPointerOverGameObject())
                {
                    actionSelected = defaultAction;
                    Cursor.SetCursor(cursors[0], Vector2.zero, CursorMode.Auto);
                }
                else
                {
                    RaycastHit hit;
                    Physics.Raycast(mainCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit);
                    Transform objectHit = hit.transform;
                    // If the object hit is not on the default (used for objects in world) delect it
                    if (objectHit != null && objectHit.gameObject.layer == 1)
                    {
                       
                    }
                    else
                    {
                        objectHit = null;
                        // Only target space if the player holds left shift, otherwise exit selection
                        if (Input.GetKey(KeyCode.LeftShift))
                        {
                            foreach (Equipment item in activeEquipment)
                            {
                                Weapon weapon = (Weapon)item;
                                weapon.set_targetSpace(Utilities.getMousePosition(0.5f), true);
                            }
                            actionSelected = defaultAction;
                            Cursor.SetCursor(cursors[0], Vector2.zero, CursorMode.Auto);
                        }
                        else
                        {
                            actionSelected = defaultAction;
                            Cursor.SetCursor(cursors[0], Vector2.zero, CursorMode.Auto);
                        }
                    }                                      
                }
            }
        }

    }

    public void setAction(string action)
    {
        actionSelected = action;
        if(action == "TargetWeapon")
        {
            Cursor.SetCursor(cursors[1], new Vector2((float)cursors[1].width/2, (float)cursors[1].height / 2), CursorMode.Auto);
        }
    }

    public void setCurrentEquipment(List<Equipment> equipment, bool clearCurrent)
    {
        if(clearCurrent)
        {
            activeEquipment.Clear();
        }
        foreach(Equipment item in equipment)
        {
            activeEquipment.Add(item);
        }
    }
}
