using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [Space]
    [Header("CharacterAttributes:")]
    private float movementBaseSpeed = 1.0f;    
    //private float aimingBasePenalty = 0.50f;    

    [Space]
    [Header("CharacterStatistics:")]
    Vector2 shootingDirection;
    Vector2 movementDirection;
    private float movementSpeed;

    [Space]
    [Header("References:")]
    public Rigidbody2D rb;
    public Animator Animator;
    public GameObject CrossHair;    
    private bool endOfAiming;
    private bool isAiming;
    public bool rangeCombat;
    //public bool meleeCombat;
    public LayerMask enemyLayer;

    [Space]
    [Header("Bullet")]
    public GameObject BulletPrefab;
    public float BulletSpd;
    public float minDamage;
    public float maxDamage;

    [Space]
    [Header("Dash")]
    private bool candash = true;
    public float DashSPD;
    public float timeBTWdash;
    public float Dashtime;

    //[Space]
    //[Header("Melee")]
    //public float attackRange = 0.5f;
    //public Transform Attackpoint; 

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    public void Update()
    {
        ProcessInputs();
        Move();
        Animate();
        Shoot();
        //Aim();
        //Attack();
    }

    private void ProcessInputs()
    {
        {
            movementDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
            movementDirection.Normalize();

            //isAiming = Input.GetButton("Fire1");
            endOfAiming = Input.GetButtonDown("Fire1");
                        
        }

        //if (isAiming/* || Shootingrecoil > 0.0f*/ || rangeCombat == true/* || meleeCombat == false*/)
        //{
        //    movementSpeed *= aimingBasePenalty;
        //}

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Dashing();
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

    void Dashing()
    {
        if (candash == true)
        {
            StartCoroutine(Dash());  
        }              
    }

    IEnumerator Dash()
    {
        candash = false;
        movementBaseSpeed = DashSPD;
        yield return new WaitForSeconds(Dashtime);
        movementBaseSpeed = 1;
        yield return new WaitForSeconds(timeBTWdash);
        candash = true;
    }

    //void Attack()
    //{
    //    if (endOfAiming /*&& meleeCombat == true*/)
    //    {
    //        Animator.SetTrigger("Attack");

    //        Collider2D[] hitenemies = Physics2D.OverlapCircleAll(Attackpoint.position, attackRange, enemyLayer);

    //        foreach (Collider2D enemy in hitenemies)
    //        {
    //            Debug.Log("HIT" + enemy.name);
    //        }
    //    }
    //}
    //void OnDrawGizmosSelected()
    //{
    //    if(Attackpoint == null)
    //    {
    //        return;
    //    }

    //    Gizmos.DrawWireSphere(Attackpoint.position, attackRange);
    //}



    void Shoot()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerpos = transform.position;
        CrossHair.transform.localPosition = mousePosition;

        shootingDirection = mousePosition - playerpos;
        shootingDirection.Normalize();

        if (endOfAiming && rangeCombat == true)
        {
            GameObject bullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity);

            bullet.GetComponent<Rigidbody2D>().velocity = shootingDirection * BulletSpd;
            bullet.GetComponent<bullet>().damage = Random.Range(minDamage, maxDamage);
            bullet.transform.Rotate(0, 0, Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
            Destroy(bullet, 3.0f);
        }
        
    }
}

