using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Character;
    public float smoothing;
    public Vector3 offset;

    void FixedUpdate()
    {
        if(Character != null)
        {
            Vector3 newPosition = Vector3.Lerp(transform.position, 
                Character.transform.position + offset, smoothing);

            transform.position = newPosition;
        }
    }
}
