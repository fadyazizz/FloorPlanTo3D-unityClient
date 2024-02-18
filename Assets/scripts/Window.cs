using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    // Start is called before the first frame update
    public float x1,x2,y1,y2;
    
    GameObject ob;
    public char rotation;
   
    Bounds meshBounds;
    public void setGameObjectReference(GameObject obj)

    {
        ob = obj;
    }

 

    void Start()

    {
        
        
        GameObject testPrefab = (GameObject)Resources.Load("windowWall");
        Vector3 coordinates = getCoordinates();
         rotation = getRotation();
        Quaternion angle = getAngle(rotation);

        GameObject prefabInstance = Instantiate(testPrefab, coordinates, angle);

        float midX = x1 + (Mathf.Abs(x2 - x1) / 2);
        float midY = y1 + (Mathf.Abs(y2 - y1) / 2);






        WindowGaps windowGapsComponent = prefabInstance.AddComponent<WindowGaps>();
        windowGapsComponent.setParentValues(midX * Builder.xScale, midY * Builder.yScale);
        windowGapsComponent.setOrientation(rotation);
        windowGapsComponent.setValues(x1, y1, x2, y2);
        windowGapsComponent.setRotation(rotation);

        ob.transform.position = coordinates;
        prefabInstance.transform.parent = ob.transform;


        meshBounds = prefabInstance.GetComponent<MeshFilter>().mesh.bounds;
        Vector3 scale = getScale();

        prefabInstance.transform.localScale = scale;
        

       BoxCollider mainBox =prefabInstance.AddComponent<BoxCollider>();
        mainBox.isTrigger = true;
        prefabInstance.AddComponent<BoxCollider>();
        BoxCollider leftMover = prefabInstance.AddComponent<BoxCollider>();
        BoxCollider rightMover = prefabInstance.AddComponent<BoxCollider>();
       
        windowGapsComponent.setBoxColliders(mainBox,leftMover, rightMover);
        /*ob.AddComponent<BoxCollider>();
        BoxCollider bc = ob.GetComponent<BoxCollider>();
        bc.bounds = prefabInstance.GetComponent<BoxCollider>().bounds;*/
        prefabInstance.AddComponent<Rigidbody>();
        prefabInstance.GetComponent<Rigidbody>().isKinematic = true;


    }
    public Vector3 getCoordinates()
    {
        float xCenter = x1 + (Mathf.Abs(x2 - x1) / 2);
        float yCenter = y1 + (Mathf.Abs(y2 - y1) / 2);
        return new Vector3(yCenter * Builder.yScale, 0, xCenter * Builder.xScale);
    }
    public Vector3 getScale()
    {
        if (rotation == 'v')
        {
            float yDiff = Mathf.Abs(y1 - y2);
            float meshBound = meshBounds.size.x;
            float scale = yDiff / meshBound;
            Vector3 newScale = new Vector3(scale * Builder.yScale, 1, 1);
            return newScale;
        }
        // here where you can handle diagonal doors in the future

        else
        {
            float xDiff = Mathf.Abs(x1 - x2);
            float meshBound = meshBounds.size.x;
            float scale = xDiff / meshBound;
            Vector3 newScale = new Vector3(scale * Builder.xScale, 1, 1);
            return newScale;
        }



    }
    public void setPoints(float x1, float y1, float x2, float y2)

    {


        this.x1 = x1;
        this.x2 = x2;
        this.y1 = y1;
        this.y2 = y2;



    }
    private char getRotation()
    {
        float xDiff = Mathf.Abs(x1 - x2);
        float yDiff = Mathf.Abs(y1 - y2);
        if (xDiff > yDiff)
        {
            return 'h';
        }
        if (yDiff > xDiff)
        {
            return 'v';
        }
        return 'n';
    }
    private Quaternion getAngle(char c)
    {
        switch (c)
        {
            case 'v': return Quaternion.identity;

            case 'h': return Quaternion.Euler(0, 90, 0);
            default: return Quaternion.identity;
        }
    }
    // Update is called once per frame
   
}
