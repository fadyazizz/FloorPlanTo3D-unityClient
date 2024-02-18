using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorGaps : MonoBehaviour
{
    // Start is called before the first frame update
    int count = 0;
    char orientation;
    /* if orientation is h
     the points array would have the following format
    arr[0]=[x1,y1,x2,y2] corresponding to the left object
    arr[1]=[x1,y1,x2,y2] corresponding to the right object
    
     if orientattion is w
    the points array would have the following format
    arr[0]=[x1,y1,x2,y2] corresponding to the top object
    arr[1]=[x1,y1,x2,y2] corresponding to the bottom object*/
    float[][] arr = new float[2][];
    float parentMidX;
    float parentMidY;
    bool didStart = false;

    string whatToFix;
    float leftTop = 0;
    float rightBottom = 0;
    BoxCollider rightBottomMover;
    BoxCollider leftTopMover;
    float x1, y1, x2, y2;
    public void setValues(float x1, float y1, float x2, float y2)
    {
        this.x1 = x1;
        this.x2 = x2;
        this.y1 = y1;
        this.y2 = y2;

    }
    public void setParentValues(float midX, float midY)
    {
        parentMidX = midX;
        parentMidY = midY;
    }
    public void setBoxColliders(BoxCollider left, BoxCollider right)
    {
        rightBottomMover = right;

        leftTopMover = left;
    }
    private void Start()
    {
        if (arr[0] != null)
        {
            Destroy(rightBottomMover);
        }
        if (arr[1] != null)
        {
            Destroy(leftTopMover);
        }

    }

    private void Update()
    {
        didStart = true;
        whatToFix = "";
        if (count != 2)
        {
            if (orientation == 'h')
            {
                if (arr[1] == null)
                {


                    whatToFix = "right";
                    float x = leftTopMover.center.x;
                    float y = leftTopMover.center.y;
                    float z = leftTopMover.center.z;
                    leftTopMover.center = new Vector3(x + leftTop, y, z);
                    leftTop += 0.001f;



                }
                else
                {
                    if (arr[0] == null)
                    {
                        whatToFix = "left";
                        float x = rightBottomMover.center.x;
                        float y = rightBottomMover.center.y;
                        float z = rightBottomMover.center.z;
                        rightBottomMover.center = new Vector3(x - rightBottom, y, z);
                        rightBottom += 0.001f;


                    }
                }
            }
            if (orientation == 'v')
            {
                if (arr[0] == null)
                {
                    whatToFix = "bottom";

                    float x = rightBottomMover.center.x;
                    float y = rightBottomMover.center.y;
                    float z = rightBottomMover.center.z;
                    rightBottomMover.center = new Vector3(x - rightBottom, y, z);
                    rightBottom += 0.001f;
                }
                else
                {
                    if (arr[1] == null)
                    {
                        whatToFix = "top";
                        float x = leftTopMover.center.x;
                        float y = leftTopMover.center.y;
                        float z = leftTopMover.center.z;
                        leftTopMover.center = new Vector3(x + leftTop, y, z);
                        leftTop += 0.001f;

                    }
                }

            }
        }
    }



    public void setOrientation(char orientation)
    {
        this.orientation = orientation;
    }


    //add doors
    float[] getPoint(Collider other)
    {
        if (other.name == "wall")
        {
            WallMesh scr = other.GetComponent<WallMesh>();
            return new float[4] { scr.x1, scr.y1, scr.x2, scr.y2 };
        }
        if (other.name == "windowWall(Clone)")
        {
            // print(other.name+"dadadadas");
            print(other.name);
            Window scr = other.GetComponentInParent<Window>();
            return new float[4] { scr.x1, scr.y1, scr.x2, scr.y2 };
        }
        else
        {
            Door scr = other.GetComponentInParent<Door>();
            return new float[4] { scr.x1, scr.y1, scr.x2, scr.y2 };
        }
    }

    void createWall(Collider other)
    {

        if (whatToFix == "right")
        {
            whatToFix = "";
            float[] points = getPoint(other);
            float otherX1 = points[0];
            float otherY2 = points[3];
            float otherY1 = points[1];
            float otherX2 = points[2];
            GameObject obj = new GameObject("wall");
            obj.AddComponent<MeshFilter>();
            obj.AddComponent<MeshRenderer>();
            WallMesh temp = obj.AddComponent<WallMesh>();
            temp.setPoints(x1, otherY2, otherX2, otherY1);
            arr[1] = points;
            count++;
            Destroy(leftTopMover);
        }
        if (whatToFix == "left")
        {
            float[] points = getPoint(other);
            float otherX1 = points[0];
            float otherY2 = points[3];
            float otherY1 = points[1];
            float otherX2 = points[2];
            GameObject obj = new GameObject("wall");
            obj.AddComponent<MeshFilter>();
            obj.AddComponent<MeshRenderer>();
            WallMesh temp = obj.AddComponent<WallMesh>();
            temp.setPoints(otherX1, otherY2, x2, otherY1);
            arr[0] = points;
            count++;
            whatToFix = "";
            Destroy(rightBottomMover);



        }
        if (whatToFix == "bottom")
        {
            float[] points = getPoint(other);
            float otherX1 = points[0];
            float otherY2 = points[3];
            float otherY1 = points[1];
            float otherX2 = points[2];
            GameObject obj = new GameObject("wall");
            obj.AddComponent<MeshFilter>();
            obj.AddComponent<MeshRenderer>();
            WallMesh temp = obj.AddComponent<WallMesh>();
            temp.setPoints(otherX2, y1, otherX1, otherY2);
            arr[0] = points;
            count++;
            whatToFix = "";
            Destroy(rightBottomMover);
        }
        if (whatToFix == "top")
        {
            float[] points = getPoint(other);
            float otherX1 = points[0];
            float otherY2 = points[3];
            float otherY1 = points[1];
            float otherX2 = points[2];
            GameObject obj = new GameObject("wall");
            obj.AddComponent<MeshFilter>();
            obj.AddComponent<MeshRenderer>();
            WallMesh temp = obj.AddComponent<WallMesh>();
            temp.setPoints(x2, otherY1, x1, y2);
            arr[1] = points;
            count++;
            whatToFix = "";
            Destroy(leftTopMover);
        }



    }
    private void OnTriggerEnter(Collider other)

    {


        if (didStart)
        {
            //GameObject gm = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //fix(other);


            //getting wall info

            createWall(other);

            return;
        }

        float[] p = getPoint(other);
        count++;

        if (orientation == 'v')
        {
            print("vertical");
            float midY = p[1] + (Mathf.Abs(p[3] - p[1]) / 2);
            //setting top
            midY = midY * Builder.yScale;
            if (midY > parentMidY)
            {
                arr[1] = p;
            }
            //setting bottom
            if (midY < parentMidY)
            {
                arr[0] = p;
            }
        }
        if (orientation == 'h')
        {

            float midX = p[0] + (Mathf.Abs(p[2] - p[0]) / 2);
            midX = midX * Builder.xScale;


            if (midX > parentMidX)
            {
                // print("herrrree11111");
                arr[0] = p;
            }
            if (midX < parentMidX)
            {
                //print("herrrree2222222");
                arr[1] = p;
            }

        }





    }
}
