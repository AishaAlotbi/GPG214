using System;
using System.Collections;
using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace AishaAlotbi
{


    public class TextureManager : MonoBehaviour
    {

        public string shipSpriteFileName = "newShipSprite.png";
        public string streamingAssetsFolderPath = Application.streamingAssetsPath;
        private SpriteRenderer shipSpriteRenderer;
        public GameObject foreground;
        


        private void Start()
        {
           
            shipSpriteRenderer = foreground.transform.Find("Dropship").GetComponent<SpriteRenderer>(); 


            LoadAndChangeSprite(shipSpriteFileName);
        }

        private void LoadAndChangeSprite(string fileName)
        {
            string filePath = Path.Combine(streamingAssetsFolderPath, fileName);
            //Debug.Log("Sprite file path: " + filePath);

            if (File.Exists(filePath)) 
            {
                shipSpriteRenderer = foreground.transform.Find("Dropship").GetComponent<SpriteRenderer>();
                StartCoroutine(LoadSprite(filePath));
            }
            //else { Debug.Log("Sprite file not found");}
        }

        IEnumerator LoadSprite(string filePath)
        {
            UnityWebRequest textureRequest = UnityWebRequestTexture.GetTexture("file://" + filePath);
            AsyncOperation downloadOperation = textureRequest.SendWebRequest();

            while (!downloadOperation.isDone)
            {
                //Debug.Log("Download Progress " + ((downloadOperation.progress / 1f) * 100) + "%");
                yield return null;
            }

            if (textureRequest.result == UnityWebRequest.Result.ConnectionError || textureRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                //Debug.LogError("Error when downloading file");
                yield break;
            }

            Texture2D texture = DownloadHandlerTexture.GetContent(textureRequest);
            Sprite newSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

            if (shipSpriteRenderer != null)
            {

                shipSpriteRenderer.sprite = newSprite;
               
            }

           

        }
    }   

}