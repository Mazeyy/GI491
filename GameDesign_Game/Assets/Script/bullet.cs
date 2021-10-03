using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public Vector2 velocity = new Vector2(0, 0);
    public GameObject player;

    public float damage;

    void FixedUpdate()
    {
        Vector2 currenPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 newPosition = currenPosition + velocity * Time.deltaTime;

        RaycastHit2D[] hits = Physics2D.LinecastAll(currenPosition, newPosition);

        foreach (RaycastHit2D hit in hits)
        {
            GameObject other = hit.collider.gameObject;

            if (other.CompareTag("Wall") || other.CompareTag("Enemy") || other.CompareTag("Boss"))
            {
                if (other.GetComponent<Enemy_Stats>() != null)
                {
                    other.GetComponent<Enemy_Stats>().DealDMG(damage);
                }

                if (other.GetComponent<Boss>() != null)
                {
                    other.GetComponent<Boss>().DealDMG(damage);
                }
                Destroy(gameObject);
            }
        }
        transform.position = newPosition;
    }      
}
