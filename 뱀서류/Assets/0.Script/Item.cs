using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool isPickup = false;
    public Player target;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isPickup)
        {
            Vector2 v1 = (target.transform.position - transform.position).normalized * Time.deltaTime * 5f;
            transform.Translate(v1);
            if (Vector3.Distance(transform.position, target.transform.position)<0.1f)
            {
                target.GetExp(1);
                Destroy(gameObject);
            }
        }
    }
}
   
