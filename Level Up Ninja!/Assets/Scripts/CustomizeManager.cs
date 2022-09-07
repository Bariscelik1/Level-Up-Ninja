using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using mynamespace;
using UnityEngine.UI;
using System.Data.Common;

public class CustomizeManager : MonoBehaviour
{
    public List<ItemInfos> itemInfos = new List<ItemInfos>();
    DataManager dataManager = new DataManager();

    public AudioSource[] Buttonsounds;
    int buttonsoundIndex;


    [Header("UI elements")]
    public int itemIndex;
    public Text itemText;
    public Text itemCostText;
    public Text itemAdCostText;
    public Text gemCountText;
    public Text adCoinText;
    public Sprite[] items;
    public Button[] buttons;
    public Image itemImage;
    public GameObject equippedPanel;


    void Start()
    {
        dataManager.Load();
        itemInfos = dataManager.TransferList();

        //dataManager.Save(itemInfos);
        PlayerPrefs.SetInt("GemCount", 5000);
        PlayerPrefs.SetInt("AdCount", 0);

        adCoinText.text = PlayerPrefs.GetInt("AdCount").ToString();

        gemCountText.text = PlayerPrefs.GetInt("GemCount").ToString();
        itemText.text = itemInfos[0].itemName;
        itemIndex = 0;
        itemCostText.text = itemInfos[0].itemCost.ToString();
        itemImage.sprite = items[0];

        if (PlayerPrefs.GetInt("GemCount") < itemInfos[itemIndex].itemCost)
        {
            buttons[2].interactable = false;
        }
        if (itemInfos[0].buyState)
        {
            buttons[2].gameObject.SetActive(false);
            buttons[3].gameObject.SetActive(true);
        }
        


    }

    

    public void BackMenu()
    {
        dataManager.Save(itemInfos);
        SceneManager.LoadScene(0);
    }

    public void adWatch()
    {
        PlayerPrefs.SetInt("AdCount", PlayerPrefs.GetInt("AdCount") + 1);

    }


    public void Buy()
    {


        
        itemInfos[itemIndex].buyState = true;
        PlayerPrefs.SetInt("GemCount", PlayerPrefs.GetInt("GemCount") - itemInfos[itemIndex].itemCost);
        buttons[2].gameObject.SetActive(false);
        buttons[3].gameObject.SetActive(true);
        gemCountText.text = PlayerPrefs.GetInt("GemCount").ToString();



    }
    public void Equip()
    {
        PlayerPrefs.SetInt("ActiveDagger", itemIndex);
        equippedPanel.SetActive(true);       
    }

