using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    public GameData data;
    public int level;

    Image icon;
    Text textLevel;

    void Awake()
    {
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
                break;
            case GameData.StatType.Def:
                break;
            case GameData.StatType.Speed:
                break;
            case GameData.StatType.Reloadspeed:
                break;
        }
        level++;

        
    }

}
