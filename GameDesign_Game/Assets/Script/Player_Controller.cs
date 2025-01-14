﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [Space]
    [Header("CharacterAttributes:")]
    public float movementBaseSpeed = 1.0f;
    public static Player_Controller instance;
    private bool CanMove = true;

    [Space]
    [Header("CharacterStatistics:")]
    Vector2 CombatDirection;
    Vector2 movementDirection;    
    public float movementSpeed;        

    [Space]
    [Header("References:")]
    public Rigidbody2D rb;
    public Animator Animator;
    public GameObject CrossHair;    
    public GameObject ArrowCircle;    

    [Space]
    [Header("Dash")]
    public float DashSpeed;
    public bool IsDashing;
    public float Dashtime;
    public float timeBTWdash;
    public float StarttimeBTWdash;
    public LayerMask PlayerLayer;
    public LayerMask EnemyLayer;

    [Space]
    [Header("Melee")]
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public Transform Attackpoint;
    public LayerMask whatIsEnemies;
    public float attackRange = 0.5f;
    public int damage = 10;
    public float knockbackPower = 1f;
    public float knockCooldown = 1;
    public float SlideAttack;

    [Space]
    [Header("Range")]
    public GameObject BulletPrefab;
    public bool rangeCombat;
    private bool Range_Attack;

    public float BulletSpd;
    public float minDamage;
    public float maxDamage;        

    void Awake()
    {
        instance = this;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Update()
    {        
        ProcessInputs();
        Move();
        Animate();
        Shoot();
        //Aim();
        Attack();
    }

    private void ProcessInputs()
    {
        if (CanMove == true)
        {
            movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
            movementDirection.Normalize();
        }        

        Range_Attack = Input.GetButtonDown("Fire2");        

        if (Input.GetKeyDown(KeyCode.Space) && IsDashing == false)
        {
            if (timeBTWdash <= 0)
            {
                //rb.AddForce(movementDirection * DashSpeed , ForceMode2D.Impulse);
                StartCoroutine(Dashing());
               // Debug.Log("dash");
            }            
        }
        else
        {
            timeBTWdash -= Time.deltaTime;
        }
    }

    private void Move()
    {
        rb.velocity = movementDirection * movementSpeed * movementBaseSpeed;
    }

    private void Animate()
    {
        if (movementDirection != Vector2.zero)
        {
            Animator.SetFloat("Horizontal", movementDirection.x);
            Animator.SetFloat("Vertical", movementDirection.y);
        }       

        Animator.SetFloat("Speed", movementSpeed);       
    } 
    private IEnumerator Dashing()
    {
        //Debug.Log("dash");
        //IsDashing = true;        
        ////rb.AddForce(movementDirection * DashSpeed , ForceMode2D.Force);        
        //yield return new WaitForSeconds(0.2f);
        ////timeBTWdash = Dashtime;
        ////IsDashing = false;
        //Physics2D.IgnoreLayerCollision(PlayerLayer, EnemyLayer, true);
        ////rb.AddForce(movementDirection * DashSpeed , ForceMode2D.Force);
        //yield return new WaitForSeconds(0.2f);
        //Physics2D.IgnoreLayerCollision(PlayerLayer, EnemyLayer, false);
        Debug.Log("dash");
        IsDashing = true;
        movementBaseSpeed = DashSpeed ;
        //Physics2D.IgnoreLayerCollision(PlayerLayer, EnemyLayer, true);
        //rb.AddForce(movementDirection * DashSpeed , ForceMode2D.Force);
        yield return new WaitForSeconds(0.2f);
        Physics2D.IgnoreLayerCollision(PlayerLayer, EnemyLayer, false);
        movementBaseSpeed = 5f;
        timeBTWdash = Dashtime;
        IsDashing = false;
    }
    void ignoreCollision()
    {
        if (IsDashing == true)
        {
            Physics2D.IgnoreLayerCollision(PlayerLayer, EnemyLayer, true);
        }
    }

    void Attack()
    {       
        if (timeBtwAttack <= 0)
        {           
            
            if (Input.GetButton("Fire1"))
            {
                movementDirection = Vector2.zero;                
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 playerpos = transform.position;
                CrossHair.transform.localPosition = mousePosition;
                CombatDirection = mousePosition - playerpos;                ;
                CombatDirection.Normalize();                
                Animator.SetFloat("Hori_Slash", CombatDirection.x);
                Animator.SetFloat("Vert_Slash", CombatDirection.y);

                //rb.AddForce(CombatDirection * SlideAttack);   //player slide while attack

                Animator.SetTrigger("Attack");
                //Collider2D[] hitenemies = Physics2D.OverlapCircleAll(Attackpoint.position, attackRange, whatIsEnemies);

                //for (int i = 0; i < hitenemies.Length; i++)
                //{
                //    hitenemies[i].GetComponent<Enemy_Stats>().DealDMG(damage);                    
                //}
                timeBtwAttack = startTimeBtwAttack;               
            }            
        }
        else
        {            
            timeBtwAttack -= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (Attackpoint == null)
        {
            return;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(Attackpoint.position, attackRange);
    }


    public IEnumerator Knockback(float knockbackDuration , float knockbackPower, Transform obj)
    {
        float timer = 0;
        while(knockbackDuration > timer)
        {
            timer += Time.deltaTime;
            Vector2 direction = (obj.transform.position - this.transform.position).normalized;
            rb.AddForce(-direction * knockbackPower);
            knockbackDuration = knockCooldown;
        }        
        yield return 0;
        
    }    


    public void Shoot()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerpos = transform.position;
        CrossHair.transform.localPosition = mousePosition;
        CombatDirection = mousePosition - playerpos;
        CombatDirection.Normalize();
        
        if (Range_Attack && rangeCombat == true)
        {
            GameObject bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);

            bullet.GetComponent<Rigidbody2D>().velocity = CombatDirection * BulletSpd;
            bullet.GetComponent<bullet>().damage = Random.Range(minDamage, maxDamage);
            bullet.transform.Rotate(0, 0, Mathf.Atan2(CombatDirection.y, CombatDirection.x) * Mathf.Rad2Deg);
            Destroy(bullet, 3.0f);
        }        
    }
}

