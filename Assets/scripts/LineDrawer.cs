using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
public class LineDrawer : MonoBehaviour
{
    // Start is called before the first frame update
    private LineRenderer lineRend;
    private Vector2 mousePos;
    private Vector2 startMousePos;
    [SerializeField]
    private Text distanceText;
    private static float distance;
    public GameObject plane;
    Vector3 end;
    Vector3 start;
    public Button okButton;
    public Button retryButton;
    public InputField textField;
    bool didSelect = false;
    void Start()
    {
        lineRend = GetComponent<LineRenderer>();
        lineRend.positionCount = 2;
        Input.simulateMouseWithTouches = true;
    }

    // Update is called once per frame
    [SerializeField] private LayerMask layerMask;
    void Update()
    {


        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                start = new Vector3(raycastHit.point.x, 0.1f, raycastHit.point.z);
               lineRend.SetPosition(0, start);
                okButton.gameObject.SetActive(false);
                retryButton.gameObject.SetActive(false);
                textField.gameObject.SetActive(false);
            }
           

        }
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit,float.MaxValue,layerMask))
            {
                end = new Vector3(raycastHit.point.x, 0.1f, raycastHit.point.z);
                lineRend.SetPosition(1, end);
               

            }
            distance = (end - start).magnitude;
            //distanceText.text = distance.ToString("F2");
            

        }
        if (Input.GetMouseButtonUp(0))
        {
            okButton.gameObject.SetActive(true);
            retryButton.gameObject.SetActive(true);
            textField.gameObject.SetActive(true);
            GetComponent<LineDrawer>().enabled = false;
           
        }
       
    }
    public static float getDistance()
    {
        return distance;
    }
 
}
