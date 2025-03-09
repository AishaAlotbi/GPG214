using System.Collections;
using System.IO;
using Gamekit2D;
using UnityEngine;
using UnityEngine.Networking;


namespace AishaAlotbi
{

    public class AudioManager : MonoBehaviour
    {
        public string musicFileName = "newMusic.mp3";
        public string streamingAssetsFolderPath = Application.streamingAssetsPath;
        private BackgroundMusicPlayer backgroundMusicPlayer;

       void Start()
       {
            
            backgroundMusicPlayer = BackgroundMusicPlayer.Instance;
            LoadAndChangeMusic(musicFileName);
            

       }

       

        private void LoadAndChangeMusic(string fileName)
        {
            string filePath = Path.Combine(streamingAssetsFolderPath,fileName);
            //Debug.Log("File Path: " + filePath);

            if (File.Exists(filePath))
            {

                StartCoroutine(LoadAudioClip(filePath));
            }
            else
            {
                //Debug.LogError("File not found at path: " + filePath);
            }
           
                
 
        }

        IEnumerator LoadAudioClip(string fileName)
        {
            string filePath = Path.Combine(streamingAssetsFolderPath, fileName);
            UnityWebRequest audioRequest = UnityWebRequestMultimedia.GetAudioClip("file://" + filePath, AudioType.UNKNOWN);
            AsyncOperation downloadOperation = audioRequest.SendWebRequest();

            while (!downloadOperation.isDone)
            {
                //Debug.Log("Download Progress " + ((downloadOperation.progress / 1f) * 100) + "%");
                yield return null;
            }

            if (audioRequest.result == UnityWebRequest.Result.ConnectionError || audioRequest.result == UnityWebRequest.Result.ProtocolError)
            {
               // Debug.LogError("Error when downloading file");
                yield break;
            }
            
            AudioClip newClip = DownloadHandlerAudioClip.GetContent(audioRequest);
            backgroundMusicPlayer.PushClip(newClip);













        }

    }
}
