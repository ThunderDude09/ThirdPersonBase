﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalSpeed = 4.0F;

    [Header("Movement Variables")]
    [SerializeField]
    float moveSpeed = 3.0f;

    float moveSpeed2 = 3.0f;

    [Header("References")]
    [SerializeField]
    Transform mainCamera;
    [SerializeField]
    BoxCollider swordCollider;


    public Transform aimCamera;

    public float throwForce = 50;

    Rigidbody rb;
    Animator anim;

    bool isPortalActive1 = true;

    bool playerIsAiming = false;
    bool StartedAiming;
    float timeSinceButtonHeld = 0.0f;

    bool startedCombo = false;
    float timeSinceButtonPressed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsAiming == false)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            var camForward = mainCamera.forward;
            var camRight = mainCamera.right;

            camForward.y = 0;
            camForward.Normalize();
            camRight.y = 0;
            camRight.Normalize();

            var moveDirection = (camForward * v * moveSpeed) + (camRight * h * moveSpeed);

            transform.LookAt(transform.position + moveDirection);
            rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);

            anim.SetFloat("moveSpeed", Mathf.Abs(moveDirection.magnitude));
        }
        else if (playerIsAiming == true)
        {
            //aimCamera.SetActive(true);
            //mainCamera = aimCamera;
            
            float h2 = horizontalSpeed * Input.GetAxis("Mouse X");
            float v2 = Input.GetAxis("Vertical");
            transform.Rotate(0, h2, 0);
            /*var camForward2 = aimCamera.forward;
            var camRight2 = aimCamera.right;

            camForward2.y = 0;
            camForward2.Normalize();
            camRight2.y = 0;
            camRight2.Normalize();

            var moveDirection = (camForward2 * v2 * moveSpeed2) + (camRight2 * h2 * moveSpeed2);*/
        }


        if (Input.GetButtonDown("Jump") && !startedCombo)
        {
            anim.SetTrigger("swordCombo");
            startedCombo = true;
        }

        if (Input.GetButtonDown("Jump") && startedCombo)
        {
            timeSinceButtonPressed = 0;
        }
        if(isPortalActive1 == false)
        {
            if (Input.GetMouseButton(1) && !StartedAiming)
            {

                playerIsAiming = true;
                Debug.Log("Left Click");
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Right Click");
                    anim.SetTrigger("ThrowSword");
                    TurnOnSwordCollider();
                    //throwSword();
                }
            }
            if (Input.GetMouseButtonUp(1))
            {
                playerIsAiming = false;
            }
            if (Input.GetMouseButton(1) && !StartedAiming)
            {
                timeSinceButtonHeld = 0;
            }
        }
        

        timeSinceButtonPressed += Time.deltaTime;

        timeSinceButtonHeld += Time.deltaTime;
    }

    public void PotentialComboEnd()
    {
        TurnOffSwordCollider();

        if (timeSinceButtonPressed < 0.5f)
            return;

        anim.SetTrigger("stopCombo");
        startedCombo = false;
        timeSinceButtonPressed = 0;
        
    }

    public void EndOfCombo()
    {
        startedCombo = false;
        timeSinceButtonPressed = 0;
        TurnOffSwordCollider();
    }

    public void TurnOnSwordCollider()
    {
        swordCollider.enabled = true;
    }

    public void TurnOffSwordCollider()
    {
        swordCollider.enabled = false;
    }

    /*void throwSword()
    {
        transform.parent = null;
        //Transform t = rb.GetComponentInChildren<Transform>().Find("Axe Model");
        transform.Translate(0, 100, 0);
    }*/

    
}
