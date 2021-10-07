using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDMGplayer_Handler : MonoBehaviour
{
    public float damage;
    public bool destroyAfhit;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            Player_Stats.PlayerStats.DealDMG(damage);

            if (destroyAfhit)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
