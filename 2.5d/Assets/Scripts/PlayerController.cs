using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 5f;

    public float velY = 0f;
    public float maxY = 3f;

    public bool isGrounded = false;

    Rigidbody rb;
    Quaternion cam;
    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main.transform.rotation;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = new Quaternion(0f,cam.y, 0f, 1f);
        CheckForFlip(); 
        Move();
        UseGravity();
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        rb.velocity = Vector3.forward * vertical + Vector3.right * horizontal;
        rb.velocity += new Vector3(0f,velY,0f);
    }

    void UseGravity()
    {
        if(!isGrounded)
        {
            velY += Physics.gravity.y;
            velY = Mathf.Clamp(velY, -maxY, maxY);
        }
        else
        {
            velY = 0;
        }
    }

    void CheckForFlip()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            sr.flipX = true;
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            sr.flipX = false;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if(col.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
