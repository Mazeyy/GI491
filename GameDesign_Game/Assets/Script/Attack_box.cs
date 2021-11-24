using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_box : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == ("Enemy"))
        {
            Enemy_Stats Enemystats;
            Enemystats = other.gameObject.GetComponent<Enemy_Stats>();
            Enemystats.DealDMG(Player_Stats.PlayerStats.damage);

        }

        if (other.tag == ("EnemyShoot"))
        {
            Enemy_Shoot EnemyShoot;
            EnemyShoot = other.gameObject.GetComponent<Enemy_Shoot>();
            EnemyShoot.DealDMG(Player_Stats.PlayerStats.damage);

        }

        if (other.tag == ("Boss"))
        {
            Boss Bossstats;
            Bossstats = other.gameObject.GetComponent<Boss>();
            Bossstats.DealDMG(Player_Stats.PlayerStats.damage);
        }

        //if(other.tag == ("bullet"))
        //{
        //    Destroy(other.gameObject);
        //}
    }
}
