using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownScript : MonoBehaviour
{
    private GameManager gameManager;
    public Transform MainCharacter;
    public GameObject BulletPrefab;
    private float Health = 3;
    private float LastShoot;
    public float lifespan = 15.0f;

    void Update()
    {
        lifespan -= Time.deltaTime;

        if (lifespan <= 0)
        {
            Destroy(gameObject);
        }

        if (MainCharacter == null) return;

        Vector3 direction = MainCharacter.position - transform.position;
        if (direction.x >= 0.0f)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }

        float distance = Mathf.Abs(MainCharacter.position.x - transform.position.x);

        if (distance < 1.0f && Time.time > LastShoot + 1f)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    private void Shoot()
    {
        Vector3 direction = new Vector3(transform.localScale.x, 0.0f, 0.0f);
        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    public void Hit()
    {
        Health -= 1;
        if (Health == 0)
        {
            Destroy(gameObject);
        }
    }
}
