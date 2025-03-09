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
        public string ambientFileName = "newAmbient.mp3";
        public string streamingAssetsFolderPath = Application.streamingAssetsPath;
        private BackgroundMusicPlayer backgroundMusicPlayer;

        private void Start()
        {
            Debug.Log("AudioManager Start called");
            backgroundMusicPlayer = BackgroundMusicPlayer.Instance;
            LoadAndChangeMusic(musicFileName, true);
            LoadAndChangeMusic(ambientFileName, false);


        }

        

        private void LoadAndChangeMusic(string fileName, bool isMusic)
        {
            string filePath = Path.Combine(streamingAssetsFolderPath,fileName);
            Debug.Log("File Path: " + filePath);

            
            
                StartCoroutine(LoadAudioClip(filePath, isMusic));
           
                //Debug.LogError("File not found at path: " + filePath);
            
            
            
        }

        IEnumerator LoadAudioClip(string fileName, bool isMusic)
        {
            UnityWebRequest audioRequest = UnityWebRequestMultimedia.GetAudioClip("file://" + streamingAssetsFolderPath + "/" + fileName, AudioType.UNKNOWN);
            AsyncOperation downloadOperation = audioRequest.SendWebRequest();

            while (!downloadOperation.isDone)
            {
                Debug.Log("Download Progress " + ((downloadOperation.progress / 1f) * 100) + "%");
                yield return null;
            }

            if(audioRequest.result == UnityWebRequest.Result.ConnectionError || audioRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error when downloading file");
                yield break;
            }

            if(audioRequest.result == UnityWebRequest.Result.Success)
            {
                AudioClip newClip = DownloadHandlerAudioClip.GetContent(audioRequest);

                if (isMusic)
                {
                    backgroundMusicPlayer.ChangeMusic(newClip);
                    
                }
                else
                {
                    backgroundMusicPlayer.ChangeAmbient(newClip);
                }
            }
            else
            {
                Debug.LogError("Failed to load audio clip: " + audioRequest.error);
            }
        }

    }
}
