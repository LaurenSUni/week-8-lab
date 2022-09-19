using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Vector3 movement;
    private float movementSqrMagnitude;
    public float WalkSpeed = 1.75f;

    //create a public variable to store that animator of the Player gameObject
    public Animator playerAnimator;

    public AudioSource footstepSource;
    public AudioSource bgMusic;

    public Collider collider;

    //create public array of AudioClips called footstepClips
    public AudioClip[] footstepClips;

    // Update is called once per frame
    void Update()
    {
        GetMovementInput();
        CharacterPosition();
        CharacterRotation();
        WalkAnimation();
        FootstepAudio();
    }

    void OnTriggerExit()
    {
        //Debug.Log("Trigger Exit: " + < GameObject name > +" : " + < GameObject Position >); //do this
    }

    void GetMovementInput() {
        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");
        movement = Vector3.ClampMagnitude(movement, 1.0f);
        movementSqrMagnitude = movement.sqrMagnitude;
        //Debug.Log(movement);
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
        //set the MovingSpeed parameter of the Player's MovementAnimator to have the value of the movementSqrMagnitude
        playerAnimator.SetFloat("MovingSpeed", movementSqrMagnitude);
    }

//test3
    void FootstepAudio() {
        if (movementSqrMagnitude > 0.25f && footstepSource.isPlaying == false)
        {
            //associate the other clip in the footstepClips array with the audio souce (its going to flip-flop from one clip to another)
            if (footstepSource.clip == footstepClips[0]){
                footstepSource.clip = footstepClips[1];
            }
            else if (footstepSource.clip == footstepClips[1]){
                footstepSource.clip = footstepClips[0];
            }
            //set the volume of the audio source to the movementSqrMagnitude variable
            footstepSource.volume = movementSqrMagnitude;
            //play the audio source
            footstepSource.Play();
            //"Duck" the background music volume by reducing it to 0.5f
            bgMusic.volume = 0.5f;
        }
        else if (movementSqrMagnitude <= 0.3f && footstepSource.isPlaying == true)
        {
            //stop the footstepAudioSource from playing
            footstepSource.Stop();
            //return the background music to 1.0
            bgMusic.volume = 1.0f;
        }

    }

}

