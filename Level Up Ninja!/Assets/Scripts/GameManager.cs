using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public TMP_Text playerLevel_text;
    public List<Text> texts;
    public List<Button> Buttons;
    public List<GameObject> Panels;


    public GameObject[] Rightdaggers;
    public GameObject[] Leftdaggers;

    public int playerLevel;
    public int diamondCount;

    [Header("Sound")]
    public List<AudioSource> sounds;
    public List<AudioSource> Gemsounds;
    public List<AudioSource> Buttonsounds;
    public int soundIndex;
    public int gemSoundIndex;
    public int buttonsoundIndex;   
    public AudioSource hurtSound;
    public AudioSource winSound;
    public AudioSource clickSound;
    public List<Sprite> soundImage;



    private void Awake()
    {
        Rightdaggers[PlayerPrefs.GetInt("ActiveDagger")].SetActive(true);
        Leftdaggers[PlayerPrefs.GetInt("ActiveDagger")].SetActive(true);
    }

    void Start()
    {
        
        playerLevel = 1;

        soundIndex = 0;

        if (Time.timeScale==0)
        {
            Time.timeScale = 1;
        }        
        
        diamondCount = 0;
        texts[2].text = diamondCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playerLevel_textChange()
    {
        playerLevel_text.text = ("Level" +" "+ playerLevel);
    }

    public void diamondChange()
    {
        
        diamondCount++;       
        
        texts[2].text = diamondCount.ToString();
    }

    public void PlaySound()
    {
        Buttonsounds[buttonsoundIndex].Play();
        buttonsoundIndex++;

        if (buttonsoundIndex == Buttonsounds.Count - 1)
        {
            buttonsoundIndex = 0;
        }

    }

    public void GoMainMenu()
    {
        
        SceneManager.LoadScene(0);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        clickSound.Play();
        if (Time.timeScale==0)
        {
            Time.timeScale = 1;

        }
        
    }
    public void Settings()
    {
        clickSound.Play();
        if (PlayerPrefs.GetInt("SoundOn") == 0)
        {
            
            Buttons[2].image.sprite = soundImage[0];
            
        }
        else if (PlayerPrefs.GetInt("SoundOn") == 1)
        {
            
            Buttons[2].image.sprite = soundImage[1];
            
        }

        Panels[3].SetActive(true);
        Time.timeScale = 0;

        
    }

    public void Revive()
    {
        Panels[0].SetActive(false);
        Panels[2].SetActive(true);
        Time.timeScale = 1;

    }

    public void Cancel()
    {
        Panels[3].SetActive(false);
        clickSound.Play();
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;

        }
    }

    public void Lose()
    {
        Panels[0].SetActive(true);
        Panels[2].SetActive(false);
        hurtSound.Play();
        PlayerPrefs.SetInt("Diamond", (PlayerPrefs.GetInt("Diamond") + diamondCount));
        texts[0].text = PlayerPrefs.GetInt("Diamond").ToString();
        Time.timeScale = 0;
    }
    public void Win()
    {
        Panels[1].SetActive(true);
        Panels[2].SetActive(false);
        winSound.Play();
        PlayerPrefs.SetInt("Diamond", (PlayerPrefs.GetInt("Diamond") + diamondCount));
        texts[1].text = PlayerPrefs.GetInt("Diamond").ToString();
        Time.timeScale = 0;
    }

    public void SoundOnOff()
    {
        if (PlayerPrefs.GetInt("SoundOn") == 0)
        {
            AudioListener.volume = 1f;
            Buttons[2].image.sprite = soundImage[1];
            PlayerPrefs.SetInt("SoundOn", 1);
        }
        else if (PlayerPrefs.GetInt("SoundOn") == 1)
        {
            AudioListener.volume = 0f;
            Buttons[2].image.sprite = soundImage[0];
            PlayerPrefs.SetInt("SoundOn", 0);
        }
    }

    

}
