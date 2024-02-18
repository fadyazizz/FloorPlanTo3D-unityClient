using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMesh : MonoBehaviour
{
    // Start is called before the first frame update
   
   
   
   
    Vector3[] meshDataVertices;
    public float x1,y1,x2,y2;
    GameObject ob;
    public char rotation;
    public bool isFiller = false;
    Bounds meshBounds;
     int wallPaperIds=0;
    Vector3 scale;
    public void setGameObjectReference(GameObject obj)

    {
        ob = obj;
    }

    public void  setPoints(float x1,float y1,float x2,float y2)
    {

        this.x1 = x1;
        this.x2 = x2;
        this.y1 = y1;
        this.y2 = y2;
       

    }
   
    void Start()
    {
        
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.layer = 9;
        cube.name = "wall1";
        cube.transform.position = new Vector3(0, 0, 0);
        Vector3 coordinates = getCoordinates();
        rotation = getRotation();

        Quaternion angle = getAngle(rotation);


       meshBounds = cube.GetComponent<MeshFilter>().mesh.bounds;
        scale = getScale();
        cube.transform.localScale = scale;
        
        cube.transform.rotation = angle;

       
        cube.transform.parent = gameObject.transform;
        transform.position = coordinates;
        if (!isFiller) {
            cube.GetComponent<BoxCollider>().isTrigger = true;
            cube.AddComponent<BoxCollider>();
            cube.name = "wall";
        }
        else
        {
            cube.AddComponent<BoxCollider>().isTrigger=true;
        }

        cube.AddComponent<Rigidbody>().isKinematic = true;
        addWallPaperPoints(cube);
      
       
        
    }
    private void wallPaperPoints(float x,float z,string name)
    {
        Vector3 colliderSize = new Vector3(0.1f, 0.1f, 0.1f);
        GameObject go = new GameObject(name);
        go.tag = "wallPaperr";
        go.transform.position = new Vector3(x, 1.25f, z);
        go.AddComponent<Rigidbody>().isKinematic = true;
        BoxCollider goCollider = go.AddComponent<BoxCollider>();
        goCollider.isTrigger = true;
        go.transform.localScale = colliderSize;
        go.transform.parent = transform;
       WallPaper wp= go.AddComponent<WallPaper>();
        wp.addId(this.wallPaperIds);
        this.wallPaperIds++;

    }
    public void addWallPaperPoints(GameObject cube)
    {

        float length = cube.transform.localScale.x;
        length = length * 100;
        int lengthInCm = (int)length;
        //print(lengthInCm);
        
        float startPoint ;
        //float midPoint;
        if (rotation == 'h')
        {
            startPoint = x1 * Builder.xScale;
        }
        else
        {
            startPoint = y1 * Builder.xScale;
        }
        int interval = 15;
        int numOfPoints = lengthInCm / interval;
        float remainingPoint = (lengthInCm % interval)/100f;
        float intervalInCm = interval / 100f;
        print(remainingPoint);
        int i;
        for ( i = 0; i <= numOfPoints; i++)
        {
            if(rotation=='h'){
                wallPaperPoints(y1 * Builder.yScale-0.1f, startPoint + (i * intervalInCm), "wallpaper_front");
                wallPaperPoints(y2 * Builder.yScale+0.1f,  startPoint + (i * intervalInCm), "wallpaper_back");
            }
            else
            {
                wallPaperPoints(startPoint + (i * intervalInCm), x1 * Builder.xScale-0.1f, "wallpaper_front");
                wallPaperPoints(startPoint + (i * intervalInCm), x2 * Builder.xScale+0.1f, "wallpaper_back");
            }
        }
        if (rotation == 'h')
        {
            wallPaperPoints(y1 * Builder.yScale - 0.1f,  (startPoint + ((i - 1) * intervalInCm)) + remainingPoint, "wallpaper_front");
            wallPaperPoints(y2 * Builder.yScale + 0.1f, (startPoint + ((i - 1) * intervalInCm)) + remainingPoint, "wallpaper_back");
        }
        else
        {
            wallPaperPoints((startPoint + ((i - 1) * intervalInCm)) + remainingPoint, x1 * Builder.xScale - 0.1f, "wallpaper_front");
            wallPaperPoints((startPoint + ((i - 1) * intervalInCm)) + remainingPoint, x2 * Builder.xScale +0.1f, "wallpaper_back");
        }
     

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
    public Vector3 getScale()
    {
        float yDiff = Mathf.Abs(y1 - y2);
        float xDiff = Mathf.Abs(x1 - x2);
        float meshBoundY = meshBounds.size.x;
        float meshBoundX = meshBounds.size.z;
        float scaleY = yDiff / meshBoundY;
        float scaleX = xDiff / meshBoundX;

        if (rotation == 'v')
        {
           
            Vector3 newScale = new Vector3(scaleY * Builder.yScale, 2.5f, scaleX*Builder.xScale);
            return newScale;
        }
        // here where you can handle diagonal doors in the future

        else
        {
           
            Vector3 newScale = new Vector3(scaleX * Builder.xScale, 2.5f, scaleY*Builder.yScale);
            return newScale;
        }
    }
    public Vector3 getCoordinates()
    {
        float xCenter = x1 + (Mathf.Abs(x2 - x1) / 2);
        float yCenter = y1 + (Mathf.Abs(y2 - y1) / 2);
        return new Vector3(yCenter * Builder.yScale, 1.25f, xCenter * Builder.xScale);
    }
 

   
    
    // Update is called once per frame

}
