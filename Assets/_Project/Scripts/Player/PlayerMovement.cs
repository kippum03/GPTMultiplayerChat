using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviourPun
{
    public float moveSpeed = 5f;
    public float turnSpeed = 180f;

    private CharacterController controller;
    private Camera playerCamera;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (photonView.IsMine)
        {
            // Attach the main camera to this player
            playerCamera = Camera.main;
            playerCamera.transform.SetParent(transform);
            playerCamera.transform.localPosition = new Vector3(0, 2, -5);
            playerCamera.transform.localRotation = Quaternion.Euler(10, 0, 0);
        }
    }

    void Update()
    {
        if (!photonView.IsMine) return;

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = transform.forward * v * moveSpeed;
        controller.Move(move * Time.deltaTime);

        transform.Rotate(0, h * turnSpeed * Time.deltaTime, 0);
    }
}