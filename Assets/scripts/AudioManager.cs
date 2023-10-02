using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource mainSpeaker;
    private AudioPool audioPool;
    private static AudioManager instance;


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
        audioPool = GetComponent<AudioPool>();
    }
    public static AudioManager GetInstance()
    {
        return instance;
    }


    private void Start()
    {
        
    }
}
