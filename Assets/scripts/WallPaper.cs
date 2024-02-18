using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPaper : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int id;
    public GameObject left;
    public GameObject right;
    public void addId(int id)
    {
        this.id = id;
    }
    public int getId()
    {
        return this.id;
    }
   
}
