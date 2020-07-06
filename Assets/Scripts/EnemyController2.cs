using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController2 : MonoBehaviour
{

    [SerializeField] Vector2 movementVect = new Vector2(10f, 10f);
    [Range(0,1)] [SerializeField] float movementFactor;
    [SerializeField] float period = 2f;

    Rigidbody2D rigidbody2D;

    float cycles;
    const float tau = Mathf.PI * 2f;
    float rawSineWave;
    Vector2 startingPos;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        startingPos = rigidbody2D.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(period <= Mathf.Epsilon)
        {
            return;
        }

        cycles = Time.time / period;
        rawSineWave = Mathf.Sin(cycles * tau);
        movementFactor = rawSineWave / 2f + 0.5f;

        rigidbody2D.position = startingPos + movementFactor * movementVect;
    }
}
