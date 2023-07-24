using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int HitCount { get; set; }
    public int HitMaxCount { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        HitCount = 0;
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (UI.instance.gameState != GameState.Play)
            return;

        transform.Translate(Vector3.right * Time.deltaTime * 15f);
    }
    public void SetHitMaxCount(int count)
    {
        HitMaxCount = count;
    }
}
