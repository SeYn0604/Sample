using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] private float speed = 3f;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sr;
    float hp = 100;
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

        if ((x == 0 && y == 0) && hp != 0) 
        {
            animator.SetTrigger("Idle");
        }
        else if (hp != 0) 
        {
            animator.SetTrigger("Run");
        }
        if (x != 0)
        {
            if (x < 0)
            {
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }
        }
    }
}
