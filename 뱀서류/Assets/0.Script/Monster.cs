using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private Player p;
    [SerializeField] private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (p == null)
            return;

        float x = p.transform.position.x - transform.position.x;

        sr.flipX = x < 0 ? true : x == 0 ? true : false;

        float distance = Vector2.Distance(p.transform.position, transform.position);

        if (distance < 1)
        {
            //공격
        }
        else
        {
            //이동
            Vector2 v1 = (p.transform.position - transform.position).normalized * Time.deltaTime * 1f;
            transform.Translate(v1);
        }
    }
    public void SetPlayer(Player p)
    {
        this.p = p;
    }
}
