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
    public bool Heart_drop;
    public GameObject HeartPrefab;
    public GameObject Player;
    //public float health;

    private void Start()
    {
        
        Health = MaxHealt;
    }

    public void DealDMG(float damage)
    {
        Health -= damage;
        print("Damage Taken");
        CheckDeath();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("Touch Damage");
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
        //Random.Range(1, 10);
        if (Heart_drop == true /*&& */)
        {            
            Vector3 position = transform.position;
            GameObject Heart = Instantiate(HeartPrefab, position , Quaternion.identity);
            //GameObject Heart = Instantiate(HeartPrefab);
            //Heart.SetActive(true);
            Destroy(Heart, 5.0f);          
        }        
    }
}
