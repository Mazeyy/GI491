using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart_Stat : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

}
