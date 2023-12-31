using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[CreateAssetMenu]
public class Encounters : ScriptableObject
{
    [SerializeField] public int index;
    [SerializeField] public Sprite background;
    [SerializeField] public Monsters[] monsters;
}
