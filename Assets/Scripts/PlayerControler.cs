using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public CharacterController controller;
    public Transform cameraDirection;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    Vector3 velocity;
    public float speed = 10f;
    public float speedCap = 40f;
    public float speedMin = 10f;
    public float gravity = -9f;
    public float turningAngleTime = 0.1f;
    public float jumpHeight = 3f;
    public float turningAngleSpeed;
    bool moving;
    bool onGround;
    public Canvas canvas;
    

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Jumping
        onGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        //Debug.Log(onGround);

        if(onGround && velocity.y <= 0)
        {
            velocity.y = 0f;
        }

        if(Input.GetButtonDown("Jump") && onGround)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        //Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //Walking
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            moving = true;
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraDirection.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turningAngleSpeed, turningAngleTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }
        else
        {
            moving = false;
        }

        //Sprint
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            while(speed < speedCap)
            {
                speed += 1f * Time.deltaTime;
            }
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            while(speed > speedMin)
            {
                speed -= 1f * Time.deltaTime;
            }
        }
    }

}
