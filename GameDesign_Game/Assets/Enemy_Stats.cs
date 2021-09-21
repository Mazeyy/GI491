using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Stats : MonoBehaviour
{
    [Space]
    [Header("Stat :")]
    public float MaxHealt = 1.0f;
    public float Health = 1.0f;
    public float AttackDMG = 1.0f;

    public GameObject HeartPrefab;
    //public float health;

    private void Start()
    {
        Health = MaxHealt;
    }

    public void DealDMG(float damage)
    {
        Health -= damage;
        CheckDeath();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("DAMAGE");
        }
    }

    void CheckDeath()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
            ItemDrop();
        }
    }

    void ItemDrop()
    {
        Vector3 position = transform.position;
        GameObject Heart = Instantiate(HeartPrefab, position , Quaternion.identity);
        //GameObject Heart = Instantiate(HeartPrefab);
        //Heart.SetActive(true);
        Destroy(Heart, 5.0f);  
        
    }
}
