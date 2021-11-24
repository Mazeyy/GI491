using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damgeBoss2 : MonoBehaviour
{
    public float damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            Player_Stats.PlayerStats.DealDMG(damage);
            gameObject.SetActive(false);
            Debug.Log("olo");
        }
    }
}
