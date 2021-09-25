using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPos_Rotate : MonoBehaviour
{
    Vector2 CombatDirection;
    //public Transform CrossHair;
    public Animator Animator;
    public GameObject Player;
    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerpos = Player.transform.position;
        //CrossHair.transform.localPosition = mousePosition;
        CombatDirection = mousePosition - playerpos;
        CombatDirection.Normalize();
        Animator.SetFloat("Hori_Slash", CombatDirection.x);
        Animator.SetFloat("Vert_Slash", CombatDirection.y);
    }
}
