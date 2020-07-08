using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody2D rigidBody2D;

    public float speed;
    public bool vertical;

    public float changeDirecTime = 3.0f;
    float timer;
    int direction = 1;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        speed = 3.0f;

        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0f)
        {
            timer = changeDirecTime;
            direction = -direction;
        }
    }

    void FixedUpdate()
    {
        Vector2 currentPos = rigidBody2D.position;

        if (vertical) {
            currentPos.y += speed * direction * Time.deltaTime;

            animator.SetFloat("Move X", 0f);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            currentPos.x += speed * direction * Time.deltaTime;


            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0f);
        }

        rigidBody2D.MovePosition(currentPos);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController controller = other.gameObject.GetComponent<RubyController>();

        if(controller != null)
        {
            controller.ChangeHealth(-1);
        }
    }
}
