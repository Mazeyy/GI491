using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation_cross : MonoBehaviour
{
    public GameObject Player;      

    private void Update()
    {        
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        mousePosition.Normalize();        

        float rotationZ = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
       
    }
}
