﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色跳跃控制
/// </summary>
public class JumpController : MonoBehaviour
{
    public LayerMask groundLayers;

    public float speed = 5;

    public float jumpForce = 7;

    private Rigidbody rb;

    private TerrainCollider col;

    private bool onGround;

    #region Monobehaviour API

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<TerrainCollider>();

    }

    /**
     * 人物跳跃
     * */
    public void jump()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        rb.AddForce(movement * speed);
        Debug.Log("in jump fun");
        if (onGround)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Terrain")
        {
            onGround = true;
            Debug.Log("onGround");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Terrain")
        {
            onGround = false;
            Debug.Log("not onGround");
        }
    }

    #endregion
}