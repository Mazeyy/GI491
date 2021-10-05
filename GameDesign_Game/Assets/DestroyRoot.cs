using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyRoot : MonoBehaviour
{
    public float timer;
  
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }
}
