using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


namespace Player
{
    public class Inputs : MonoBehaviour
    {
        [Header("Character Input Values")] public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool sprint;
        public bool aim;
        public bool shoot;

        [Header("Movement Settings")] public bool analogMovement;

        [Header("Mouse Cursor Settings")] public bool cursorLocked = true;
        public bool cursorInputForLook = true;

        public static Action OnInteractInput;

        public bool CursorLocked
        {
            get => cursorLocked;
            set
            {
                cursorLocked = value;
                cursorInputForLook = value;
                SetCursorState(value);
                look = Vector2.zero;
            }
        }

#if ENABLE_INPUT_SYSTEM
        public void OnMove(InputAction.CallbackContext context)
        {
            MoveInput(context.ReadValue<Vector2>());
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            if (cursorInputForLook)
            {
                LookInput(context.ReadValue<Vector2>());
            }
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            JumpInput(context.action.IsPressed());
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            SprintInput(context.action.IsPressed());
        }

        public void OnAim(InputAction.CallbackContext context)
        {
            AimInput(context.action.IsPressed());
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            ShootInput(context.action.IsPressed());
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnInteractInput.Invoke();
        }
#endif


        public void MoveInput(Vector2 newMoveDirection)
        {
            move = newMoveDirection;
        }

        public void LookInput(Vector2 newLookDirection)
        {
            look = newLookDirection;
        }

        public void JumpInput(bool newJumpState)
        {
            jump = newJumpState;
        }

        public void SprintInput(bool newSprintState)
        {
            sprint = newSprintState;
        }

        public void AimInput(bool newAimState)
        {
            aim = newAimState;
        }

        public void ShootInput(bool newShootState)
        {
            shoot = newShootState;
        }


        private void OnApplicationFocus(bool hasFocus)
        {
            SetCursorState(cursorLocked);
        }

        private void OnEnable()
        {
            SetCursorState(cursorLocked);
        }

        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }
}