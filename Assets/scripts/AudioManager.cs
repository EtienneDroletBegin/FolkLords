using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] Songs;
    [SerializeField]
    private AudioClip[] SFXs;

    Dictionary<string, Dictionary<string, AudioClip>> clipDict;
    [SerializeField]
    private AudioSource mainSpeaker;
    [SerializeField]
    private float latency = 0.1f;
    private AudioPool audioPool;
    private static AudioManager instance;

    #region SINGLETON
    public AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if (instance == null)
                {
                    instance = new GameObject("AudioManager").AddComponent<AudioManager>();

                }
            }
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);


        clipDict = new Dictionary<string, Dictionary<string, AudioClip>>();

        clipDict.Add("Songs", new Dictionary<string, AudioClip>());
        for (int i = 0; i < Songs.Length; i++)
        {

            if (Songs[i] != null)
            {

                AudioClip clip = Songs[i];
                clipDict["Songs"].Add(clip.name, clip);
                Debug.Log(clip.name);
            }

        }
        clipDict.Add("SFXs", new Dictionary<string, AudioClip>());
        for (int i = 0; i < SFXs.Length; i++)
        {

            if (SFXs[i] != null)
            {

                AudioClip clip = SFXs[i];
                clipDict["SFXs"].Add(clip.name, clip);
                Debug.Log(clip.name);

            }

        }
        audioPool = GetComponent<AudioPool>();
    }
   
    public static AudioManager GetInstance()
    {
        return instance;
    }
    #endregion
    public void PlaySongOnMain(string sound)
    {
        StartCoroutine(PlayWithDelay(sound));
    }
    public void PauseMusic()
    {
        mainSpeaker.Pause();
    }
    public void StopMainSpeaker()
    {
        mainSpeaker.Stop();
    }

    public AudioSource PlaySFX(Vector3 pos, string sound)
    {
        AudioSource audioSource = audioPool.GetAvailableAudio(pos);
        audioSource.PlayOneShot(GetClip("SFXs", sound));
        return audioSource;
    }

    private AudioClip GetClip(string type, string name)
    {
        if (clipDict.TryGetValue(type, out Dictionary<string, AudioClip> dict))
        {
            return dict[name];
        }
        else return null;
    }

    private IEnumerator PlayWithDelay(string sound)
    {

        yield return new WaitForSeconds(.5f);
        mainSpeaker.PlayOneShot(GetClip("Songs", sound), 0.2f);

    }
}
