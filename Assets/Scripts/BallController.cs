using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Collider2D collide;

    private Collider2D thatCollider;
    public AudioSource sound;

    public bool candy;

    private void Start()
    {
        collide = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Dot"))
        {
            if (!candy)
            {
                Debug.Log("click");
                sound.Play();
            }
        }

        if (collision.gameObject.CompareTag("Candy"))
        {
            candy = true;
            sound.Play();
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
            //collide.enabled = false;
            collide.isTrigger = true;
            transform.SetParent(collision.gameObject.transform);
            this.enabled = false;
        }

        if (collision.gameObject.CompareTag("Cloud"))
        {
            StartCoroutine(cloudOff());
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
            collide.enabled = false;
            if (thatCollider != null)
            {
                thatCollider.enabled = true;
                thatCollider = null;
            }
            thatCollider = collision.collider;
            transform.SetParent(collision.gameObject.transform);
        }
    }


    public IEnumerator cloudOff()
    {
        yield return new WaitForSeconds(2f);

        thatCollider.enabled = false;
        rb.isKinematic = false;
        transform.SetParent(null);
        collide.enabled = true;

        StartCoroutine(cloudOn());
    }

    public IEnumerator cloudOn()
    {
        yield return new WaitForSeconds(1f);
        thatCollider.enabled = true;
    }

    }
