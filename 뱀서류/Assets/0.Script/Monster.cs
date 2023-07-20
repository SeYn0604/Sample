using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private Player p;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject expPrefab;
    [SerializeField] private GameObject magPrefab;
 
    float hp;
    protected float atkTime = 2f;
    protected int power = 6;
    private float atkTimer;
    private float hitFreezeTimer;
    // Start is called before the first frame update
    void Start()
    {
        hp = 100;
    }
    // Update is called once per frame
    void Update()
    {
        if (UI.instance.gameState != GameState.Play)
            return;
        if (p == null || hp < 0)
            return;
        if(hitFreezeTimer > 0)
        {
            hitFreezeTimer -= Time.deltaTime;
            return;
        }
        

        float x = p.transform.position.x - transform.position.x;

        sr.flipX = x < 0 ? true : x == 0 ? true : false;

        float distance = Vector2.Distance(p.transform.position, transform.position);

        if (distance <= 1)
        {
            atkTimer += Time.deltaTime;
            //����
            if (atkTimer > atkTime)
            {
                atkTimer = 0;
                p.Hit(power);
            }
        }
        else
        {
            //�̵�
            if (hp > 0)
            {
                Vector2 v1 = (p.transform.position - transform.position).normalized * Time.deltaTime * 1f;
                transform.Translate(v1);
            }
        }
    }
    public void SetPlayer(Player p)
    {
        this.p = p;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Shield>())
        {
            Dead(0.5f, 30);
        }
        if(collision.GetComponent<Bullet>())
        {
            Dead(1f, 100);
            Destroy(collision.gameObject);
        }
    }
    void Dead(float freezeTime, int damage)
    {
        hitFreezeTimer = freezeTime;
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(GetComponent<Rigidbody2D>());
            GetComponent<CapsuleCollider2D>().enabled = false;
            animator.SetBool("Dead", true);
            StartCoroutine("CDropExp");
            if(UnityEngine.Random.value < 0.005)
            {
                Instantiate(magPrefab, transform.position, Quaternion.identity);
            }
        }
    }
    IEnumerator CDropExp()
    {
        UI.instance.KillCount++;
        Instantiate(expPrefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }    
    void DropExp()
    {
        Instantiate(expPrefab,transform.position, Quaternion.identity);
    }
}
