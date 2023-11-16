using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Upgrade : MonoBehaviour
{
    public GameData data;
    public UserData user;
    public int level;

    Image icon;
    Text textLevel;

    void Awake()
    {
        LoadUserData();
        //icon = GetComponentsInChildren<Image>()[1];
        Text[] texts = GetComponentsInChildren<Text>();
        textLevel = texts[0];
        
    }

    private void LateUpdate()
    {
        textLevel.text = data.statType.ToString()+ "  " + (level + 1);
    }

    public void OnClick()
    {
        switch (data.statType)
        {
            case GameData.StatType.Hp:
                //user.userHp = level + 1;
                break;
            case GameData.StatType.Def:
                user.userDef = level + 1;
                break;
            case GameData.StatType.Speed:
                user.userSpeed = level + 1;
                break;
            case GameData.StatType.Reloadspeed:
                user.userReloadspeed = level + 1;
                break;
        }
        level++;
        //SaveUserData();

        
    }

    public void SaveUserData()
    {
        FileStream file = new FileStream(Application.persistentDataPath + "/userdata.dat", FileMode.Create);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(file, user);
        file.Close();
        
    }

    public void LoadUserData()
    {
        if (File.Exists(Application.persistentDataPath + "/userdata.dat"))
        {
            FileStream file = new FileStream(Application.persistentDataPath + "/userdata.dat", FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            user = (UserData)binaryFormatter.Deserialize(file);
            file.Close();
        }
        else
        {
            user = new UserData();
        }
    }
    
}
