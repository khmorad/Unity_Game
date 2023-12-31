using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 5f;
    [SerializeField] private float sprintMultiplier;

    public Rigidbody2D rb;
    public Animator animator;
    private Vector2 movement;
    public Health health;

    public LayerMask solidObjectsLayer;

    private bool isMovingUp = true;
    private bool isMovingDown = true;
    private bool isMovingLeft = true;
    private bool isMovingRight = true;
    
    private bool isRunning = false;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
    }
    // Update is called once per frame
    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.LeftShift)) // Check if LeftShift is pressed
        {

            isRunning = !isRunning;

        }


        animator.SetBool("RunningLeft", isRunning);




        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.Space))
        {
            if (movement.x > 0)
            {
                animator.SetTrigger("AttackRight");
            }
            else if (movement.x < 0)
            {
                animator.SetTrigger("AttackLeft");
            }
            else if (movement.y > 0)
            {
                animator.SetTrigger("AttackUp");
            }
            else if (movement.y < 0)
            {
                animator.SetTrigger("AttackDown");
            }
            else
            {
                 // If the player is idle, determine the attack direction based on the mouse position
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            float xPos = mousePos.x - rb.position.x;
            float yPos = mousePos.y - rb.position.y;

            if(Mathf.Abs(xPos) > Mathf.Abs(yPos)){
                if(xPos > 0){
                    animator.SetTrigger("AttackRight");
                }else{
                    animator.SetTrigger("AttackLeft");
                }
            }else{
                if(yPos > 0){
                    animator.SetTrigger("AttackUp");
                }else{
                    animator.SetTrigger("AttackDown");
                }
            }
            }
        }
    }

    private void FixedUpdate()
    {
        if (isRunning)
        {
            movement *= sprintMultiplier;
        }
        if (isMovingUp && isMovingDown && isMovingLeft && isMovingRight)
        {


            var targetPos = rb.position + movement * moveSpeed * Time.fixedDeltaTime;
            if (IsWalkable(targetPos))
            {
                rb.MovePosition(targetPos);
            }
        }

    }

    private bool IsWalkable(Vector2 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.5f, solidObjectsLayer) != null)
        {
            return false;
        }
        return true;
    }


    public void UpMovementLock()
    {
        isMovingUp = false;
    }

    public void UpUnlockMovement()
    {
        isMovingUp = true;
    }

    public void DownMovementLock()
    {
        isMovingDown = false;
    }

    public void DownUnlockMovement()
    {
        isMovingDown = true;
    }
    public void LeftMovementLock()
    {
        isMovingLeft = false;
    }

    public void LeftUnlockMovement()
    {
        isMovingLeft = true;
    }
    public void RightMovementLock()
    {
        isMovingRight = false;
    }

    public void RightUnlockMovement()
    {
        isMovingRight = true;
    }
}
