using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    CharacterController controller;

    [SerializeField]
    [Range(0f,100f)]
    float speed = 5f;

    Vector3 velocity;

    [SerializeField]
    float gravity = -9.81f;
    [SerializeField]
    Transform groundCheck;
    [SerializeField]
    float groundDistance = 0.4f;
    [SerializeField]
    LayerMask groundMask;
    bool isGrounded;

    [SerializeField]
    float jumpHeight;


    [SerializeField]
    [Range(0f,  5f)]

    float maxClickDistance;

    [SerializeField]
    Camera camera;

    [SerializeField]
    LayerMask  layerClick;

    

    void Start()
    {
        
    }

    // Update is called once per frame
    float zSp = 0;
    float xSp = 0;

    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position,groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if(Input.GetButtonDown("Jump")&& isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        float x = 0;
        float z = 0;
        if (Input.GetKey(KeyCode.D))
             x = 1;
        if (Input.GetKey(KeyCode.A))
             x = -1;
        if (Input.GetKey(KeyCode.W))
             z = 1;
        if (Input.GetKey(KeyCode.S))
             z = -1;
        
        zSp = Mathf.Lerp(zSp,z, 0.1f);
        xSp = Mathf.Lerp(xSp, x, 0.1f);
        Vector3 move = transform.right * xSp + transform.forward * zSp;



        controller.Move(move * Time.deltaTime * speed);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


        if(Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            if(Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, maxClickDistance))
            {
                Button button;
                if(hit.collider.gameObject.TryGetComponent( out button))
                {
                    button.Click();
                }
            }
        }
    }
    public void Move()
    {
                controller.Move(transform.forward * Time.deltaTime * speed);
        }

    }

