using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableManager : MonoSingleton<AddressableManager>
{
    [SerializeField] bool useLocal = false;
    [SerializeField] bool useDelay = true;
 
    [SerializeField] UnityEvent OnFinishedLoad;

    [SerializeField] private AssetLabelReference soundAssetLabel;
    private Dictionary<string, AudioClip> soundBank = new Dictionary<string, AudioClip>();

    public List<AudioClip> soundList = new List<AudioClip>();

    private void Awake()
    {
        if(useLocal)
        {
            Utils.Delay(this, () =>
            {
                for (int i = 0; i < soundList.Count; i++)
                    AddClipToBank(soundList[i].name, soundList[i]);

                OnFinishedLoad?.Invoke();
            }, useDelay ? 1f : 0f);
        }
        else
        {
            LoadSoundAssets();
        }

    }

    public void LoadSoundAssets(UnityAction callback = null)
    {
        Addressables.LoadAssetsAsync<AudioClip>(soundAssetLabel, (clip) => 
        {
            AddClipToBank(clip.name, clip);
        })
        .Completed += (clips)=> 
        {
            if(clips.Status == AsyncOperationStatus.Succeeded)
            {
                callback?.Invoke();
                OnFinishedLoad?.Invoke();

                Debug.Log("Load Successful");
            }
            else
            {
                Debug.LogError("Load Failed!");
            }
        };
    }

    void AddClipToBank(string name, AudioClip clip)
    {
        if (!soundBank.ContainsKey(name))
            soundBank.Add(clip.name, clip);
    }

    public AudioClip GetAudioClip(string name) 
    {
        if (soundBank.ContainsKey(name))
            return soundBank[name];

        return null;
    }

    public static AudioClip GetClip(string name) 
    {
        return instance.GetAudioClip(name);
    }
}
