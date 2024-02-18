using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SnappingImage : MonoBehaviour
{
    // Start is called before the first frame update
   public static WebCamTexture webCamTexture;
    bool imageTaken = false;
    bool locationSelected = false;
    public GameObject placeholder;
    public GameObject discardButton;
    public GameObject nextButton;
    public GameObject changeLocation;
    public RawImage displayImage;



    IEnumerator Start()
    {
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        findWebCams();


        if(Application.HasUserAuthorization(UserAuthorization.WebCam)){
        webCamTexture = new WebCamTexture(WebCamTexture.devices[0].name, 2592, 1944);
       webCamTexture.filterMode = FilterMode.Trilinear;
        //displayImage.material.mainTexture = webCamTexture;
        

        webCamTexture.Play();
        }
        
    }
    void findWebCams()
    {
        foreach (var device in WebCamTexture.devices)
        {
            Debug.Log("Name: " + device.name);
        }
    }
    void Update()
    {
        if(webCamTexture!=null){
        if (webCamTexture.width > 100)
        {

            //  Debug.Log
           

            if (!imageTaken)
            {
               displayImage.GetComponent<RawImage>().texture = webCamTexture;
            }
            else
            {
                locationSelected = true;
                discardButton.SetActive(true);
                nextButton.SetActive(true);
               // changeLocation.SetActive(true);
            }
        }
        }  
    }
    public void takeImage()
    {
        webCamTexture.Pause();
        imageTaken = true;
        print(webCamTexture.width);
        print(webCamTexture.height);

    }
    public void restartCamera()
    {
        webCamTexture.Play();
        imageTaken = false;
        locationSelected = false;

    }
    public void selectAnotherLocation()
    {
        locationSelected = false;
    }
}
