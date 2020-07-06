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

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        speed = 3.0f;
        vertical = true;

        if (vertical)
        {
            rigidBody2D.constraints = RigidbodyConstraints2D.FreezePositionX |
                RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            rigidBody2D.constraints = RigidbodyConstraints2D.FreezePositionY |
                RigidbodyConstraints2D.FreezeRotation;
        }
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
        }
        else
        {
            currentPos.x += speed * direction * Time.deltaTime;
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
