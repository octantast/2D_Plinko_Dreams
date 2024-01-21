using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GeneralController general;
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ball"))
        {
            general.ballDone(collision.gameObject);
            collision.gameObject.transform.localScale = Vector3.zero;
            collision.collider.enabled = false;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            general.ballDone(collision.gameObject);
            collision.gameObject.transform.localScale = Vector3.zero;
            collision.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }
}
