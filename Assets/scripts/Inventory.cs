using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
public class Inventory : MonoBehaviour
{
    //reference to the playey;
    
    public AudioSource goodArea;
    public AudioSource ohCant;

    public GameObject furnishingObj;
    public GameObject furnishing;
    public GameObject Player;
    public  Vector3 playerPosition;
    public GameObject pieceBeingAdded;
    public GameObject changeDoorSliders;
    public GameObject deleteFurnitureMenu;
    public GameObject cancelSelectionToDelete;
    public GameObject doorSelection;
    public static bool spawningFurnishing = false;
    //here are all the doors buttons;
    public Button door_1;
    public Button door_2;
    public Button door_3;
    public Button door_4;
    public Button defualt;


    public GameObject sliders;


    string name;
    bool isDeleting=false;
    bool isChangingDoor = false;
    GameObject doorBeingChanged;
    bool itemSelected;
    //here are the beds
    public Button bed_01;
    public Button bed_02;

    //table category here

    public Button desk;
    public Button table;
    public Button bar_counter;
    public Button bollard;
    public Button kitchen_table;
    public Button stool;
    public Button chair;
    public Button sofa;
    public Button toilet_seat;
    public Button wash_basin;
    void Start()
    {

        door_1.onClick.AddListener(delegate { ChangeDoor("Door_1"); });
        door_2.onClick.AddListener(delegate { ChangeDoor("Door_2"); });
        door_3.onClick.AddListener(delegate { ChangeDoor("Door_3"); });
        door_4.onClick.AddListener(delegate { ChangeDoor("Door_4"); });
        defualt.onClick.AddListener(delegate { ChangeDoor("Door"); });
        bed_01.onClick.AddListener(delegate { addFurniturePiece("Bed_01"); });
        bed_02.onClick.AddListener(delegate { addFurniturePiece("Bed_02"); });
        desk.onClick.AddListener(delegate { addFurniturePiece("Desk"); });
        table.onClick.AddListener(delegate { addFurniturePiece("table"); });
        bar_counter.onClick.AddListener(delegate { addFurniturePiece("bar_counter"); });
        bollard.onClick.AddListener(delegate { addFurniturePiece("bollard"); });
        kitchen_table.onClick.AddListener(delegate { addFurniturePiece("kitchen_table"); });
        stool.onClick.AddListener(delegate { addFurniturePiece("stool"); });
        sofa.onClick.AddListener(delegate { addFurniturePiece("sofa"); });
        toilet_seat.onClick.AddListener(delegate { addFurniturePiece("toilet"); });
        wash_basin.onClick.AddListener(delegate { addFurniturePiece("washbasin"); });
        chair.onClick.AddListener(delegate { addFurniturePiece("chair"); });
        

    }

    public void ChangeDoor(string door)
    {
       
        doorBeingChanged.GetComponent<Door>().changeDoorShape(door);
        doorSelection.SetActive(false);
        doorBeingChanged = null;
        Destroy(furnishing);
        isChangingDoor = false;

        changeDoorSliders.SetActive(false);
        Player.SetActive(true);
        

    }
    public void cancelChangeDoor()
    {
        //playerPosition = Player.transform.position;
        doorSelection.SetActive(false);
        doorBeingChanged = null;
        Destroy(furnishing);
        isChangingDoor = false;


        Player.SetActive(true);
        changeDoorSliders.SetActive(false);

    }
    public void addFurniturePiece(string name)
    {
        GameObject pre = (GameObject)Resources.Load("Furnishing");
        furnishing = Instantiate(pre);
        this.name = name;
        furnishing.SetActive(true);
        
        
        print("adding furniture");
        playerPosition = Player.transform.position;
        float posX = Player.transform.position.x;
        float posZ = Player.transform.position.z;
        print(posX);
        print(posZ);
        
        //will be set to false by the menu function script
        spawningFurnishing = true;
    

        sliders.SetActive(true);
       

        GameObject testPrefab = (GameObject)Resources.Load(name);
        testPrefab.transform.position = new Vector3(0, 0, 0);
        
        GameObject prefabInstance = Instantiate(testPrefab, (new Vector3(0f,0f,0f)), Player.transform.rotation);
        prefabInstance.transform.position = new Vector3(0, 0, 0);
        pieceBeingAdded = prefabInstance;


      
       
        prefabInstance.transform.GetChild(0).gameObject.AddComponent<Mover>();
       // prefabInstance.transform.GetChild(0).gameObject.transform.position = new Vector3(0, 0, 0);
        Player.SetActive(false);
        prefabInstance.transform.parent = furnishing.transform;
        furnishing.transform.GetChild(2).position = new Vector3(0, 0, 0);
       // prefabInstance.transform.position = new Vector3(0, 0, 0);

    }
    public void scaleX(float newXScale)
    {
        float scaleY = pieceBeingAdded.transform.localScale.y;
        float scaleZ = pieceBeingAdded.transform.localScale.z;
        pieceBeingAdded.transform.localScale = new Vector3(newXScale,scaleY,scaleZ);

    }
    public void scaleZ(float newZScale)
    {
        float scaleY = pieceBeingAdded.transform.localScale.y;
        float scaleX = pieceBeingAdded.transform.localScale.x;
        pieceBeingAdded.transform.localScale = new Vector3(scaleX, scaleY, newZScale);

    }

