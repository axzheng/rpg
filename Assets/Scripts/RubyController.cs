using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    float DHorizontal;
    float DVertical;

    public int maxHealth = 5;
    int currentHealth;
    public float speed;

    public int health { get { return currentHealth; } }

    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;

    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    public GameObject projectilePrefab;

    public ParticleSystem healthparticles;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>(); //get rigidbody component
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;

        speed = 4.0f;
    }

    // Update is called once per frame
    void Update()
    {
        DHorizontal = Input.GetAxis("Horizontal"); //gets movement input
        DVertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(DHorizontal, DVertical);

        if(!Mathf.Approximately(move.x, 0f) || !Mathf.Approximately(move.y, 0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);


        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;

            if(invincibleTimer < 0)
            {
                isInvincible = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }
        
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position; //moves ruby
        position.x += speed * DHorizontal * Time.deltaTime;
        position.y += speed * DVertical * Time.deltaTime;
        rigidbody2d.position = position;
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
            {
                return;
            }
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        else if (amount > 0)
        {
            ParticleSystem health = Instantiate(healthparticles, rigidbody2d.position,
                Quaternion.identity);
            health.Play();
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
        animator.SetTrigger("Hit");
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab,
            rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();

        projectile.Launch(lookDirection, 300);
        animator.SetTrigger("Launch");
    }
}
