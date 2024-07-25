using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyCharacterMovement;
using SFRemastered.InputSystem;

namespace SFRemastered
{
    public class InputPoller : MonoBehaviour
    {
        public BlackBoard blackBoard;
        public PlayerMovement playerMovement;

        private void Update()
        {
            // Poll movement Input
            Vector2 movementInput = new Vector2
            {
                x = InputManager.instance.move.x,
                y = InputManager.instance.move.y
            };

            // Add movement input in world space
            Vector3 movementDirection = Vector3.zero;
            Vector3 wallMovementDirection = Vector3.zero;

            movementDirection += Vector3.right * movementInput.x;
            movementDirection += Vector3.forward * movementInput.y;
            
            wallMovementDirection +=movementInput.x * blackBoard.characterVisual.transform.right;
            if (movementInput.y >= 0)
            {
                wallMovementDirection += blackBoard.characterVisual.transform.up * movementInput.y;
            }
            
            // If Camera is assigned, add input movement relative to camera look direction

            if (blackBoard.camera != null)
            {
                movementDirection = movementDirection.relativeTo(blackBoard.camera.transform);
            }
            
            blackBoard.wallMoveDirection = wallMovementDirection;
            blackBoard.moveDirection = movementDirection;
            blackBoard.jump = InputManager.instance.jump.Pressing;
            blackBoard.sprint = InputManager.instance.sprint.Pressing;
            blackBoard.swing = InputManager.instance.swing.Pressing;
            blackBoard.zip = InputManager.instance.zip.Pressing;
            blackBoard.isGrounded = playerMovement.IsGrounded();
        }
    }
}
