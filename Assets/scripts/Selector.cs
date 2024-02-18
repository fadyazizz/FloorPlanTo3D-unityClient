using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    // Start is called before the first frame update
    Color c;
    void Start()
    {
        c= gameObject.GetComponent<MeshRenderer>().material.color;
        
            gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
        
    }
    private void OnDestroy()
    {
        gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", c);
    }
    // Update is called once per frame

}
