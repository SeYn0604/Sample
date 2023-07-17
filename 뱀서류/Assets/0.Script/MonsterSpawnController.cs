using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnController : MonoBehaviour
{
    [SerializeField] private Player p;

    [SerializeField] private Monster monster;
    [SerializeField] private Transform parent;

    [SerializeField] private BoxCollider2D[] boxColls;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CreateMonster", 0.5f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateMonster()
    {
        int rand = Random.Range(0, boxColls.Length);
        Vector2 v = RandomPosition(boxColls[rand]);

        Monster m = Instantiate(monster, v, Quaternion.identity);
        m.SetPlayer(p);
        m.transform.SetParent(parent);
    }
    Vector2 RandomPosition(BoxCollider2D boxColl)
    {
        Vector2 pos = boxColl.transform.position;

        Vector2 ranger = new Vector2(boxColl.bounds.size.x, boxColl.bounds.size.y);

        ranger.x = Random.Range((ranger.x / 2) * -1, ranger.x / 2);
        ranger.y = Random.Range((ranger.y / 2) * -1, ranger.y / 2);

        return pos + ranger;
    }
}
