using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Slider sliderExp;
    [SerializeField] private Text txtTime;
    [SerializeField] private Text txtKillCount;
    [SerializeField] private Text txtLv;

    private float maxExp;
    private float exp;

    private int level = 0;
    private float timer = 0;  
    
    private int killCount = 0;  
    
    //Sample Code
    private float[] exps = { 100f, 200f, 300f, 400f, 500f };
    public float Exp
    {
        get { return exp; }
        set
        {
            exp = value;
            sliderExp.value = exp / maxExp;

            if(exp >= maxExp)
            {
                level++;
                maxExp = exps[level];
                sliderExp.value = 0f;
                exp = 0f;

                txtLv.text = $"Lv.{level + 1}";
            }
        }
    }

    public int KillCount
    {
        get { return killCount; }
        set
        {
            killCount = value;
            txtKillCount.text = $"{killCount}";
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        maxExp = exps[level];
        sliderExp.value = 0f;

    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.F1))
        {
            Exp += 1f;
        }

        timer += Time.deltaTime;
        System.TimeSpan ts = System.TimeSpan.FromSeconds(timer);
        txtTime.text = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
    }
}
