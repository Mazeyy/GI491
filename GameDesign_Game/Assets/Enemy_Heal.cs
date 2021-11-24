using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Heal : MonoBehaviour
{
    private Animator anim;

    private float HealCooldown;
    public float Health;
    public float currentHealth;


    public void HealOverTime(int healAmount, int duration)
    {
        StartCoroutine(HealOverTimeCoroutine(healAmount, duration));
    }

    //Heal Over time
    IEnumerator HealOverTimeCoroutine(float healAmount, float duration)
    {
        float amountHeal = 0;
        float healPerLoop = healAmount / duration;
        while (amountHeal < healAmount)
        {
            currentHealth += healPerLoop;
            amountHeal += healPerLoop;
            yield return new WaitForSeconds(1f);
        }
    }
    

    //Heal Zone
    /*  private void OnTriggerEnter2D(Collider2D col) 
    {
        if (col.gameObject.name.Equals("Enemy") && Enemy_Stats.EnemyStats.Health < 20)
            StartCoroutine ("Heal");
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name.Equals("Enemy"))
            StopCoroutine ("Heal");
    }
  
    IEnumerable Heal()
    {
        for (float currentHealth = Enemy_Stats.EnemyStats.Health; currentHealth <= 20; currentHealth += 10f)
        {
            Enemy_Stats.EnemyStats.Health = currentHealth;
            yield return new WaitForSeconds (Time.deltaTime);
        }
        Enemy_Stats.EnemyStats.Health = 30f;
    }

    IEnumerable HealCD() //Heal every seconds
    {
        HealCooldown = 0.2f;
        yield return new WaitForSeconds(HealCooldown);
        Enemy_Stats.EnemyStats.Health += 10;
        HealCooldown = 0;
    }*/
}
