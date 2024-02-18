using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
public class Builder : MonoBehaviour
{
    // Start is called before the first frame update
    
    public static float xScale;
    public static float yScale;
    public static float originalScale;
    public static bool scaleSet;
    public static string data;
    public static Point[] points;
    public static NamesSub[] classes;

    public GameObject spwaner;

    [System.Serializable]
 
    public class Points
    {
       public  Point[] points;
    }
    [System.Serializable]
    public class xDimension
    {
        public int Width;
    }[System.Serializable]
    public class DoorAverage
    {
        public float averageDoor;
    }
    [System.Serializable]
    public class yDimension
    {
        public int Height;
    }
    [System.Serializable]
    public class Point
    {
        public double x1;
        public double y1;
        public double x2;
        public double y2;

    }
    [System.Serializable]
    public class Names
    {
        public NamesSub[] classes; 
    }
    [System.Serializable]
    public class NamesSub
    {
        public string name;
    }
 

    private Point[] localReaderPoints()
    {
       
        
        Points x = JsonUtility.FromJson<Points>(data);
        return x.points;


    }
    private NamesSub[] localReaderNames()
    {
        
        Names x = JsonUtility.FromJson<Names>(data);
        return x.classes;
    }
    private void localReaderScale()
    {

        
        DoorAverage da= JsonUtility.FromJson<DoorAverage>(data);

       
       // xScale = scale + x4K / (float)x.Width;
        //yScale = scale + y4K / (float)y.Height;
        xScale = 1/da.averageDoor;
       yScale = 1/da.averageDoor;
        originalScale = 1 / da.averageDoor;

    }
    private void Awake()
    {
      
        data = Analyze.data;
        //StreamReader reader = new StreamReader("D:/Mask_RCNN/unity/unityData.json");
       // data = reader.ReadToEnd();
       // reader.Close();
        points = localReaderPoints();
        classes = localReaderNames();
        localReaderScale();
        createObjects();
      
    }

  
    private void createObjects()
    {
        int count = -1;
        foreach (Point point in points)
        {
            
            count++;
            if (classes[count].name != "wall")
            {
                continue;
            }
            GameObject gameObj = new GameObject(classes[count].name);
           
            componentsAdder(gameObj, point, classes[count].name);

        }
        count = -1;
       
        foreach (Point point in points)
        {
            count++;
            if (classes[count].name == "wall")
            {
                continue;
            }
            GameObject gameObj = new GameObject(classes[count].name);
            componentsAdder(gameObj, point, classes[count].name);
            

        }

    }

    void componentsAdder(GameObject obj,Point p,string className)
    {
        obj.AddComponent<MeshFilter>();
        obj.AddComponent<MeshRenderer>();
        
        if (className.Equals("wall"))
        {
            obj.tag = "wall";
            obj.AddComponent<WallMesh>();

            WallMesh temp = obj.GetComponent<WallMesh>();
            temp.setPoints((float)p.x1, (float)p.y1, (float)p.x2, (float)p.y2);
            temp.setGameObjectReference(obj);
        }
        if (className.Equals("door"))
        {
            obj.tag = "door";
            obj.AddComponent<Door>();
            Door temp = obj.GetComponent<Door>();
           
            temp.setPoints((float)p.x1, (float)p.y1, (float)p.x2, (float)p.y2);
            temp.setGameObjectReference(obj);
            
        }
        if (className.Equals("window"))
        {
            obj.tag = "window";
            obj.AddComponent<Window>();
            Window temp = obj.GetComponent<Window>();

            temp.setPoints((float)p.x1, (float)p.y1, (float)p.x2, (float)p.y2);
            temp.setGameObjectReference(obj);

        }
        

    }

}