    public void PanelClose()
    {
        equippedPanel.SetActive(false);
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
    public void SpecialItem()
    {
        if (PlayerPrefs.GetInt("AdCount") >= itemInfos[itemIndex].itemCost)
        {
            itemInfos[itemIndex].buyState = true;
            PlayerPrefs.SetInt("AdCount", PlayerPrefs.GetInt("AdCount") - itemInfos[itemIndex].itemCost);
            buttons[4].gameObject.SetActive(false);
            buttons[2].gameObject.SetActive(false);
            buttons[3].gameObject.SetActive(true);
            adCoinText.text = PlayerPrefs.GetInt("AdCount").ToString();

        }
        else
        {

            //ödüllü reklam çalýþtýr
            PlayerPrefs.SetInt("AdCount", PlayerPrefs.GetInt("AdCount") + 1);
            adCoinText.text = PlayerPrefs.GetInt("AdCount").ToString();
            itemAdCostText.text = PlayerPrefs.GetInt("AdCount") + "/" + itemInfos[itemIndex].itemCost.ToString();
        }
        


    }


    public void Left_rightArrows(string direction)
    {
        if (direction == "right")
        {


            if (itemImage.enabled == false)
            {
                itemImage.enabled = true;
            }

            if (itemIndex == -1)
            {
                buttons[2].interactable = true;

                itemIndex = 0;
                itemText.text = itemInfos[itemIndex].itemName;
                itemImage.sprite = items[itemIndex];
                itemCostText.text = itemInfos[itemIndex].itemCost.ToString();
                itemAdCostText.text = PlayerPrefs.GetInt("AdCount") + "/" + itemInfos[itemIndex].itemCost.ToString();

                if (itemInfos[itemIndex].buyState)
                {
                    buttons[2].gameObject.SetActive(false);
                    buttons[3].gameObject.SetActive(true);
                }
                else
                {

                    if (PlayerPrefs.GetInt("GemCount") < itemInfos[itemIndex].itemCost)
                    {
                        buttons[2].interactable = false;
                    }
                    else
                    {
                        buttons[2].interactable = true;
                    }

                    if (itemIndex>5)
                    {
                        buttons[4].gameObject.SetActive(true);
                        buttons[3].gameObject.SetActive(false);
                    }
                    else
                    {
                        buttons[2].gameObject.SetActive(true);
                        buttons[3].gameObject.SetActive(false);
                    }
                    

                }

            }
            else
            {
                itemIndex++;
                itemText.text = itemInfos[itemIndex].itemName;
                itemImage.sprite = items[itemIndex];
                itemCostText.text = itemInfos[itemIndex].itemCost.ToString();
                itemAdCostText.text = PlayerPrefs.GetInt("AdCount") + "/" + itemInfos[itemIndex].itemCost.ToString();



                if (itemInfos[itemIndex].buyState)
                {
                    buttons[2].gameObject.SetActive(false);
                    buttons[3].gameObject.SetActive(true);
                }
                else
                {

                    if (PlayerPrefs.GetInt("GemCount") < itemInfos[itemIndex].itemCost)
                    {
                        buttons[2].interactable = false;
                    }
                    else
                    {
                        buttons[2].interactable = true;
                    }
                    if (itemIndex > 5)
                    {
                        buttons[4].gameObject.SetActive(true);
                        buttons[3].gameObject.SetActive(false);
                        buttons[2].gameObject.SetActive(false);
                    }
                    else
                    {
                        buttons[2].gameObject.SetActive(true);
                        buttons[3].gameObject.SetActive(false);
                        buttons[4].gameObject.SetActive(false);
                    }

                }

            }
            if (itemIndex == items.Length - 1)
            {
                buttons[1].interactable = false;
            }
            else
            {
                buttons[1].interactable = true;

            }
            if (itemIndex != -1)
            {
                buttons[0].interactable = true;
            }
        }
        if (direction == "left")
        {
            if (itemIndex != -1)
            {
                itemIndex--;
                if (itemIndex != -1)
                {
                    buttons[0].interactable = true;
                    itemText.text = itemInfos[itemIndex].itemName;
                    itemImage.sprite = items[itemIndex];
                    itemCostText.text = itemInfos[itemIndex].itemCost.ToString();
                    itemAdCostText.text = PlayerPrefs.GetInt("AdCount") + "/" +itemInfos[itemIndex].itemCost.ToString();

                    if (itemInfos[itemIndex].buyState)
                    {
                        buttons[2].gameObject.SetActive(false);
                        buttons[3].gameObject.SetActive(true);
                    }
                    else
                    {

                        if (PlayerPrefs.GetInt("GemCount") < itemInfos[itemIndex].itemCost)
                        {
                            buttons[2].interactable = false;
                        }
                        else
                        {
                            buttons[2].interactable = true;
                        }
                        if (itemIndex > 5)
                        {
                            buttons[4].gameObject.SetActive(true);
                            buttons[3].gameObject.SetActive(false);
                            buttons[2].gameObject.SetActive(false);
                        }
                        else
                        {
                            buttons[2].gameObject.SetActive(true);
                            buttons[3].gameObject.SetActive(false);
                            buttons[4].gameObject.SetActive(false);
                        }
                    }
                }
                else
                {
                    buttons[2].interactable = false;

                    buttons[0].interactable = false;
                    itemText.text = "No Dagger";
                    itemImage.enabled = false;
                    itemCostText.text = "";
                    buttons[3].gameObject.SetActive(false);

                }
            }
            else
            {
                buttons[2].interactable = false;
                buttons[0].interactable = false;
                itemText.text = "No Dagger";
                itemImage.enabled = false;
                itemCostText.text = "";
                buttons[3].gameObject.SetActive(false);
            }

            if (itemIndex != items.Length - 1)
            {
                buttons[1].interactable = true;
            }
        }
    }
}
