using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 5f;

    private PlayerMotor motor;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();
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
        float _xCameraRotation = Input.GetAxisRaw("Mouse Y");

        Vector3 _cameraRotation = new Vector3(_xCameraRotation, 0f,0f);

        //aplica rotacao da camera
        motor.CameraRotation(_cameraRotation);


    }
}
