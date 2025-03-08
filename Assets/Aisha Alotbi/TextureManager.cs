using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
namespace AishaAlotbi
{


    public class TextureManager : MonoBehaviour
    {

        public string textureFileName;
        public string streamingAssetsFolderPath = Application.streamingAssetsPath;

        IEnumerator Start()
        {
            yield return StartCoroutine(LoadNewTexture());
        }

        IEnumerator LoadNewTexture()
        {
            string textureFilePath = Path.Combine(streamingAssetsFolderPath, textureFileName);
            UnityWebRequest imageRequest = UnityWebRequestTexture.GetTexture("file://" + textureFilePath);

            yield return imageRequest.SendWebRequest();

            if (imageRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to load texture: " + imageRequest.error);
                yield break;
            }



            Texture2D newTexture = DownloadHandlerTexture.GetContent(imageRequest);
            SpriteRenderer spriteImage = GetComponent<SpriteRenderer>();

            Sprite newSprite = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), transform.position);
            spriteImage.sprite = newSprite;

            imageRequest.Dispose();
            yield return null;
        }
    }
}