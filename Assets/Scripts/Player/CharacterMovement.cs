using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Camera cam;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;

    public float lookSpeed = 2f;
    public float lookXLimit = 45f;

    Vector3 moveDir = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;

    CharacterController characterController;
    Animator anim;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Vector3 foward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float currSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDir.y;
        float isMoving = Mathf.Abs(Input.GetAxisRaw("Vertical")) + Mathf.Abs(Input.GetAxisRaw("Horizontal"));

        moveDir = (foward * currSpeedX) + (right * currSpeedY);

        if(Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDir.y = jumpPower;
        }
        else
        {
            moveDir.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDir.y -= gravity * Time.deltaTime;
        }

        if(isMoving != 0)
        {
            characterController.Move(moveDir * Time.deltaTime);
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
      

        if (canMove)
        {
            Transform oldCamTransform = cam.transform;
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX , -lookXLimit, lookXLimit);
           if(InventoryManager.inMenu == false)
            {
                cam.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            }
            else if (InventoryManager.inMenu)
            {
                cam.transform.localRotation = oldCamTransform.localRotation;
            }
           

        }
    }
}
