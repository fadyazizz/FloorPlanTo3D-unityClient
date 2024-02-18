using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    // Start is called before the first frame update

    public int count = 0;
    Color c;
    private void Start()
    {
        c = gameObject.GetComponent<MeshRenderer>().material.color;   
    }
    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name);
        count++;
       
    }
    private void OnTriggerExit(Collider other)
    {
        count--;
    }
    private void Update()
    {
        if (count > 0)
        {
            gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", c);
        }
    }
    // Update is called once per frame

}
