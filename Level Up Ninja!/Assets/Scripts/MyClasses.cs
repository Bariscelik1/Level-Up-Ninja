using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace mynamespace
{
    public class CustomizationData
    {

        public static List<ItemInfos> ItemInfos = new List<ItemInfos>();

    }
    [Serializable]
    public class ItemInfos
    {
        public int itemIndex;
        public string itemName;
        public int itemCost;
        public bool buyState;

    }

    public class DataManager
    {


        public void StartValues()
        {
            if (!PlayerPrefs.HasKey("LastLevel"))
            {
                PlayerPrefs.SetInt("LastLevel", 2);
                PlayerPrefs.SetInt("GemCount", 0);
                PlayerPrefs.SetInt("SoundOn", 1);
                PlayerPrefs.SetInt("ActiveDagger", 0);

            }

        }

        public void Save(List<ItemInfos> itemInfos)
        {

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenWrite(Application.persistentDataPath + "/ItemInfos.gd");
            bf.Serialize(file, itemInfos);
            file.Close();

        }       

        List<ItemInfos> itemInfosTemp;
        public void Load()
        {
            if (File.Exists(Application.persistentDataPath + "/ItemInfos.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/ItemInfos.gd", FileMode.Open);
                itemInfosTemp = (List<ItemInfos>)bf.Deserialize(file);
                file.Close();
                

            }

        }

        public List<ItemInfos> TransferList()
        {
            return itemInfosTemp;

        }

        public void FileCreate(List<ItemInfos> itemInfos)
        {

            if (!File.Exists(Application.persistentDataPath + "/ItemInfos.gd"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/ItemInfos.gd");
                bf.Serialize(file, itemInfos);
                file.Close();
            }

        }

    }

}


