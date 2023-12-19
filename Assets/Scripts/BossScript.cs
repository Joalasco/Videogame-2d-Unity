using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class BossScript : MonoBehaviour
{
    public Transform mainCharacter;
    public GameObject BulletPrefab;
    public HealthBar bossHealthBar;
    public float activationDistance = 4.0f;
    private int Health = 15;
    private float LastShoot;
    public GameManager gameManager;
    public Transform firePoint1; 
    public Transform firePoint2;
    private void Start()
    {
        bossHealthBar.setMaxHealth(Health);
    }

    void Update()
    {
        if (mainCharacter == null)
        {
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, mainCharacter.position);

        if (distanceToPlayer <= activationDistance)
        {
            bossHealthBar.gameObject.SetActive(true);
            UpdateBoss();
        }
        else
        {
            bossHealthBar.gameObject.SetActive(false);
        }
    }
    private void Shoot()
    {
        Vector3 diagonalDirection = new Vector3(transform.localScale.x, -1.0f, 0.0f);

        GameObject bullet1 = Instantiate(BulletPrefab, transform.position + diagonalDirection * 0.15f, Quaternion.identity);
        bullet1.GetComponent<BulletScript>().SetDirection(diagonalDirection);

        // Ajusta la distancia "y" aleatoria aquí
        float randomY = Random.Range(-0.05f, 0.4f);
        Vector3 randomPosition = new Vector3(firePoint2.position.x -0.1f, firePoint2.position.y + randomY, firePoint2.position.z);

        GameObject bullet2 = Instantiate(BulletPrefab, randomPosition, Quaternion.identity);
        bullet2.GetComponent<BulletScript>().SetDirection(Vector3.left);
        bullet2.GetComponent<BulletScript>().Speed = 10.0f;
    }

    void UpdateBoss()
    {
        Vector3 direction = mainCharacter.position - transform.position;
        if (direction.x >= 0.0f)
        {
            transform.localScale = new Vector3(5.0f, 5.0f, 1.0f);
        }
        else
        {
            transform.localScale = new Vector3(-5.0f, 5.0f, 1.0f);
        }

        float distance = Vector3.Distance(transform.position, mainCharacter.position);

        if (distance < 2.5f && Time.time > LastShoot + 0.6f)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    public void Hit()
    {
        Health -= 1;
        bossHealthBar.SetHeatlh(Health);
        if (Health == 0)
        {
            Destroy(gameObject);
            bossHealthBar.gameObject.SetActive(false);
            gameManager.CambiarEstado(GameState.Victoria);
            Destroy(mainCharacter.gameObject);
        }
    }
}
