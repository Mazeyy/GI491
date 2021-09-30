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
            Enemystats.DealDMG(Player_Controller.instance.damage);
            //Destroy(other.gameObject);            
        }
    }
}
