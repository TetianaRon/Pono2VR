using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  Assets.StarterAssets.InputSystem;

[RequireComponent(typeof(StarterAssetsInputs))]
public class RotateCamera : MonoBehaviour
{
    private StarterAssetsInputs _input;
    private float _cinemachineTargetPitch;
    private float _rotationVelocity;

    public float RotationSpeed = 1f; 
    [Tooltip("How far in degrees can you move the camera up")]
    public float TopClamp = 90.0f;
    [Tooltip("How far in degrees can you move the camera down")]
    public float BottomClamp = -90.0f;

    [SerializeField]
    private  float _threshold = 0.1f;

    private float _thresholdSqr;

    void Awake()
    {
        _input = GetComponent<StarterAssetsInputs>();
        _thresholdSqr = _threshold * _threshold;

    }

    private void Update()
    {
        CameraRotation();


    }
    
    // Update is called once per frame
        private void CameraRotation()
            // if there is an input
        { 
 
                var MousePosition = Input.mousePosition;
                MousePosition.x = (Screen.height/2) - Input.mousePosition.y;
                MousePosition.y = -(Screen.width/2) + Input.mousePosition.x;
                var newAngle = (MousePosition * Time.deltaTime * RotationSpeed); 
            if(newAngle.sqrMagnitude>_thresholdSqr)
                transform.localRotation = Quaternion.Euler(
                    transform.localRotation.eulerAngles.x + newAngle.x,
                    transform.localRotation.eulerAngles.y + newAngle.y,0 );

        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
}