    public void confirmAddingPiece()
    {
        if (pieceBeingAdded.transform.GetChild(0).GetComponent<Mover>().count > 0)
        {
            ohCant.Play();
            return;
        }
        else
        {
           goodArea.Play();
            GameObject testPrefab = (GameObject)Resources.Load(this.name);
            
            testPrefab.transform.position = new Vector3(0, 0, 0);
            float posX = pieceBeingAdded.transform.position.x;
            float posZ = pieceBeingAdded.transform.position.z;
            
            GameObject prefabInstance = Instantiate(testPrefab, new Vector3(posX,0,posZ), pieceBeingAdded.transform.rotation);
            prefabInstance.transform.localScale = pieceBeingAdded.transform.localScale;
            prefabInstance.tag = "furniture";
            prefabInstance.layer = 0;
            prefabInstance.transform.GetChild(0).gameObject.layer = 0;
            prefabInstance.transform.GetChild(0).gameObject.tag = "furniture";
            Destroy(furnishing.transform.GetChild(2).gameObject);
           
            furnishing.SetActive(false);
           
           
            pieceBeingAdded = null;
            sliders.SetActive(false);
            Destroy(furnishing);
            Player.SetActive(true);
        }
        

    }
    public void cancel()
    {
        Destroy(furnishing);
        pieceBeingAdded = null;
        Player.SetActive(true);
        //changeDoorSliders.SetActive(false);

    }
    public void changeDoor()
    {
        GameObject pre = (GameObject)Resources.Load("Furnishing");
        furnishing = Instantiate(pre);
        float rotationY = furnishing.transform.rotation.eulerAngles.y;
        float rotationZ = furnishing.transform.rotation.eulerAngles.z;
        float playersY = Player.transform.rotation.eulerAngles.y;
        furnishing.transform.rotation = Quaternion.Euler(340f, playersY, rotationZ);
        
        playerPosition = Player.transform.position;
        Player.SetActive(false);
        furnishing.SetActive(true);
        isChangingDoor = true;
        spawningFurnishing = true;
        changeDoorSliders.SetActive(true);
    }
    public void delete()
    {
        print("in delete");
        GameObject pre = (GameObject)Resources.Load("Furnishing");
        furnishing = Instantiate(pre);
        playerPosition = Player.transform.position;
        Player.SetActive(false);
        furnishing.SetActive(true);
        isDeleting = true;
        spawningFurnishing = true;
        cancelSelectionToDelete.SetActive(false);
        changeDoorSliders.SetActive(true);

    }
    private void Update()
    {
        if (isDeleting)
        {
           // print("is deletingggg");
            if (Input.GetMouseButtonDown(0))
            {
               // print("hereee");
                
                Ray ray = furnishing.transform.GetChild(1).GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit raycastHit))
                {
                    //print("hit!");
                   // print(raycastHit.transform.gameObject.tag);
                   // print(raycastHit.transform.gameObject.name);
                    if (raycastHit.transform.gameObject.tag == "furniture")
                    {
                       
                            print("jkasndaskdn");
                            if (pieceBeingAdded != null)
                            {
                                Destroy(pieceBeingAdded.transform.GetChild(0).GetComponent<Selector>());
                            }
                            pieceBeingAdded = raycastHit.transform.parent.gameObject;
                            raycastHit.collider.gameObject.AddComponent<Selector>();
                            deleteFurnitureMenu.SetActive(true);
                        changeDoorSliders.SetActive(false);
                        
                    }
                
                }
            }
        }
        if (isChangingDoor)
        {
            // print("is deletingggg");
            if (Input.GetMouseButtonDown(0))
            {
                
                Ray ray = furnishing.transform.GetChild(1).GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit raycastHit))
                {
                    
                    if (raycastHit.transform.gameObject.name == "Door(Clone)")
                    {
                        changeDoorSliders.SetActive(false);
                        if (!EventSystem.current.IsPointerOverGameObject())
                        {
                            print("jkasndaskdn");
                            if (doorBeingChanged == null)
                            {
                                doorBeingChanged = raycastHit.transform.parent.gameObject;
                                doorSelection.SetActive(true);

                            }
                        }
                        
                        
                    }

                }
            }
        }
    }
    public void confirmDeletingFurniture()
    {
        cancelSelectionToDelete.SetActive(false);
        print("confirm deletion");
        isDeleting = false;
        Destroy(furnishing);
        Destroy(pieceBeingAdded);
        Player.SetActive(true);
    }
    public void cancelDeletingFurniture()
    {
        cancelSelectionToDelete.SetActive(false);
        isDeleting = false;
        
       if(pieceBeingAdded!=null) Destroy(pieceBeingAdded.transform.GetChild(0).GetComponent<Selector>());
        Destroy(furnishing);
        Player.SetActive(true);
        changeDoorSliders.SetActive(false);
    }
  


}
