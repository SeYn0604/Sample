using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    Play,
    Pause,
    Stop
}
[System.Serializable]
public class UpgradeUI
{
    public Image icon;
    public TMP_Text levelTxt;
    public TMP_Text title;
    public TMP_Text description1;
    public TMP_Text description2;
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
    [SerializeField] private UpgradeUI[] upUI;
    [SerializeField] public UpgradeData[] upData;
    [SerializeField] private BoxCollider2D[] boxColls;
    [SerializeField] private Slider sliderExp;
    [SerializeField] private Text txtTime;
    [SerializeField] private Text txtKillCount;
    [SerializeField] private Text txtLv;
    [SerializeField] private RectTransform canvas;
    [SerializeField] private Transform levelUpPopup;
    [SerializeField] private Image hpimg;
    [SerializeField] MonsterSpawnController monsterSpawnController;
    [SerializeField] private Player p;
    [SerializeField] private Bullet bullet;
    private float maxExp;
    private float exp;
    private int level = 0;
    private float timer = 0;  
    private int killCount = 0;
    private float[] exps;
    private List<UpgradeData> upgradeDatas = new List<UpgradeData>();
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
                SetUpgradeData();
                AudioManager.instance.Play("levelup");
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
    void SetUpgradeData()
    {
        List<UpgradeData> datas = new List<UpgradeData>();
        for (int i = 0; i < upData.Length; i++)
        {
            UpgradeData ud = new UpgradeData();
            ud.sprite = upData[i].sprite;
            ud.title = upData[i].title;
            ud.description1 = upData[i].description1;
            ud.description2 = upData[i].description2;
            datas.Add(ud);
        }

        upgradeDatas = new List<UpgradeData>();
        for (int i = 0; i < 3; i++)
        {
            int rand = Random.Range(0, datas.Count);
            UpgradeData ud = new UpgradeData();
            ud.sprite = datas[rand].sprite;
            ud.title = datas[rand].title;
            ud.description1 = datas[rand].description1;
            ud.description2 = datas[rand].description2;
            upgradeDatas.Add(ud);
            datas.RemoveAt(rand);
        }
        for (int i = 0; i < upgradeDatas.Count; i++)
        {
            upUI[i].icon.sprite = upgradeDatas[i].sprite;
            upUI[i].title.text = upgradeDatas[i].title;
            upUI[i].description1.text = upgradeDatas[i].description1;
            upUI[i].description2.text = upgradeDatas[i].description2;
        }
    }
    public void OnUpgrade(int index)
    {
        switch(upgradeDatas[index].sprite.name)
        {
            case "Bullet 0":
                p.AddShield();
                break;
            case "Select 5":
                p.BulletFireDelayTime -= p.BulletFireDelayTime * 0.1f;
                p.BulletHitMaxCount++;
                break;
            case "Select 6":
                break;
            case "Select 7":
                p.Speed += 2f;
                break;
            case "Select 8":
                p.HP = p.MaxHP;
                SetHP(p.HP,p.MaxHP);
                break;
        }
    }
}
