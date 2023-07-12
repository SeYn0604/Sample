using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    [SerializeField] private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            Vector3 vec3 = new Vector3(target.position.x, target.position.y, -10f);
            transform.position = Vector3.Lerp(vec3, transform.position, Time.deltaTime * 5);
        }
    }
}
