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

    bool isBroken;

    public ParticleSystem smokeEffect;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        speed = 3.0f;

        animator = GetComponent<Animator>();

        isBroken = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (!isBroken) //if the robot is fixed, stop calculating movement timer
        {
            return;
        }

        timer -= Time.deltaTime; //calculate change direction time
        if(timer < 0f)
        {
            timer = changeDirecTime;
            direction = -direction;
        }

        
    }

    void FixedUpdate()
    {
        if (!isBroken) //if robot is fixed, stop moving
        {
            return;
        }

        //Robot movement
        Vector2 currentPos = rigidBody2D.position;

        if (vertical) {
            currentPos.y += speed * direction * Time.deltaTime;

            animator.SetFloat("Move X", 0f); //tells animator bullshit
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            currentPos.x += speed * direction * Time.deltaTime;


            animator.SetFloat("Move X", direction); //tells animator bullshit
            animator.SetFloat("Move Y", 0f);
        }

        rigidBody2D.MovePosition(currentPos);
        //end Robot movement
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController controller = other.gameObject.GetComponent<RubyController>();

        if(controller != null)
        {
            controller.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        isBroken = false;
        animator.SetTrigger("Fixed");
        rigidBody2D.simulated = false; //removes rigidbody from physics system
        smokeEffect.Stop(); //stops smoke effect
    }
}
