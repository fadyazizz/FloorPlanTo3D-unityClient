using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    public static bool teleport = false;
    public GameObject teleportButton;
    public static GameObject door;
    void Update()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit) )
        {
            if (hit.transform.gameObject.name == "Door(Clone)" && Vector3.Distance(transform.position, hit.transform.position)<1f)
            {
                print(hit.transform.gameObject.name);
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                //Debug.Log("Did Hit");
                door = hit.transform.gameObject;
                teleportButton.SetActive(true);
            }
            else
            {
                teleportButton.SetActive(false);
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
              //  Debug.Log("Did not Hit");
            }
        }
       
    }
}
