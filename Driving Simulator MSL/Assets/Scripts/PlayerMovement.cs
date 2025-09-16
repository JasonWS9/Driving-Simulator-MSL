using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public static PlayerMovement Instance;

    public float motorForce = 100f;
    public float brakeForce = 1000f;
    public float maxSteerAngle = 30f;

    [SerializeField] private WheelCollider frontLeftWheelColldier;
    [SerializeField] private WheelCollider frontRightWheelColldier;
    [SerializeField] private WheelCollider backLeftWheelColldier;
    [SerializeField] private WheelCollider backRightWheelColldier;

    [SerializeField] Transform frontLeftWheelTransform;
    [SerializeField] Transform frontRightWheelTransform;
    [SerializeField] Transform backLeftWheelTransform;
    [SerializeField] Transform backRightWheelTransform;

    private float horizontalInput;
    private float verticalInput;

    private float currentSteerAngle;
    private float currentBreakForce;
    private bool isBreaking;

    private void Start()
    {
        Instance = this;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0f, -0.5f, 0f);
    }

    void Update()
    {
        GetInput();
    }
    private void FixedUpdate()
    {
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void HandleMotor()
    {
        frontLeftWheelColldier.motorTorque = verticalInput * motorForce;
        frontRightWheelColldier.motorTorque = verticalInput * motorForce;

        currentBreakForce = isBreaking ? brakeForce : 0f;

        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        frontLeftWheelColldier.brakeTorque = currentBreakForce;
        frontRightWheelColldier.brakeTorque = currentBreakForce;
        backLeftWheelColldier.brakeTorque = currentBreakForce;
        backRightWheelColldier.brakeTorque = currentBreakForce;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;

        frontLeftWheelColldier.steerAngle = currentSteerAngle;
        frontRightWheelColldier.steerAngle = currentSteerAngle;
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 wheelPosition;
        Quaternion wheelRotation;
        wheelCollider.GetWorldPose(out wheelPosition, out wheelRotation);
        wheelTransform.position = wheelPosition;
        wheelTransform.rotation = wheelRotation;
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelColldier, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelColldier, frontRightWheelTransform);
        UpdateSingleWheel(backLeftWheelColldier, backLeftWheelTransform);
        UpdateSingleWheel(backRightWheelColldier, backRightWheelTransform);
    }
}
