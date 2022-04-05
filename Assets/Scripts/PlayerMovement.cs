using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float runningSpeed = 20.0f;
    public float sideSpeed = 6.0f;
    public float jumpForce = 4.0f;
    private bool hasJumped = false;
    System.Random random = new System.Random();
    public GameObject wall;
    private Rigidbody body;
    private bool started, canTurn;
    Animator animator;
    private float score, timer, jumpTimer;
    private int lives = 3;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        //turnPlattforms = GameObject.FindGameObjectsWithTag("turnPlatform");
    }

    //private void checkDistance()
    //{
    //    foreach (GameObject plattform in turnPlattforms)
    //    {
    //        if (true)
    //        {

    //        }
    //    }
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "turnPlatform")
    //    {
    //        canTurn = true;
    //    }
    //}
    void Update()
    {

        timer += Time.deltaTime;
        
        if (timer > 1.5f)
        {
            started = true;
        }
        if (started)
        {
            // Rakt fram, vänster och höger justering med A och D

            //
            //if (animator.GetBool("leftStrafe") is false && animator.GetBool("rightStrafe") is false)
            //{
                animator.SetBool("IsRunning", true);
            //}

            transform.Translate(Vector3.forward * Time.deltaTime * runningSpeed);
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * Time.deltaTime * sideSpeed);
                //animator.SetBool("leftStrafe", true);
                //animator.SetBool("rightStrafe", false);
                //body.velocity = Vector3.forward * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * Time.deltaTime * sideSpeed);
                //animator.SetBool("leftStrafe", false);
                //animator.SetBool("rightStrafe", true);
            }
            //else
            //{
            //    animator.SetBool("leftStrafe", false);
            //    animator.SetBool("rightStrafe", false);
            //}

            // 90 graders sväng med pilar
            if (canTurn)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    transform.Rotate(0, 90, 0);
                    canTurn = false;
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    transform.Rotate(0, -90, 0);
                    canTurn = false;
                }
            }



            if (Input.GetKeyDown(KeyCode.Space) && hasJumped is false)
            {
                hasJumped = true;
                animator.SetBool("hasJumped", true);
                body.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
            if (hasJumped is true)
            {
                jumpTimer += Time.deltaTime;
                if (jumpTimer > 0.4)
                {
                    hasJumped = false;
                    animator.SetBool("hasJumped", false);
                    jumpTimer = 0;
                }
            }

            //if (animator.GetBool("swingJump") is true)
            //{
            //    hasSwingJumped = false;
            //    animator.
            //}


        }
    }

    //private bool checkAnimationStatus(string name)
    //{
    //    return checkAnimationStatus(name) && animator.GetCurrentAnimatorStateInfo(0).IsName(name);
    //}


    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger && other.gameObject.tag == "Wall")
        {
            transform.Rotate(0, 90, 0);
            other.isTrigger = false;
        }
        if (other.isTrigger && other.gameObject.tag == "turnPlatform")
        {
            canTurn = true;
        }
        //if (other.isTrigger && other.gameObject.tag == "Coin")
        //{
        //    score += 100;
        //}
        //Debug.Log("collision");
    }
}
