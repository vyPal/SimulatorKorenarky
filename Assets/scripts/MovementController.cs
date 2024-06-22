using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class MovementController : MonoBehaviour {
    public float _speed = 6;
    public float _jumpForce = 6;
    private Rigidbody _rig;
    private Vector2 _input;
    private Vector3 _movementVector;
    private void Start () {
        _rig = GetComponent<Rigidbody> ();
        //Need to freez rotation so the player do not flip over
        _rig.freezeRotation = true;
    }
    private void Update () {
        //Cleanerway to get input
        _input = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
        if (Input.GetButtonDown ("Jump") && IsGrounded ()) {
            _rig.AddForce (Vector3.up * _jumpForce, ForceMode.Impulse);
        }
        Vector3 moveDir = new Vector3(_input.x, 0f, _input.y);
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }
    private void FixedUpdate () {
        //Keep the movement vector aligned with the player rotation
        _movementVector = _input.x * Vector3.right * _speed + _input.y * Vector3.forward * _speed;
        //Apply the movement vector to the rigidbody without effecting gravity
        _rig.velocity = new Vector3 (_movementVector.x, _rig.velocity.y, _movementVector.z);
    }
    private bool IsGrounded () {
        float rayLength = 1.5f; // Zkuste zvýšit tuto hodnotu, pokud detekce selhává
        Vector3 rayStartAdjustment = new Vector3(0f, 0.005693734f, 0f);
        Vector3 rayStart = transform.position + rayStartAdjustment;
        Vector3 rayDirection = Vector3.down;

        // Vizualizace paprsku ve scéně
        Debug.DrawRay(rayStart, rayDirection * rayLength, Color.red);

        if (Physics.Raycast(rayStart, rayDirection, rayLength)) {
            return true;
        } else {
            return false;
        }
    }
}