using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject targetSprite;
   public  bool postionSelected=false;
    public Vector3 position;

    public GameObject okButton;
    public GameObject retryButton;
    void Start()
    {
        Input.simulateMouseWithTouches = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)&&!postionSelected)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                
                    postionSelected = true;
                    position = new Vector3(raycastHit.point.x, 0.2f, raycastHit.point.z);
                    targetSprite.transform.position = position;
                    okButton.SetActive(true);
                    retryButton.SetActive(true);
                
            }
        }
    } 
}
