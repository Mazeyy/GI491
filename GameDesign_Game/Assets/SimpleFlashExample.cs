using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFlashExample : MonoBehaviour
{
    [SerializeField] private SimpleFlash flashEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == ("Weapon"))
        {
            flashEffect.Flash();
        }
    }
}
