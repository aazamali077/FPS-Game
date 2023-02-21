using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{

    public float walkspeed = 8f;
    public float maxVelocityChange = 10f;
    public float sprintspeed = 14f;
    [Space]
    public float jumpheight;

    private Vector2 input;
    private Rigidbody rb;

    private bool sprinting;
    private bool Jumping;

    private bool Grounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        input.Normalize();

        sprinting = Input.GetButton("Sprint");
        Jumping = Input.GetButton("Jump");
    }

    private void OnTriggerStay(Collider other)
    {
        Grounded= true;
    }

    private void FixedUpdate()
    {
        if(Grounded)
        {
            if (Jumping)
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpheight, rb.velocity.z);

            }

            else if (input.magnitude > 0.5f)
            {  
                rb.AddForce(CalculateMovement(sprinting ? sprintspeed : walkspeed), ForceMode.VelocityChange);
            }
            else
            {
                var velocity1 = rb.velocity;
                velocity1 = new Vector3(velocity1.x * 0.2f * Time.fixedDeltaTime, velocity1.y, velocity1.z * 0.2f * Time.fixedDeltaTime);
                rb.velocity = velocity1;
            }

        }

        Grounded= false;

     
        
    }

    Vector3 CalculateMovement(float speed)
    {
        Vector3 targetvelocity = new Vector3(input.x, 0, input.y);
        targetvelocity = transform.TransformDirection(targetvelocity);

        targetvelocity*=speed;
        Vector3 velocuty = rb.velocity;

        if (input.magnitude > 0.5)
        {
            Vector3 velocitychange = targetvelocity - velocuty;
            velocitychange.x = Mathf.Clamp(velocitychange.x, -maxVelocityChange, maxVelocityChange);
            velocitychange.z = Mathf.Clamp(velocitychange.z, -maxVelocityChange, maxVelocityChange);
            velocitychange.y = 0;


            return (velocitychange);
        }
        else
        {
            return new Vector3();
        }
    }
}
