using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Kakera
{
    public class ImageLoader : MonoBehaviour
    {
        [SerializeField]
        private Unimgpicker imagePicker;
      

        
        public static Texture2D tex;
        void Awake()
        {
            imagePicker.Completed += (string path) =>
            {
                StartCoroutine(LoadImage(path));
            };
        }

        public void OnPressShowPicker()
        {
            imagePicker.Show("Select Image", "unimgpicker");
        }

        private IEnumerator LoadImage(string path)
        {
            var url = "file://" + path;
            var unityWebRequestTexture = UnityWebRequestTexture.GetTexture(url);
            yield return unityWebRequestTexture.SendWebRequest();

            var texture = ((DownloadHandlerTexture)unityWebRequestTexture.downloadHandler).texture;
            if (texture == null)
            {
                Debug.LogError("Failed to load texture url:" + url);
            }
            else {
                gameObject.SetActive(false);
                tex = texture;
                

          }
        }
    }
}