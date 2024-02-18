using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class selected_dictionary : MonoBehaviour
{
    

    public Dictionary<int, ArrayList> selectedTable = new Dictionary<int, ArrayList>();
    public ArrayList vectorContainingAll = new ArrayList();
    public ArrayList vectorContainingIds = new ArrayList();
    public GameObject player;
    public FlexibleColorPicker picker;
    public GameObject okPaintingButton;
    public GameObject cancelPaintingButton;
    public bool isPainting=false;
    ArrayList provisionalObjects=new ArrayList();
    public void DestroyProvisional()
    {
        foreach (var obj in provisionalObjects)
        {
            Destroy((GameObject)obj);
        }
        provisionalObjects.Clear();
    }
    public void clearProvisional()
    {
        provisionalObjects.Clear();
    }
    public void addSelected(GameObject go)
    {
        print("adding selected");
        //before adding selection if there is no direct line of sight i will remove it
        RaycastHit hit;
        if (!Physics.Linecast(go.transform.position,player.transform.position,1<<9))
        {
            
                print("hereeeerere");
                vectorContainingAll.Add(go);
                vectorContainingIds.Add(go.GetInstanceID());
                Debug.DrawLine(go.transform.position, player.transform.position, Color.red, 20.0f);
            
        }
       
        
    }
        
    public void deselect(int id)
    {
    
       
        selectedTable.Remove(id);
    }

    public void deselectAll()
    {
        vectorContainingAll.Clear();
        selectedTable.Clear();
    }
    private void Update()
    {
        if (isPainting)
        {
            
           for(int i = 0; i < provisionalObjects.Count; i++)
            {
            
                GameObject go = (GameObject)provisionalObjects[i];
                Material objectMaterial = go.GetComponent<MeshRenderer>().material;
                objectMaterial.color = picker.color;
            }
            
        }
        
    }
    public void paint()
    {
        isPainting = true;
        Dictionary<int, ArrayList>.KeyCollection keyColl = selectedTable.Keys;
        provisionalObjects.Clear();
        foreach (int i in keyColl)
        {
            ArrayList arr = selectedTable[i];

            for (int j = 0; j < arr.Count - 1; j++)
            {
                GameObject ob1 = (GameObject)arr[j];
                GameObject ob2 = (GameObject)arr[j + 1];
                
                Vector3 ob1Position = getPosition(ob1);
                Vector3 ob2Position = getPosition(ob2);
                Vector3 between = ob2Position - ob1Position;
                float distance = between.magnitude;
                GameObject newGameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                newGameObject.tag = "paint";
                WallPaper w1 = ob1.GetComponent<WallPaper>();
                WallPaper w2 = ob2.GetComponent<WallPaper>();
                if(w1.right!=null && w2.left!=null && w1.right.GetInstanceID()==w2.left.GetInstanceID())
                {
                    Destroy(w1.right);  
                }
                w1.right = newGameObject;
                w2.left = newGameObject;
                Vector3 scale = newGameObject.transform.localScale;
                newGameObject.transform.localScale = new Vector3(0.01f, 2.5f, distance);
                newGameObject.transform.position = ob1Position + (between / 2.0f);
                newGameObject.transform.LookAt(ob2Position);
                provisionalObjects.Add(newGameObject);
            }
        }
        picker.gameObject.SetActive(true);
        okPaintingButton.SetActive(true);
        cancelPaintingButton.SetActive(true);
        //so not to select when dragging while picking color 
        gameObject.GetComponent<global_selection>().enabled = false;

    }

    public Vector3 getPosition(GameObject ob)
    {
        GameObject parent = ob.transform.parent.gameObject;
        float x = ob.transform.position.x;
        float y = ob.transform.position.y;
        float z = ob.transform.position.z;
        char orientation = parent.GetComponent<WallMesh>().rotation;
        if (orientation == 'h')
        {
            if(ob.name== "wallpaper_front")
            {
                return new Vector3(x + 0.1f,y,z);
            }
            else
            {
                return new Vector3(x - 0.1f, y, z);
            }
        }
        else
        {
            if (ob.name == "wallpaper_front")
            {
                return new Vector3(x , y, z + 0.1f);
            }
            else
            {
                return new Vector3(x , y, z - 0.1f);
            }
        }
    }
    public void reformSelection()
    {


        print("reforming");
        for (int i = 0; i < vectorContainingAll.Count; i++)
        {
            GameObject ob = (GameObject)vectorContainingAll[i];
            if ((ob.name == "wallpaper_front" || ob.name == "wallpaper_back"))
            {
                int parentId = ob.transform.parent.GetChild(0).gameObject.GetInstanceID();
                if (!selectedTable.ContainsKey(parentId))
                {
                    selectedTable.Add(parentId, new ArrayList());
                }
                ArrayList wall = selectedTable[parentId];
                if(wall!=null) wall.Add(ob);
            }
        }
        sort();
        paint();
        


    }
    public void sort()
    {
        
        Dictionary<int, ArrayList>.KeyCollection keyColl = selectedTable.Keys;

        Asc compare = new Asc();
        foreach (int i in keyColl)
        {
            ArrayList arr = selectedTable[i];
            arr.Sort(compare);
         
           

        }
    }

    public class Asc : IComparer
    {
       
        public int Compare(System.Object x, System.Object y)
        {
            WallPaper wallpaper1 = ((GameObject)x).GetComponent<WallPaper>();
            WallPaper wallpaper2 = ((GameObject)y).GetComponent<WallPaper>();
            int id1 = wallpaper1.getId();
            int id2 = wallpaper2.getId();
            return id1.CompareTo(id2);
        }
    }

}