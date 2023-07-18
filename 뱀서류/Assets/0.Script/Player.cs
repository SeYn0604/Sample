using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] private float speed = 3f;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Transform shieldPrefab;
    [SerializeField] private Transform shieldParent;
    [SerializeField] private UI ui;
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
