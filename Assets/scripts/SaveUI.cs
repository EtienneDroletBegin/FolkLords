using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveUI : MonoBehaviour
{
    [SerializeField]
    private GameObject SaveSlots;

    [SerializeField]
    private Sprite emptySlot;
    
    


    // Start is called before the first frame update
    void Start()
    {
        SaveSystem.CheckFiles();
        SetSaveImages();
    }

    public void FileIndex(int index)
    {

        SaveSystem.SetFileIndex(index);
        
        

    }
    public void LoadGame()
    {
        StartCoroutine(LoadScene());
    }
    private IEnumerator LoadScene()
    {
        SceneManager.LoadScene("OverWorld");
        yield return new WaitForEndOfFrame();
        PartyManager.GetInstance().ConfirmPartyChange();
    }
    public void SetFiles()
    {
        SaveSlots.SetActive(true);
    }
    public void DeleteFile(int index)
    {
        SaveSystem.DeleteFile(index);
        SetSaveImages();

    }
    
    public void PartySelect()
    {
        SceneManager.LoadScene("PartySelect");
    }

    private void SetSaveImages()
    {

        for(int i = 0; i < 3; i++)
        {
            SaveSystem.SetFileIndex(i);
            SaveData saveData = SaveSystem.load();
            Debug.Log(saveData);
            if(saveData != null)
            {
                GameObject currentSlot = SaveSlots.transform.GetChild(i).gameObject;
                currentSlot.SetActive(true);
                for(int j = 0; j < 3; j++)
                {

                    Image currentPortrait = currentSlot.transform.GetChild(j).GetComponent<Image>();
                    currentPortrait.sprite = saveData.ActiveParty[j].Portrait;
                    currentPortrait.gameObject.SetActive(true);
                }

            }
            else
            {
                GameObject currentSlot = SaveSlots.transform.GetChild(i).gameObject;
                currentSlot.GetComponent<Image>().sprite = emptySlot;
                for (int j = 0; j < 3; j++)
                {

                    Image currentPortrait = currentSlot.transform.GetChild(j).GetComponent<Image>();
                    currentPortrait.sprite = null;
                    currentPortrait.gameObject.SetActive(false);
                }
            }

        }
    }
}
