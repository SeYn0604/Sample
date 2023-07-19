using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnController : MonoBehaviour
{
    [SerializeField] private Player p;

    [SerializeField] private Monster monster;
    [SerializeField] private Transform parent;

    [SerializeField] private BoxCollider2D[] boxColls;

    int range = 10;
    void Start()
    {
        StartCoroutine(CreateMonster(0.1f));
    }
    IEnumerator CreateMonster(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            int rand = Random.Range(0, boxColls.Length);
            Vector2 v = RandomPosition(rand);

            Monster m = Instantiate(monster, v, Quaternion.identity);
            m.SetPlayer(p);
            m.transform.SetParent(parent);
        }
    }
    Vector2 RandomPosition(int index)
    {

        RectTransform pos = boxColls[index].GetComponent<RectTransform>();

        Vector3 randPos = Vector3.zero;
        // Top = 0 , Bottom = 1
        if (index == 0 || index == 1)
        {
            randPos = new Vector2(pos.position.x + Random.Range(-range, range), pos.position.y);
        }
        // ³ª¸ÓÁö
        else
        {
            randPos = new Vector2(pos.position.x, pos.position.y + Random.Range(-range, range));
        }

        return randPos;
    }
}
