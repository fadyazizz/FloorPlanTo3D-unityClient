using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    void Start()
    {
        Input.simulateMouseWithTouches = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))

            {
               
                //print(hitInfo.transform.gameObject.name);
                
               // print("It's working");
            }
        }
    }
}
