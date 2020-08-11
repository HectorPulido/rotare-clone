using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float[] yRotations;
    public float speed;
    public Rigidbody2D rb;
    public ParticleSystem explosion;

    private bool invertIndex = false;
    private float offsetRotation = 0;
    private int indexRotation = 0;
    private Vector3 initialPosition;

    // Start is called before the first frame update
    private void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        SetRotation(yRotations[indexRotation]);

        initialPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (invertIndex)
            {
                indexRotation--;
            }
            else
            {
                indexRotation++;
            }
            indexRotation = indexRotation % yRotations.Length;

            if (indexRotation < 0)
            {
                indexRotation = yRotations.Length + indexRotation;
            }
            print(indexRotation);

            SetRotation(yRotations[indexRotation]);
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rb.velocity = transform.right * speed;
    }

    private void SetRotation(float rotation)
    {
        var currentRotation = transform.eulerAngles;
        currentRotation.z = rotation + offsetRotation;
        transform.eulerAngles = currentRotation;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Collider"))
        {
            explosion.transform.position = transform.position;
            explosion.Play();

            invertIndex = false;
            offsetRotation = 0;
            indexRotation = 0;
            SetRotation(yRotations[indexRotation]);
            transform.position = initialPosition;
        }
        else if (col.CompareTag("ChangeRotation"))
        {
            invertIndex = !invertIndex;
            offsetRotation += 180;
            SetRotation(yRotations[indexRotation]);
        }
    }
}
