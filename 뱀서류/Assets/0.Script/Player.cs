using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] private float speed = 3f;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sr;
    int hp, maxhp = 100;
    float x, y;
    // Start is called before the first frame update
    void Start()
    {
        
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
    }
    public void Hit(int damage)
    {
        hp -= damage;
        UI.instance.SetHP(hp, maxhp);
    }
}
