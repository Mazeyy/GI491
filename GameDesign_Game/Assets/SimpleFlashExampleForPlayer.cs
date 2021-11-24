using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFlashExampleForPlayer : MonoBehaviour
{
    [SerializeField] private SimpleFlashForPlayer flashEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == ("Enemy") || other.tag == ("Boss"))
        {
            flashEffect.Flash();
        }
        else if(other.tag == ("Boss"))
        {
            flashEffect.Flash();
        }
    }
}
