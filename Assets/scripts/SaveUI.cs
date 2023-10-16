using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveUI : MonoBehaviour
{
    [SerializeField]
    private GameObject SaveSlots;
    // Start is called before the first frame update
    void Start()
    {
        SaveSystem.CheckFiles();
    }

    public void FileIndex(int index)
    {

        SaveSystem.SetFileIndex(index);
        Debug.Log(SaveSystem.GetPath());
    }
    public void SetFiles()
    {
        SaveSlots.SetActive(true);
    }
    public void PartySelect()
    {
        SceneManager.LoadScene("PartySelect");
    }
}
