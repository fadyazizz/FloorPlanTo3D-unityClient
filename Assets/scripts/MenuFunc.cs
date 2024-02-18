using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
public class MenuFunc : MonoBehaviour
{
   public GameObject p;
    public  Camera mainCamera;
    public  Camera topCamera;
    public Button okButton;
    public Button retryButton;
    public InputField textField;
    public GameObject lineDrawer;
    public GameObject plane;
    public GameObject hamburgerbutton;
    public GameObject okButtonSpawner;
    public GameObject retryButtonSpawner;
    public static bool okButtonIsPressed;
    public Button spawnButton;
    public Camera painterCamera;
    public GameObject player;
   
    public void Quit()
    {
        print("quitingggg");
        Application.Quit();
    }
    public void spawn()
    {
        switchToTopCamera();
    }
    public  void LoadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void hamburger()
    {
       
        p.SetActive(!p.activeSelf);
       
    }
    public  void switchToTopCamera()
    {

        mainCamera.enabled = false;
        topCamera.enabled = true;
    }
    public  void switchToMainCamera()
    {

        mainCamera.enabled = true;
        topCamera.enabled = false;
    }
    public void  restartLineDrawer()
    {
        lineDrawer.GetComponent<LineRenderer>().SetPosition(0, new Vector3(0, 0, 0));
        lineDrawer.GetComponent<LineRenderer>().SetPosition(1, new Vector3(0, 0, 0));
        lineDrawer.GetComponent<LineDrawer>().enabled = true;
    } 
    public void retry()
    {
        okButton.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        textField.gameObject.SetActive(false);
        lineDrawer.GetComponent<LineRenderer>().SetPosition(0,new Vector3(0,0,0));
        lineDrawer.GetComponent<LineRenderer>().SetPosition(1,new Vector3(0,0,0));
        lineDrawer.GetComponent<LineDrawer>().enabled = true;
        
    }
    public void retrySpawner()
    {
        okButtonSpawner.SetActive(false);
        retryButtonSpawner.SetActive(false);
        GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>().postionSelected = false;
    }
    public void moveCameraAndPlane()
    {
        float x = topCamera.transform.position.x;
        float y = topCamera.transform.position.y;
        float z = topCamera.transform.position.z;
        x = x * (1 / Builder.originalScale) * (Builder.yScale);
        z = z * (1 / Builder.originalScale) * (Builder.xScale);

        topCamera.transform.position = new Vector3(x, y, z);
        x = plane.transform.position.x;
        y = plane.transform.position.y;
        z = plane.transform.position.z;
        x = x * (1 / Builder.originalScale) * (Builder.yScale);
        z = z * (1 / Builder.originalScale) * (Builder.xScale);
        plane.transform.position = new Vector3(x, y, z);
    }
    public void settingScale()
    {
        hamburgerbutton.GetComponent<Button>().enabled = false;
    }
    public void cancel()
    {
        okButton.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        textField.gameObject.SetActive(false);
        lineDrawer.gameObject.SetActive(false);
        okButtonSpawner.SetActive(false);
        retryButtonSpawner.SetActive(false);
        switchToMainCamera();
        moveCameraAndPlane();
        //Spawner spawnerComponent = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();
        //spawnerComponent.postionSelected = false;
        //spawnerComponent.gameObject.SetActive(false);
        hamburgerbutton.GetComponent<Button>().enabled = true;
    }
    public void ok()
    {
        print(Builder.xScale);
        float distanceInGame = LineDrawer.getDistance();
        float distanceInReality = float.Parse(textField.text);
      //  print(distanceInReality);
       // print(distanceInGame);
        
        distanceInGame = distanceInGame * (1 / Builder.xScale);
        print(distanceInGame);
        Builder.xScale = distanceInReality/distanceInGame;
        Builder.yScale = distanceInReality / distanceInGame;
        //delete all wallPaper
        GameObject[] wallPaperFront = GameObject.FindGameObjectsWithTag("wallPaperr");
        for(int i = 0; i < wallPaperFront.Length; i++)
        {
            Destroy(wallPaperFront[i]);
        }
        GameObject[] walls= GameObject.FindGameObjectsWithTag("wall");
        

        for(int i = 0; i < walls.Length; i++)
        {
            GameObject parent = walls[i];
            GameObject child = parent.transform.GetChild(0).gameObject;

            WallMesh wallmesh = parent.GetComponent<WallMesh>();
            Vector3 scale= wallmesh.getScale();
            Vector3 coordinates = wallmesh.getCoordinates();
            parent.transform.position = coordinates;
            child.transform.localScale = scale;
            wallmesh.addWallPaperPoints(child);
            
            
           
        }
        GameObject[] doors= GameObject.FindGameObjectsWithTag("door");
        for (int i = 0; i < doors.Length; i++)
        {
            GameObject parent = doors[i];
            
            GameObject child = parent.transform.GetChild(0).gameObject;

            Door door = parent.GetComponent<Door>();
            Vector3 scale = door.getScale();
            Vector3 coordinates =door.getCoordinates();
            parent.transform.position = coordinates;
            child.transform.localScale = scale;



        }
        GameObject[] windows = GameObject.FindGameObjectsWithTag("window");
        for (int i = 0; i < windows.Length; i++)
        {
            GameObject parent = windows[i];

            GameObject child = parent.transform.GetChild(0).gameObject;

            Window window = parent.GetComponent<Window>();
            Vector3 scale = window.getScale();
            Vector3 coordinates = window.getCoordinates();
            parent.transform.position = coordinates;
            child.transform.localScale = scale;




        }
        GameObject[] fillers = GameObject.FindGameObjectsWithTag("wall1");
        for (int i = 0; i < fillers.Length; i++)
        {
            GameObject parent = fillers[i];
            GameObject child = parent.transform.GetChild(0).gameObject;

            WallMesh wallmesh = parent.GetComponent<WallMesh>();
            Vector3 scale = wallmesh.getScale();
            Vector3 coordinates = wallmesh.getCoordinates();
            parent.transform.position = coordinates;
            child.transform.localScale = scale;
            wallmesh.addWallPaperPoints(child);



        }
       

            okButton.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        textField.gameObject.SetActive(false);
        lineDrawer.gameObject.SetActive(false);
        switchToMainCamera();
        moveCameraAndPlane();
        hamburgerbutton.GetComponent<Button>().enabled = true;
        StartGame();

    }
    public void resetScale()
    {
        float camerScale = Builder.xScale;
        
        Builder.xScale = Builder.originalScale;
        Builder.yScale = Builder.originalScale;
        GameObject[] wallPaperFront = GameObject.FindGameObjectsWithTag("wallPaperr");
        for (int i = 0; i < wallPaperFront.Length; i++)
        {
            Destroy(wallPaperFront[i]);
        }
        GameObject[] walls = GameObject.FindGameObjectsWithTag("wall");
        for (int i = 0; i < walls.Length; i++)
        {
            GameObject parent = walls[i];
            GameObject child = parent.transform.GetChild(0).gameObject;

            WallMesh wallmesh = parent.GetComponent<WallMesh>();
            Vector3 scale = wallmesh.getScale();
            Vector3 coordinates = wallmesh.getCoordinates();
            parent.transform.position = coordinates;
            child.transform.localScale = scale;
            wallmesh.addWallPaperPoints(child);


        }
        GameObject[] doors = GameObject.FindGameObjectsWithTag("door");
        for (int i = 0; i < doors.Length; i++)
        {
            GameObject parent = doors[i];

            GameObject child = parent.transform.GetChild(0).gameObject;

            Door door = parent.GetComponent<Door>();
            Vector3 scale = door.getScale();
            Vector3 coordinates = door.getCoordinates();
            parent.transform.position = coordinates;
            child.transform.localScale = scale;



        }
        GameObject[] windows = GameObject.FindGameObjectsWithTag("window");
        for (int i = 0; i < windows.Length; i++)
        {
            GameObject parent = windows[i];

            GameObject child = parent.transform.GetChild(0).gameObject;

            Window window = parent.GetComponent<Window>();
            Vector3 scale = window.getScale();
            Vector3 coordinates = window.getCoordinates();
            parent.transform.position = coordinates;
            child.transform.localScale = scale;



        }
        GameObject[] fillers = GameObject.FindGameObjectsWithTag("wall1");
        for (int i = 0; i < fillers.Length; i++)
        {
            GameObject parent = fillers[i];
            GameObject child = parent.transform.GetChild(0).gameObject;

            WallMesh wallmesh = parent.GetComponent<WallMesh>();
            Vector3 scale = wallmesh.getScale();
            Vector3 coordinates = wallmesh.getCoordinates();
            parent.transform.position = coordinates;
            child.transform.localScale = scale;
            wallmesh.addWallPaperPoints(child);
        }

        float x = topCamera.transform.position.x;
        float y = topCamera.transform.position.y;
        float z = topCamera.transform.position.z;
        x = x * (1 / camerScale) * (Builder.originalScale);
        z = z * (1 / camerScale) * (Builder.originalScale);

        topCamera.transform.position = new Vector3(x, y, z);
        x = plane.transform.position.x;
        y = plane.transform.position.y;
        z = plane.transform.position.z;
        x = x * (1 / camerScale) * (Builder.originalScale);
        z = z * (1 / camerScale) * (Builder.originalScale);
        plane.transform.position = new Vector3(x, y, z);
        StartGame();

    }
   public  void sayOkToPosition()
    {
        //GameObject player = GameObject.FindGameObjectWithTag("Player");
        switchToMainCamera();
        Spawner spawnerComponent= GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();
        //player.transform.position = spawnerComponent.position;
       okButtonIsPressed = true;
        okButtonSpawner.SetActive(false);
        retryButtonSpawner.SetActive(false);
        
       
    }
    public void StartGame()
    {
        spawnButton.onClick.Invoke();
    }
    public void Customize()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<CanCustomize>().enabled = true;
    }
    public void CancelCustomize()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<CanCustomize>().enabled = false;

    }
 
    public void changeToPainterCamera()
    {
        Vector3 playerPosition = player.transform.position;
        float playerYRotation = player.transform.rotation.eulerAngles.y;
   
        painterCamera.transform.rotation = Quaternion.Euler(90, playerYRotation, 0);
        painterCamera.transform.position = new Vector3(playerPosition.x, 6.5f, playerPosition.z);
        GameObject painter = GameObject.Find("Painter");
      
        global_selection s = painter.GetComponent<global_selection>();
        s.enabled = true;


    }
    public void setIsPaintingToFalse()
    {
        selected_dictionary s = GameObject.Find("Painter").GetComponent<selected_dictionary>();
        s.isPainting = false;

    }
    public void deleteProvisional()
    {
        selected_dictionary s = GameObject.Find("Painter").GetComponent<selected_dictionary>();
        s.DestroyProvisional();

    }
    public void clearProvisional()
    {
        selected_dictionary s = GameObject.Find("Painter").GetComponent<selected_dictionary>();
        s.clearProvisional();
    }
    public void addFurniturePiece()
    {
        GameObject inventorysystem = GameObject.Find("inventorySystem");
        inventorysystem.GetComponent<Inventory>().confirmAddingPiece();


    }
    public void cancelAddingFurniture()
    {
        GameObject inventorysystem = GameObject.Find("inventorySystem");
        inventorysystem.GetComponent<Inventory>().cancel();
    }
    public void deleteFurniture()
    {
        GameObject inventorysystem = GameObject.Find("inventorySystem");
        inventorysystem.GetComponent<Inventory>().delete();
    }
    public void confirmDeletingFurniture()
    {
        print("confirminggg");
        GameObject inventorysystem = GameObject.Find("inventorySystem");
        inventorysystem.GetComponent<Inventory>().confirmDeletingFurniture();
    }
    public void cancelDeletingFurniture()
    {
        GameObject inventorysystem = GameObject.Find("inventorySystem");
        inventorysystem.GetComponent<Inventory>().cancelDeletingFurniture();
    }
    public void changeDoor()
    {
        GameObject inventorysystem = GameObject.Find("inventorySystem");
        inventorysystem.GetComponent<Inventory>().changeDoor();
    }
   public void cancelChangedoor()
    {
        GameObject inventorysystem = GameObject.Find("inventorySystem");
        inventorysystem.GetComponent<Inventory>().cancelChangeDoor();
    }
    public static bool tipsDone = false;
    public void tipsAreDone()
    {
        tipsDone = true;
    }
    public void changeControlSpeed(float newControlSpeed)
    {
        GameObject player = GameObject.Find("FPSController");
        print("player");
        print(player != null);
        
       if(player!=null) player.GetComponent<FirstPersonController>().sliderSpeedMultiplier = newControlSpeed;
        GameObject furnishing = GameObject.Find("Furnishing(Clone)");
        print("furnishing");
        print(furnishing != null);
        if (furnishing != null) furnishing.GetComponent<FirstPersonController>().sliderSpeedMultiplier = newControlSpeed;
    }
    public void changeLookAroundSpeed(float newControlSpeed)
    {
        GameObject player = GameObject.Find("FPSController");
        if (player != null) player.GetComponent<FirstPersonController>().sliderSpeedLookAroundMultiplier = newControlSpeed;

        GameObject furnishing = GameObject.Find("Furnishing(Clone)");
        if (furnishing != null) furnishing.GetComponent<FirstPersonController>().sliderSpeedLookAroundMultiplier = newControlSpeed;

    }
    public void teleport()
    {
        Teleport.teleport = true;
    }
    public static bool paintedFurnished = false;
   public void paintFurniture()
    {
        paintedFurnished = true;
    }
    public GameObject warningMenu;
    public GameObject warningReset;
    public Button scaleButton;
    public void checkPaintFurniture()
    {
        if (paintedFurnished)
        {
            warningMenu.SetActive(true);
        }
        else
        {
            scaleButton.gameObject.SetActive(true);
            scaleButton.onClick.Invoke();
            scaleButton.gameObject.SetActive(false);
        }
    }
    public void proceedWithPaint()
    {
        paintedFurnished = false;
        GameObject[] paintObjs = GameObject.FindGameObjectsWithTag("paint");
        for (int i = 0; i < paintObjs.Length; i++)
        {
            Destroy(paintObjs[i]);


        }
        GameObject[] furniture = GameObject.FindGameObjectsWithTag("furniture");
        for (int i = 0; i < furniture.Length; i++)
        {
            Destroy(furniture[i]);


        }
        scaleButton.onClick.Invoke();
    }
    public Button reset;
    public void checkPaintFurnitureReset()
    {
        if (paintedFurnished)
        {
            warningReset.SetActive(true);
        }
        else
        {
           
            reset.onClick.Invoke();
           
        }
    }
    public void proceedWithPaintReset()
    {
        paintedFurnished = false;
        GameObject[] paintObjs = GameObject.FindGameObjectsWithTag("paint");
        for (int i = 0; i < paintObjs.Length; i++)
        {
            Destroy(paintObjs[i]);


        }
        GameObject[] furniture = GameObject.FindGameObjectsWithTag("furniture");
        for (int i = 0; i < furniture.Length; i++)
        {
            Destroy(furniture[i]);


        }
        
        reset.onClick.Invoke();
        
    }
}
