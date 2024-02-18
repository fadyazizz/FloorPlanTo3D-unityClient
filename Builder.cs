using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class Builder : MonoBehaviour
{
    // Start is called before the first frame update
    [System.Serializable]
    public class Points
    {
       public  Point[] points;
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
    private string fileReader()
    {
        StreamReader reader = new StreamReader("D:/Mask_RCNN/unity/unityData.json");
        string data = reader.ReadToEnd();
        reader.Close();
        return data;
    }
    private Point[] localReaderPoints(string data)
    {
       
        
        Points x = JsonUtility.FromJson<Points>(data);
        return x.points;


    }
    private NamesSub[] localReaderNames(string data)
    {
        
        Names x = JsonUtility.FromJson<Names>(data);
        return x.classes;
    }

    private void Awake()
    {

        string data = fileReader();
        Point[] points = localReaderPoints(data);
        NamesSub[] classes = localReaderNames(data);
        createObjects(points, classes);


        
       
    }
    
    private void createObjects(Point[] points,NamesSub[] classes)
    {
        int count = -1;
        foreach(Point point in points)
        {
            count++;
            GameObject gameObj = new GameObject(classes[count].name);
            componentsAdder(gameObj,point,classes[count].name);
           
        }
        
    }
  
    void componentsAdder(GameObject obj,Point p,string className)
    {
        obj.AddComponent<MeshFilter>();
        obj.AddComponent<MeshRenderer>();
        if (className.Equals("wall"))
        {
            obj.AddComponent<WallMesh>();
            obj.GetComponent<WallMesh>().setPoints((float)p.x1,(float)p.y1,(float)p.x2,(float)p.y2);
        }

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
