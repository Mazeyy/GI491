using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggro : MonoBehaviour
{        

    public float movementSpeed = 1.0f;
    public float movement_base_speed = 1.0f;
    public float Range = 5F;
    
    [Space]
    [Header("Statistics :")]    
    private Vector2 movementDirection;  

    [Space]
    [Header("References:")]
    public Rigidbody2D rb;
    public Animator Animator;
    public Transform Player;    
    private Transform PlayerPos;
    //private void Start()
    //{
    //    SlimeWalk();
    //}

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Animate();
        FollowPlayer();
        Slime_Movement_type();
    }    

    void Slime_Movement_type()
    {

    }

    void FollowPlayer()
    {
        if (Vector2.Distance(transform.position, PlayerPos.position) <= Range )
        {            
            movementDirection = (PlayerPos.transform.position - transform.position).normalized;
            transform.position = Vector2.MoveTowards(transform.position, PlayerPos.position, movementSpeed * Time.deltaTime);            
        }               
    }
    
    void Animate()
    {
        if (movementDirection != Vector2.zero)
        {
            Animator.SetFloat("Horizontal", movementDirection.x);
            Animator.SetFloat("Vertical", movementDirection.y);
        }        
    }    
}
