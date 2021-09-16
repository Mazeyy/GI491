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
    private Coroutine slimeUpdate;
   
    void Update()
    {
        Animate();
        SlimeWalk();
    }    

    void SlimeWalk()
    {
        if (Vector3.Distance(transform.position, Player.position) <= Range)
        {
            movementDirection = (Player.transform.position - transform.position).normalized;
            transform.position = Vector3.MoveTowards(transform.position, Player.position, movementSpeed * Time.deltaTime);            
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
