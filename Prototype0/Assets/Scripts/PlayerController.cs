using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   //movement
    [Header("Movement")]
    public float movement_Speed = 2f;
    float horizontal_Input;
    float vertical_Input;
    public Transform orientation;
    Rigidbody rb;
    Vector3 move_Direction;
    public float sprint = 1f;

    //jumping
    public float jump_Height = 250f;
    public float jump_Cooldown;
    public float air_Multiplier;
    public bool ready_To_Jump = true;
    public TextMeshProUGUI speed;
    //ground check 
    [Header("Ground Check")]
    public float player_Height;
    public bool grounded;
    public LayerMask what_Is_Ground;
    public float ground_Drag;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        
    }
    private void FixedUpdate()
    {
        Move_Player();
    }
    void Update()
    {
        Movement_Input();
       // Ground_Check();
       Speed_Control();
        speed.text = "Speed: " + rb.velocity.magnitude.ToString();
    }

    private void Movement_Input()
    {
        horizontal_Input = Input.GetAxisRaw("Horizontal");
        vertical_Input = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && ready_To_Jump && grounded)
        {
            ready_To_Jump = false;
            grounded = false;
            Jump();
            Invoke(nameof(Reset_Jump), jump_Cooldown);
        }
    }

    private void Move_Player()
    {
        move_Direction = (orientation.forward * vertical_Input) + (orientation.right * horizontal_Input);
      

        if(grounded)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                
                rb.AddForce(move_Direction.normalized *( movement_Speed * 3)* 10f, ForceMode.Force);
            }

            rb.AddForce(move_Direction.normalized * movement_Speed * 10f, ForceMode.Force);
        }
           

        else if(grounded)
            rb.AddForce(move_Direction.normalized * movement_Speed * 10f * air_Multiplier, ForceMode.Force);
    }

    private bool Ground_Check()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, player_Height, what_Is_Ground);
        Debug.Log(grounded);
        if (grounded)
        {
            rb.drag = ground_Drag;
            return true;
        }
           
        else
        {
            rb.drag = 0;
            return false;
        }
            
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ground")
            grounded = true;
    }

    private void Jump()
    {
        rb.AddForce(transform.up* jump_Height, ForceMode.Impulse);
        
    }

    private void Reset_Jump()
    {
            ready_To_Jump = true;
    }

    private void Speed_Control()
    {
        Vector3 flat_Velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.y);

        if(rb.velocity.magnitude > movement_Speed)
        {
            Vector3 limited_Velocity = rb.velocity.normalized * movement_Speed;
            rb.velocity = new Vector3(limited_Velocity.x, rb.velocity.y, limited_Velocity.z);
        }
    }
}
