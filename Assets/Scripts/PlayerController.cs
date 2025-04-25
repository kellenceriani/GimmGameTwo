// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.InputSystem;

// public class PlayerController : MonoBehaviour
// {
//     public float moveSpeed = 5f;
//     private Vector2 moveInput;
//     private InputMaster controls; // Reference to the generated InputMaster class

//     private void Awake()
//     {
//         controls = new InputMaster(); // Initialize the InputMaster
//     }

//     private void OnEnable()
//     {
//         // Subscribe to the Movement action, correctly referenced here
//         controls.Player.Movement.performed += OnMove;
//         controls.Player.Movement.canceled += OnMove;  // Reset movement on cancel
//         controls.Enable(); // Enable controls
//     }

//     private void OnDisable()
//     {
//         // Unsubscribe from the Movement action
//         controls.Player.Movement.performed -= OnMove;
//         controls.Player.Movement.canceled -= OnMove;
//         controls.Disable(); // Disable controls
//     }

//     private void Update()
//     {
//         MovePlayer(); // Call movement logic every frame
//     }

//     // This function gets called whenever the Movement action is performed or canceled
//     public void OnMove(InputAction.CallbackContext context)
//     {
//         moveInput = context.ReadValue<Vector2>(); // Read movement vector from input
//     }

//     // Function to move the player based on input
//     private void MovePlayer()
//     {
//         Vector3 move = new Vector3(-moveInput.x, 0, -moveInput.y) * moveSpeed * Time.deltaTime; // Negate the y component
//         transform.Translate(move, Space.World); // Move the player
//         Debug.Log("Movement Vector: " + moveInput); // Debug the movement vector
//     }
// }
