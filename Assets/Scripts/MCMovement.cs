using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using static GameManager;

public class MCMovement : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public GameObject BulletPrefab;
    public HealthBar healthBar;
    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    private bool Grounded;
    private float LastShoot;
    private int Health = 10;
    public GameManager gameManager;

    private void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        healthBar.setMaxHealth(Health);
    }
    private void Update()
    {
        if (Time.timeScale > 0f && gameManager.siguiente)
        {
            Horizontal = Input.GetAxisRaw("Horizontal");

            if (Horizontal < 0.0f)
            {
                transform.localScale = new Vector3(-1.0f, 0.9f, 1.0f);
            }
            else if (Horizontal > 0.0f)
            {
                transform.localScale = new Vector3(1.0f, 0.9f, 1.0f);
            }

            Animator.SetBool("running", Horizontal != 0.0f);

            if (Physics2D.Raycast(transform.position, Vector3.down, 0.15f))
            {
                Grounded = true;
            }
            else Grounded = false;

            if (Input.GetKeyDown(KeyCode.W) && Grounded)
            {
                Saltar();
            }

            if (Input.GetKey(KeyCode.P) && Time.time > LastShoot + 0.25f)
            {
                Disparar();
                LastShoot = Time.time;
            }
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Teleport")
        {
            transform.position = new Vector3(9.4f, 1f, 0f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int salud;
        if (collision.gameObject.tag == "objeto")
        {
            salud = Health + 1;
            if(salud <= 10)
            {
                Health += 1;
                healthBar.SetHeatlh(Health);
                Destroy(collision.gameObject);
            }
        }
    }
    private void FixedUpdate()
    {
        
        if (Time.timeScale > 0f && gameManager.siguiente)
        {
            Rigidbody2D.velocity = new Vector2(Horizontal * Speed, Rigidbody2D.velocity.y);
        }
    }

    private void Saltar()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    private void Disparar()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector3.right;
        else direction = Vector3.left;

        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.15f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    public void Hit()
    {
        Health -= 1;
        healthBar.SetHeatlh(Health);
        if (Health == 0)
        {
            Destroy(gameObject);
            gameManager.CambiarEstado(GameState.GameOver);
        }
    }
}