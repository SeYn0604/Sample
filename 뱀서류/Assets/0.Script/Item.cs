using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
public enum FieldDropItem
{
    ExpCoin,
    Mag,
}
public class Item : MonoBehaviour
{
    public FieldDropItem dropItem;
    public bool isPickup = false;
    public Player target;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (UI.instance.gameState != GameState.Play)
            return;
        if (isPickup)
        {
            Vector2 v1 = (target.transform.position - transform.position).normalized * Time.deltaTime * 5f;
            transform.Translate(v1);
            if (Vector3.Distance(transform.position, target.transform.position)<0.1f)
            {
                if (FieldDropItem.ExpCoin == dropItem)
                {
                    target.GetExp(1);
                }
                else
                {
                    Item[] items = FindObjectsOfType<Item>();
                    foreach (var item in items)
                    {
                        if (item.dropItem == FieldDropItem.Mag)
                            continue;
                        item.target = target;
                        item.isPickup = true;
                    }
                }
                Destroy(gameObject);
            }
        }
    }
}
   
