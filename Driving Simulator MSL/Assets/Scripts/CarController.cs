using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public static CarController instance;
    public enum Axel
    {
        Front,
        Rear
    }

    [Serializable]
    public struct Wheel
    {
        public GameObject wheelModel;
        public WheelCollider wheelCollider;
        public Axel axel;
    }

    [SerializeField] private float maxAcceleration = 30f;
    [SerializeField] private float brakeAcceleration = 50f;

    [SerializeField] private float turnSensitivity = 1.0f;
    [SerializeField] private float maxSteerAngle = 30.0f;

    [SerializeField] private List<Wheel> wheels;

    private float moveInput;
    private float steerInput;

    private float finalMove;

    private Rigidbody carRB;


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        carRB = GetComponent<Rigidbody>();
        carRB.centerOfMass = new Vector3(0, -0.5f, 0);
    }

    private void Update()
    {
        GetInputs();
        AnimateWheels();
    }
    private void LateUpdate()
    {
        Move();
        Steer();
        Brake();
    }

    /*
    private void OnEnable()
    {
        PlayerManager.OnGearShift += ShiftGear;
    }
    private void OnDisable()
    {
        PlayerManager.OnGearShift -= ShiftGear;
    }

    public void ShiftGear()
    {
        Debug.Log("ggeerrrar");
    }
    */
    void GetInputs()
    {
        moveInput = MathF.Max(0, Input.GetAxis("Vertical"));
        steerInput = Input.GetAxis("Horizontal");
    }

    private void Move()
    {
        if (PlayerManager.Instance.isEngineStarted)
        {
            finalMove = PlayerManager.Instance.currentState switch
            {
                PlayerManager.GearState.Drive => MathF.Abs(moveInput),
                PlayerManager.GearState.Reverse => -Mathf.Abs(moveInput),
                PlayerManager.GearState.Neutral => 0,
                PlayerManager.GearState.Park => 0,
                _ => 0,
            };
        } else
        {
            finalMove = 0;
        }

            /*
            switch (PlayerManager.Instance.currentState)
             {
                 case PlayerManager.GearState.Drive:
                     finalMove = Mathf.Abs(moveInput);
                     break;
                 case PlayerManager.GearState.Reverse:
                     finalMove = -(Mathf.Abs(moveInput));
                     break;
                 case PlayerManager.GearState.Neutral:
                     finalMove = 0;
                     break;
                 case PlayerManager.GearState.Park:
                     finalMove = 0;
                     break;
             }
            */

            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.motorTorque = finalMove * 600 * maxAcceleration * Time.deltaTime;
            }
    }

    private void Steer()
    {
        foreach(var wheel in wheels)
        {
            if (wheel.axel == Axel.Front)
            {
                var _steerAngle = steerInput * turnSensitivity * maxSteerAngle;
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, 0.6f); 
            }
        }
    }

    private void Brake()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.S) || PlayerManager.Instance.currentState == PlayerManager.GearState.Park || PlayerManager.Instance.isEngineStarted == false)
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 300 * brakeAcceleration * Time.deltaTime;
            }
        } 
        else
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 0;
            }
        }
    }
    private void AnimateWheels()
    {
        foreach (var wheel in wheels)
        {
            Quaternion rotation;
            Vector3 position;
            wheel.wheelCollider.GetWorldPose(out position, out rotation);
            wheel.wheelModel.transform.position = position;
            wheel.wheelModel.transform.rotation = rotation;
        }
    }
}
