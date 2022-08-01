using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    [Header("General Setup settings")]
    [Tooltip("How fast ship moves up and down based upon player input")] [SerializeField] float controlSpeed = 15f;
    [Tooltip("How far player moves horizontally")] [SerializeField] float xRange = 8f;
    [Tooltip("How far player moves vertically")] [SerializeField] float yRange = 6f;

    [Header("Screen position based tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = 2f;

    [Header("Player input based tuning")]
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float controlRollFactor = -20f;

    [Header("Laser gun array")]
    [Tooltip("Add all player lasers here")] [SerializeField] GameObject[] lasers;
    // Defining an array

    float xThrow, yThrow;

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        
        // When we pitch our rotation (x, up-down) also we wanna move up-down as position (y)
        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        // When we yaw our rotation (y, left-right) also we wanna move left-right as position (x)
        float yaw = transform.localPosition.x + positionYawFactor;
        float roll = xThrow * controlRollFactor;
        
        // Quaternions are used to represent rotations.
        // Quaternion.Euler(pitch, yaw, roll): Returns a rotation that rotates z degrees around the z axis, x degrees around the x axis, and y degrees around the y axis; applied in that order.
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        // Lrft-right
        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        // Use Clamp to restrict a value to a range that is defined by the minimum and maximum values.
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        // Up-down
        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float newYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(newYPos, -yRange, yRange);

        transform.localPosition = new Vector3
        (clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessFiring()
    {
        // if pushing fire button, in input manager "ctrl"
        if (Input.GetButton("Fire1"))
        {
            SetActivateLasers(true);
        }

        else
        {
            SetActivateLasers(false);
        }
    }

    void SetActivateLasers(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            // SetActive is for gameObjects and enabled is just for Components on the Object.
            // laser.SetActive(true);
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
}