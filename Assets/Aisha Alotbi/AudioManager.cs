using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace AishaAlotbi
{

    public class AudioManager : MonoBehaviour
    {
        public AudioSource CharacterVoiceAudio;
            
        public string audioFileName = "Footsteps.ogg";
        public string streamingAssetsFolderPath = Application.streamingAssetsPath;

        IEnumerator Start()
        {
            yield return StartCoroutine(LoadAudioClip());
        }


        IEnumerator LoadAudioClip()
        {
            string audioFilePath = Path.Combine(streamingAssetsFolderPath, audioFileName);


            if (File.Exists(audioFilePath))
            {
                UnityWebRequest audioClipRequest = UnityWebRequestMultimedia.GetAudioClip("file://"+ audioFilePath, AudioType.UNKNOWN);
                yield return audioClipRequest.SendWebRequest();

                if(audioClipRequest.result == UnityWebRequest.Result.Success)
                {
                    AudioClip newClip = DownloadHandlerAudioClip.GetContent(audioClipRequest);

                    CharacterVoiceAudio.clip = newClip;
                    CharacterVoiceAudio.Play();
                }
                else { Debug.Log("Failed to load clip " + audioClipRequest.error); }
               
                audioClipRequest.Dispose();
                
            }
            else { Debug.Log("File not found at path " + audioFilePath); }
          
            
            
        }
    }
}
