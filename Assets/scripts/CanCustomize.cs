using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanCustomize : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    public GameObject customizeDoor;
    public GameObject customizeWindow;
    public GameObject customizeWall;
   public static GameObject wall;
   public static GameObject door;
   public static GameObject window;
    void Update()
    {
        
        if (Input.GetMouseButton(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))

            {
                // print(Vector3.Distance(transform.position, hitInfo.transform.position));
                
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hitInfo.distance, Color.yellow);
                    if (hitInfo.transform.gameObject.name == "wall")
                    {
                  
                        wall = hitInfo.transform.parent.gameObject;
                        
                        customizeWall.SetActive(true);
                        customizeDoor.SetActive(false);
                        customizeWindow.SetActive(false);
                    }
                    else
                    {
                        if (hitInfo.transform.gameObject.name == "windowWall(Clone)")
                        {
                            window = hitInfo.transform.parent.gameObject;
                            customizeWindow.SetActive(true);
                            customizeDoor.SetActive(false);
                            customizeWall.SetActive(false);
                        }
                        else if (hitInfo.transform.gameObject.name == "Door(Clone)")
                        {
                            door = hitInfo.transform.parent.gameObject;
                            customizeDoor.SetActive(true);
                            customizeWindow.SetActive(false);
                            customizeWall.SetActive(false);
                        }
                    }
                
                
            }
        }
   
    }
}

