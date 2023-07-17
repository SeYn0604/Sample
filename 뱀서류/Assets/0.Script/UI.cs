using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI instance;
    [SerializeField] private BoxCollider2D[] boxColls;
    [SerializeField] private Slider sliderExp;
    [SerializeField] private Text txtTime;
    [SerializeField] private Text txtKillCount;
    [SerializeField] private Text txtLv;
    [SerializeField] private RectTransform canvas;
    [SerializeField] private Image hpimg;

    private float maxExp;
    private float exp;

    private int level = 0;
    private float timer = 0;  
    
    private int killCount = 0;

    //Sample Code
    private float[] exps;

    public void Awake()
    {
        exps = new float[100];
        for (int i = 0; i < exps.Length; i++)
        {
            exps[i] = 10 * i;
        }
    }
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
        instance = this;
        maxExp = exps[level];
        sliderExp.value = 0f;

        
         for (int i = 0; i < boxColls.Length; i++)
         {
             Vector2 v1 = canvas.sizeDelta;
             if (i < 2)
                 v1.y = 5;
             else
                 v1.x = 5;
             boxColls[i].size = v1;
         }
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
    public void SetHP(int HP, int maxHP)
    {
        hpimg.fillAmount = (float)HP / maxHP;
    }
}
