using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] private float speed = 3f;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Transform shieldPrefab;
    [SerializeField] private Transform shieldParent;
    [SerializeField] private UI ui;
    [SerializeField] private Transform firePos;
    [SerializeField] Bullet bullet;

    float bulletTimer;
    private List<Transform> shields = new List<Transform>();
    int hp, maxhp, shieldCount, shieldSpeed;
    float x, y;
    // Start is called before the first frame update
    void Start()
    {
        hp = maxhp = 100;
        shieldSpeed = 10;
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(x, y, 0f) * Time.deltaTime * speed);

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

        Monster[] monsters = FindObjectsOfType<Monster>();
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
                //b.transform.SetParent(firepos);
                b.transform.SetParent(null);
            }
            bulletTimer = 0;
        }
    }
    public void Hit(int damage)
    {
        hp -= damage;
        UI.instance.SetHP(hp, maxhp);
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
        ui.Exp += exp;
    }
}
