using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    float DHorizontal;
    float DVertical;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        DHorizontal = Input.GetAxis("Horizontal");
        DVertical = Input.GetAxis("Vertical");
        
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x += 3.0f * DHorizontal * Time.deltaTime;
        position.y += 3.0f * DVertical * Time.deltaTime;
        rigidbody2d.position = position;
    }
}
