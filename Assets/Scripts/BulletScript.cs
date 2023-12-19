using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float Speed;
    public AudioClip Sound;
    public GameObject impactEffect;
    private Rigidbody2D Rigidbody2D;
    private Vector3 Direction;

    private void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Camera.main.GetComponent<AudioSource>().PlayOneShot(Sound);
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = Direction * Speed;
    }

    public void SetDirection(Vector3 direction)
    {
        Direction = direction;
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ClownScript clown = other.GetComponent<ClownScript>();
        MCMovement mc = other.GetComponent<MCMovement>();
        BossScript boss = other.GetComponent<BossScript>();
        if (clown != null)
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            clown.Hit();
        }
        if (mc != null)
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            mc.Hit();
        }
        if(boss != null)
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            boss.Hit();
        }
        
        DestroyBullet();
    }
}
