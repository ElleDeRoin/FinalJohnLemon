using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Animator m_Animator;

    public InputAction MoveAction;
    public InputAction SprintAction;      // ⬅ NEW

    public float walkSpeed = 1.0f;
    public float sprintSpeed = 3.0f;      // ⬅ NEW
    public float turnSpeed = 20f;

    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    bool isSprinting = false;             // ⬅ NEW

    void Start ()
    {
        m_Rigidbody = GetComponent<Rigidbody>();

        MoveAction.Enable();
        SprintAction.Enable();            // ⬅ NEW

        m_Animator = GetComponent<Animator>();
    }

    void FixedUpdate ()
    {
        // --- INPUT ---
        var pos = MoveAction.ReadValue<Vector2>();
        float horizontal = pos.x;
        float vertical = pos.y;

        // Sprint input
        isSprinting = SprintAction.IsPressed();   // ⬅ NEW

        // --- MOVEMENT VECTOR ---
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        bool hasMovement = !Mathf.Approximately(horizontal, 0f) || !Mathf.Approximately(vertical, 0f);
        m_Animator.SetBool("IsWalking", hasMovement);

        // --- ROTATION ---
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);
        m_Rigidbody.MoveRotation(m_Rotation);

        // --- SPEED CHOICE ---
        float currentSpeed = isSprinting ? sprintSpeed : walkSpeed;

        // --- MOVE ---
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * currentSpeed * Time.deltaTime);
    }
}
