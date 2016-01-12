using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private Rigidbody _rigidbody;
    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float cameraRotationX = 0f;
    private float currentCameraRotationX = 0f;
    private Vector3 thrusterForce = Vector3.zero;

    private float cameraRotationLimit = 85f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    private void PerformRotation()
    {
        _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(rotation));
        
        if (cam != null)
        {
            // seta nossa rotacao da camera
            currentCameraRotationX -= cameraRotationX;
            currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

            // aplica a rotacao da camera
            cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f,0f);
        }
    }

    private void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            _rigidbody.MovePosition(_rigidbody.position + velocity * Time.fixedDeltaTime);
        }

        if (thrusterForce != Vector3.zero)
        {
            _rigidbody.AddForce(thrusterForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }

    // pega o movimento do verctor
    internal void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    // pega a rotacao do verctor
    internal void Rotation(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    internal void CameraRotation(float _cameraRotationX)
    {
        cameraRotationX = _cameraRotationX;
    }

    // pega a forca do verctor para o propusor
    internal void ApplyThrusterForce(Vector3 _thrusterForce)
    {
        thrusterForce = _thrusterForce;
    }
}
