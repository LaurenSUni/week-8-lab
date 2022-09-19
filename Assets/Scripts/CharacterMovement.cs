using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Vector3 movement;
    private float movementSqrMagnitude;
    public float WalkSpeed = 1.75f;

    // Update is called once per frame
    void Update()
    {
        GetMovementInput();
        CharacterPosition();
        CharacterRotation();
        WalkAnimation();
        FootstepAudio();
    }

    void GetMovementInput() {
        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");
        movement = Vector3.ClampMagnitude(movement, 1.0f);
        movementSqrMagnitude = movement.sqrMagnitude;
        Debug.Log(movement);
    }

    void CharacterPosition() {
        //move the current gameobject in the direction specified by the movement vector and Transform.Translate()
        transform.Translate(movement * Time.deltaTime * WalkSpeed, Space.World);
    }

    void CharacterRotation() {
        if (movement != Vector3.zero)
        {
            transform.rotation = (Quaternion.LookRotation(movement, Vector3.up));
        }
    }

    void WalkAnimation() {
    }

    void FootstepAudio() {
    }
}

