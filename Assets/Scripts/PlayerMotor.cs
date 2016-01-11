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
    private Vector3 cameraRotation = Vector3.zero;

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
            cam.transform.Rotate(-cameraRotation);
        }
    }

    private void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            _rigidbody.MovePosition(_rigidbody.position + velocity * Time.fixedDeltaTime);
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

    internal void CameraRotation(Vector3 _cameraRotation)
    {
        cameraRotation = _cameraRotation;
    }
}
