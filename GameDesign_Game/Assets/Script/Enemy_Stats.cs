using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Stats : MonoBehaviour
{
    [Space]
    [Header("Stat :")]
    public float MaxHealt = 1.0f;
    public float Health = 1.0f;
    public float AttackDMG = 1.0f;
    public bool Heart_drop;
    public GameObject HeartPrefab;
    public GameObject PotionPrefab;
    public GameObject Player;    
    public Rigidbody2D rb;
    //public float health;

    public float knockbackPower = 0.002f;
    public float knockbackDuration = 0.7f;

    private Animator anim;

    private int randDrop;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Health = MaxHealt;

        anim = GetComponent<Animator>();
    }

    private void Update()
    //ถ้า mon เหลือเลือดเท่าที่กำหนด ก็เปลี่ยน state
    {
        if (Health <= 499)
        {
            anim.SetTrigger("StateTwo");
        }
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
            StartCoroutine(Player_Controller.instance.Knockback(knockbackDuration, knockbackPower, this.transform));            
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
        randDrop = Random.Range(1, 10);
        if (randDrop == 1)
        {
            Vector2 position = transform.position;
            GameObject Heart = Instantiate(HeartPrefab, position, Quaternion.identity);
            //GameObject Heart = Instantiate(HeartPrefab);
            //Heart.SetActive(true);
            Destroy(Heart, 5.0f);
        }

        if (randDrop == 2)
        {
            Vector2 position = transform.position;
            GameObject Potion = Instantiate(PotionPrefab, position, Quaternion.identity);
            Destroy(Potion, 5.0f);
        }
        
    }
}
