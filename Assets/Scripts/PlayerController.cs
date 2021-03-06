﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 5f;
    [SerializeField]
    private float thrustersForce = 1000f;

    [Header("Spring settings:")]
    [SerializeField]
    private JointDriveMode jointMode = JointDriveMode.Position;
    [SerializeField]
    private float jointSpring = 20f;
    [SerializeField]
    private float jointMaxForce = 40f;

    private ConfigurableJoint joint;
    private PlayerMotor motor;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();

        SetJointSettings(jointSpring);
    }

    void Update()
    {
        // calcula o moviemnto e velocidade um vector 3d
        float _xMove = Input.GetAxisRaw("Horizontal"); //(1,0,0) || (-1,0,0)
        float _zMove = Input.GetAxisRaw("Vertical");//(0,0,1) || (0,0,-1)

        Vector3 _moveHorizontal = transform.right * _xMove;
        Vector3 _moveVertical = transform.forward * _zMove;

        // movimento final
        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * speed;

        //aplica movimento
        motor.Move(_velocity);

        // calcula a rotacao de um verctor 3d.
        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

        //aplica rotacao
        motor.Rotation(_rotation);

        // calcula rotacao da camera no eixo Y
        float _xCameraRotationX = Input.GetAxisRaw("Mouse Y");

        float _cameraRotation = _xCameraRotationX * lookSensitivity;

        //aplica rotacao da camera
        motor.CameraRotation(_cameraRotation);

        // calcula forca
        Vector3 _thrusterForce = Vector3.zero;
        if (Input.GetButton("Jump"))
        {
            _thrusterForce = Vector3.up * thrustersForce;
            SetJointSettings(0f);
        } else
        {
            SetJointSettings(jointSpring);
        }
        //aplica forca no propusor
        motor.ApplyThrusterForce(_thrusterForce);

    }


    private void SetJointSettings(float _jointSpring)
    {
        joint.yDrive = new JointDrive {
            mode = jointMode,
            positionSpring = jointSpring,
            maximumForce = jointMaxForce
        };
    }
}
