using mynamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Sound")]
    public List<Sprite> soundImage;
    
    

    public GameObject quitPanel;
    public List<Button> buttonList;
    public AudioSource[] Buttonsounds;
    int buttonsoundIndex;





    public List<ItemInfos> itemInfos = new List<ItemInfos>();
    DataManager dataManager = new DataManager();

    void Start()
    {
        if (Time.timeScale==0)
        {
            Time.timeScale = 1;
        }


        SoundOnOff();

        dataManager.StartValues();
        dataManager.FileCreate(itemInfos); // en son aktifleþtir.
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SoundOnOff()
    {
        if (PlayerPrefs.GetInt("SoundOn")==1)
        {
            AudioListener.volume = 1f;
            buttonList[0].image.sprite = soundImage[1];
            PlayerPrefs.SetInt("SoundOn", 1);
        }
        else if (PlayerPrefs.GetInt("SoundOn") == 0)
        {
            AudioListener.volume = 0f;
            buttonList[0].image.sprite = soundImage[0];
            PlayerPrefs.SetInt("SoundOn", 0);
        }
    }

    public void PlaySound()
    {
        Buttonsounds[buttonsoundIndex].Play();
        buttonsoundIndex++;

        if (buttonsoundIndex == Buttonsounds.Length - 1)
        {
            buttonsoundIndex = 0;
        }

    }

    public void Quit(string quit)
    {
        switch (quit)
        {
            case "quitbutton":
                quitPanel.SetActive(true);
                break;

            case "yes":
                Application.Quit();
                break;

            case "no":
                quitPanel.SetActive(false);
                break;
        }
        
    }
    public void Play()
    {

        SceneManager.LoadScene(PlayerPrefs.GetInt("LastLevel"));
    }

    public void CustomizeButton()
    {
        SceneManager.LoadScene(1);

    }
}
