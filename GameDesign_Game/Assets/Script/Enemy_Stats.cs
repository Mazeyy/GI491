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
    public float AttackDMG = 10f;
    public bool Heart_drop;
    public GameObject HeartPrefab;
    public GameObject PotionPrefab;
    public GameObject Player;
    private Transform PlayerPos;
    //private float HealCooldown;
    public static Enemy_Stats EnemyStats;

    public Rigidbody2D rb;
    //public float health;

    public float knockbackPower = 0.1f;
    public float knockbackDuration = 0.7f;
    public float knockbackResistance = 0f;
    private float knockCooldown;
    public float StartknockCooldown = 1;

    private Animator anim;

    private int randDrop;
   
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Health = MaxHealt;

        anim = GetComponent<Animator>();
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;

    }
    private void Update()
    {
        //PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void DealDMG(float damage)
    {
        Health -= damage;
        print("Damage Taken");           
        CheckDeath();        

    }    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(Player_Controller.instance.Knockback(knockbackDuration, knockbackPower, this.transform));            
        }

        if (other.tag == ("Weapon"))
        {
            if (knockCooldown <= 0)
            {
                StartCoroutine(Knock());
                Vector2 direction = (this.transform.position - PlayerPos.transform.position);
                direction = direction.normalized * 1;
                transform.position = new Vector2(transform.position.x + direction.x, transform.position.y + direction.y/* * knockbackPower*/);
                
            }
        }
        
    }

    IEnumerator Knock()
    {
        knockCooldown = 0.2f;
        yield return new WaitForSeconds(knockCooldown);
        knockCooldown =0;
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
        if (randDrop == 5)
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



    /*void Heal()
    {
        if (Health <= 20)
        {
            Health += 5;
        }
    }*/

    /*IEnumerable Heal() //Heal every seconds
    {
        if (Health <= 20)
        {
            Health += 10;
            HealCooldown = 0.2f;
            yield return new WaitForSeconds(HealCooldown);
            HealCooldown = 0;
            print("heal");
        }
    }*/
}
