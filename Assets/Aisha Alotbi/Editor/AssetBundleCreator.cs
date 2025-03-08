using UnityEngine;
using UnityEditor;
using System.IO;

namespace AishaAlotbi
{
    public class AssetBundleCreator
    {
        [MenuItem("Assets/Build AssetBundles")]
        static void BuildAssetBundles()
        {
            string assetBundleDirectory = "Assets/AssetBundles";

            if (!Directory.Exists(assetBundleDirectory))
            {
                Directory.CreateDirectory(assetBundleDirectory);
            }

            BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
        }

    }
}
