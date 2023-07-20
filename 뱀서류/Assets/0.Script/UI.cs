using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    Play,
    Pause,
    Stop
}
[System.Serializable]
public class UpgradeData
{
    public Sprite sprite;
    public string title;
    public string description1;
    public string description2;
}
public class UI : MonoBehaviour
{
    public static UI instance;
    [HideInInspector] public GameState gameState = GameState.Stop;
    [SerializeField] private UpgradeData[] upData;
    [SerializeField] private BoxCollider2D[] boxColls;
    [SerializeField] private Slider sliderExp;
    [SerializeField] private Text txtTime;
    [SerializeField] private Text txtKillCount;
    [SerializeField] private Text txtLv;
    [SerializeField] private RectTransform canvas;
    [SerializeField] private Transform levelUpPopup;
    [SerializeField] private Image hpimg;
    [SerializeField] MonsterSpawnController monsterSpawnController;
    private float maxExp;
    private float exp;
    private int level = 0;
    private float timer = 0;  
    private int killCount = 0;
    private float[] exps;
    public void Awake()
    {
        instance = this;
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
                gameState = GameState.Pause;
                levelUpPopup.gameObject.SetActive(true);
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
            txtKillCount.text = $"{killCount.ToString("000")}";
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        OnGameStart();
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
        if(Input.GetKeyDown(KeyCode.F5)) 
        {
            gameState = GameState.Play;
        }
        if(Input.GetKey(KeyCode.F1))
        {
            Exp += 1f;
        }
        if(gameState != GameState.Play)
        {
            monsterSpawnController.StartSpawn(false); 
            return;
        }
            
        timer += Time.deltaTime;
        System.TimeSpan ts = System.TimeSpan.FromSeconds(timer);
        txtTime.text = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
    }
    public void SetHP(int HP, int maxHP)
    {
        hpimg.fillAmount = (float)HP / maxHP;
    }
    public void OnGameStart()
    {
        monsterSpawnController.StartSpawn(true);
        gameState = GameState.Play;
    }
}
