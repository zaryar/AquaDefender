using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BasicMovementPlaceholder : MonoBehaviour
{
    public float moveSpeed;
    public float moveDrag;

    public float horizontalInput;
    public float verticalInput;
    Vector3 moveDirection;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        myInput();
        rb.drag = moveDrag;
    }
    private void FixedUpdate()
    {
        myMove();
    }

    private void myInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector3(horizontalInput * Time.deltaTime, 0, verticalInput * Time.deltaTime);
    }
    private void myMove()
    {
        rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);
        Vector3 flatVel = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        if(flatVel.magnitude > moveSpeed) {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = limitedVel;
        }
    }

}

