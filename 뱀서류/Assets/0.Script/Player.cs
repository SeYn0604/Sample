using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Bullet bullet;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Transform shieldPrefab;
    [SerializeField] private Transform shieldParent;
    [SerializeField] public Transform firePos;

    public GameObject aimObject;
    float bulletTimer;
    private List<Transform> shields = new List<Transform>();
    int shieldCount, shieldSpeed;
    float x, y;
    public int HP { get; set; }
    public int MaxHP { get; set; }
    public float Speed { get; set; }
    public float BulletFireDelayTime { get; set; } //차후 개발 시 총기의 연사속도로 재활용?
    public int BulletHitMaxCount { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        Speed = 3f;
        BulletFireDelayTime = 2f;
        HP = MaxHP = 100;
        shieldSpeed = 10;
    }

    // Update is called once per frame
    void Update()
    {
        // 마우스 위치에 따라 에임 조정
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        Vector2 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        firePos.rotation = Quaternion.Euler(0, 0, angle);
        //
        if (UI.instance.gameState != GameState.Play)
            return;
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(x, y, 0f) * Time.deltaTime * Speed);

        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))  
        {
            sr.flipX = true;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            sr.flipX= false;
        }
        if(x == 0 && y == 0)
        {
            animator.SetBool("Run", false);
        }
        else
        {
            animator.SetBool("Run", true);
        }
        //else dead 조건 추가해야됌

        if(Input.GetKeyDown(KeyCode.F2))
        {
            shieldCount++;
            shields.Add(Instantiate(shieldPrefab, shieldParent));
            Shield();
        }
        if(Input.GetKeyDown(KeyCode.F3))
        {
            shieldSpeed += 10;
        }
        shieldParent.Rotate(Vector3.back * Time.deltaTime * shieldSpeed);

        /*Monster[] monsters = FindObjectsOfType<Monster>();
        List<Monster> atkMonsterList = new List<Monster>();
        bulletTimer += Time.deltaTime;

        if(monsters.Length > 0 && bulletTimer > 2f)
        {
            foreach(Monster m in monsters)
            {
                float distance = Vector3.Distance(transform.position, m.transform.position);
                if(distance > 4)
                {
                    atkMonsterList.Add(m);
                }    
            }
            if(atkMonsterList.Count > 0)
            {
                Monster m = atkMonsterList[Random.Range(0,atkMonsterList.Count)];
                //타겟을 찾아 방향 전환
                Vector2 vec = transform.position - m.transform.position;
                float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
                firePos.rotation = Quaternion.AngleAxis(angle - 180, Vector3.forward);
                Bullet b = Instantiate(bullet, firePos);
                b.SetHitMaxCount(BulletHitMaxCount + 1);
                //b.transform.SetParent(firepos);
                b.transform.SetParent(null);
            }
            bulletTimer = 0;
        }
        *///(기존에 있던)랜덤한 좀비에게 자동으로 총알을 발사하는 코드
        if (Input.GetKeyUp(KeyCode.F4))
        {
            BulletHitMaxCount++;
        }
    }
    public void Hit(int damage)
    {
        HP -= damage;
        UI.instance.SetHP(HP, MaxHP);
    }
    public void Shield()
    {
        float z = 360 / shieldCount;
        for (int i = 0; i < shieldCount; i++)
        {
            shields[i].gameObject.SetActive(true);
            shields[i].rotation = Quaternion.Euler(0, 0, z * i);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Item>())
        {
            collision.GetComponent<Item>().isPickup = true;
            collision.GetComponent<Item>().target = this;
        }
    }
    public void GetExp(int exp)
    {
        UI.instance.Exp += exp;
    }
    public void AddShield()
    {
        shieldCount++;
        shields.Add(Instantiate(shieldPrefab, shieldParent));
        Shield();
        shieldSpeed += 10;
    }
}
