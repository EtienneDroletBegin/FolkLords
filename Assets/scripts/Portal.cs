using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private string destination;

    public void SetDestination(string _destination)
    {
        destination = _destination;
    }

    public void Teleport()
    {
        SceneManager.LoadScene(destination);
    }
}
