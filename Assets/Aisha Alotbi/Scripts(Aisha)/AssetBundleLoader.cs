using System.IO;
using UnityEngine;

namespace AishaAlotbi
{


    public class AssetBundleLoader : MonoBehaviour
    {
        string folderPath = "AssetBundles";
        string FileName = "backgrounds";
        string combinedPath;


        private AssetBundle backgrounds;

        Vector3 backgroundPosition = new Vector3(22.05f, 0, 0);


        void Start()
        {
            LoadAssetBundles();
            LoadBackground();

        }


        void LoadBackground()
        {
            if (backgrounds == null)
            {
                //Debug.Log("Background Bundle Not Found");
                return;
            }

            GameObject backgroundPrefab = backgrounds.LoadAsset<GameObject>("Background");

            if (backgroundPrefab != null)
            {
                Instantiate(backgroundPrefab, backgroundPosition, Quaternion.identity);

            }
        }


        void LoadAssetBundles()
        {
            combinedPath = Path.Combine(Application.streamingAssetsPath, folderPath, FileName);

            if (File.Exists(combinedPath))
            {
                backgrounds = AssetBundle.LoadFromFile(combinedPath);
                backgrounds.LoadAllAssets();
                // Debug.Log("Foreground Asset Bunlde Loaded");

            }
            // else { Debug.Log("File does not exist " + combinedPath); }


        }




    }
}
