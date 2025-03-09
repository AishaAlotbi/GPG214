using System.IO;
using UnityEngine;

public class AssetBundleLoader : MonoBehaviour
{
    string folderPath = "AssetBundles";
    string FileName = "backgrounds";
    string combinedPath;
    
    
    private AssetBundle backgrounds;
    Vector3 foregroundPosition = new Vector3(22.05f, 0, 0);
    Vector3 backgroundPosition = new Vector3(22.05f, 0, 0);


    void Start()
    {
        LoadAssetBundles();
        LoadForeground();
        LoadBackground();

    }

  
    void LoadForeground()
    {
        if(backgrounds == null)
        {
            Debug.Log("Foreground Bundle Not Found");
            return;
        }

       GameObject foregroundPrefab = backgrounds.LoadAsset<GameObject>("Foreground");

        if(foregroundPrefab != null)
        {
            Instantiate(foregroundPrefab, foregroundPosition, Quaternion.identity);
            
        }

       
    }

    void LoadBackground()
    {
        if(backgrounds == null)
        {
            Debug.Log("Background Bundle Not Found");
            return;
        }

        GameObject backgroundPrefab = backgrounds.LoadAsset<GameObject>("Background");

        if (backgroundPrefab != null)
        {
            Instantiate(backgroundPrefab, foregroundPosition, Quaternion.identity);
            
        }
    }





    void LoadAssetBundles()
    {
        combinedPath = Path.Combine(Application.streamingAssetsPath, folderPath, FileName);

        if (File.Exists(combinedPath))
        {
            backgrounds = AssetBundle.LoadFromFile(combinedPath);
            backgrounds.LoadAllAssets();
            Debug.Log("Foreground Asset Bunlde Loaded");

        }
        else { Debug.Log("File does not exist " + combinedPath); }

        
    }

    


}
