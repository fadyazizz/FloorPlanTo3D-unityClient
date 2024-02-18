using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
public class Analyze : MonoBehaviour
{ 
    // Start is called before the first frame update
    public GameObject loadingMenu;
    public GameObject errorMenu;
    public GameObject errorMenuLoading;
    public static string data;
    public GameObject startGameMenu;
    UnityWebRequest res;
    public GameObject tips;
    bool loaded;
   public void sendToServer()

    {
       
      StartCoroutine(Upload(false));
    }
    public void sendToServerLoadedImage()
    {
        StartCoroutine(Upload(true));

    }

    // Update is called once per frame
    IEnumerator Upload(bool isLoaded)
    {
        loaded = isLoaded;
        loadingMenu.SetActive(true);
        tips.SetActive(true);
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        Texture2D snap;
        if (!isLoaded)
        {
            WebCamTexture snappedImage = SnappingImage.webCamTexture;
            snap = new Texture2D(snappedImage.width, snappedImage.height, TextureFormat.RGB24, false);
            snap.SetPixels(snappedImage.GetPixels());
            snap.Apply();
        }
        else
        {
            Texture2D texture = Kakera.ImageLoader.tex;
            snap = new Texture2D(texture.width, texture.height, TextureFormat.RGB24, false);
            snap.SetPixels(texture.GetPixels());
            snap.Apply();
        }
        
       
        
        
       
       


        byte[] bytes = snap.EncodeToPNG();
        formData.Add(new MultipartFormFileSection("image",bytes, "F1_original.png", "image/png"));

        UnityWebRequest www = UnityWebRequest.Post("http://localhost:5000", formData);
        
        yield return www.SendWebRequest();
        res = www;

        
    }
    private void Update()
    {
        if (res != null && MenuFunc.tipsDone)
        {

            if (res.isNetworkError ||res.isHttpError)
            {
                loadingMenu.SetActive(false);
                if (loaded)
                {
                    errorMenuLoading.SetActive(true);
                }
                else
                {
                    errorMenu.SetActive(true);
                }

               // Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                data = res.downloadHandler.text;
                startGameMenu.SetActive(true);
                gameObject.SetActive(false);
                tips.SetActive(false);
            }
            res = null;
        }
    }
    public  void LoadScene()
    {
        // SceneManager.LoadScene("Game");
        GameObject builder = new GameObject("Ya3m ana 3omdaaaaaaa");
        builder.AddComponent<Builder>();
    }
}