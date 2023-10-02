using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPool : MonoBehaviour
{
    private List<AudioSource> audioPool = new List<AudioSource>();

    public AudioSource GetAvailableAudio(Vector3 pos)
    {
        foreach (AudioSource current in audioPool)
        {
            if (current == null)
            {
                audioPool.Remove(current);
                break;
            }
            if (!current.isPlaying)
            {
                current.volume = .1f;
                current.transform.position = pos;
                return current;
            }
        }
        AudioSource newSource = new GameObject().AddComponent<AudioSource>();
        newSource.volume = .1f;
        newSource.transform.position = pos;
        audioPool.Add(newSource);
        return newSource;
    }

}