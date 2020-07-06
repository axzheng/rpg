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

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>(); //get rigidbody component

        currentHealth = maxHealth;

        speed = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        DHorizontal = Input.GetAxis("Horizontal"); //gets movement input
        DVertical = Input.GetAxis("Vertical");

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;

            if(invincibleTimer < 0)
            {
                isInvincible = false;
            }
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

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}
