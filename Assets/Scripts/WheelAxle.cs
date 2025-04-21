using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WheelAxle
{
    [SerializeField] private WheelCollider leftWheelColider;
    [SerializeField] private WheelCollider rightWheelColider;

    [SerializeField] private Transform leftWheelMesh;
    [SerializeField] private Transform rightWheelMesh;

    [SerializeField] private bool isMotor;
    [SerializeField] private bool isSteer;

    [SerializeField] private float wheelWidth;

    [SerializeField] private float antiRollForce;

    [SerializeField] private float additionalWheelDownForce;

    [SerializeField] private float baseForwardStifness = 1.5f;
    [SerializeField] private float stabilityForwardFactor = 1.0f;
    [SerializeField] private float stabilitySideWaysFactor = 1.0f;

    [SerializeField] private float baseSidewaysStifness = 2.0f;

    private WheelHit leftWheelHit;
    private WheelHit rightWheelHit;

    public bool IsMotor => isMotor;
    public bool IsSteer => isSteer;

    public void Update()
    {
        UpdateWheelHits();

        ApplyAntiRoll();
        ApplyDownForce();
        CorrectStifness();

        SyncMeshTransform();
    }
    public void ConfigureVehicleSubsteps(float speedTreshold,int speedBelowTheshold, int stepsAboveTreshold)
    {
        leftWheelColider.ConfigureVehicleSubsteps(speedTreshold, speedBelowTheshold, stepsAboveTreshold);
        rightWheelColider.ConfigureVehicleSubsteps(speedTreshold, speedBelowTheshold, stepsAboveTreshold);
    }
    private void UpdateWheelHits()
    {
        leftWheelColider.GetGroundHit(out leftWheelHit);
        rightWheelColider.GetGroundHit(out rightWheelHit);
    }
    private void ApplyAntiRoll()
    {
        float travelL = 1.0f;
        float travelR = 1.0f;

        if (leftWheelColider.isGrounded == true)
            travelL = (-leftWheelColider.transform.InverseTransformPoint(leftWheelHit.point).y - leftWheelColider.radius) / leftWheelColider.suspensionDistance;
        if (rightWheelColider.isGrounded == true)
            travelR = (-rightWheelColider.transform.InverseTransformPoint(rightWheelHit.point).y - rightWheelColider.radius) / rightWheelColider.suspensionDistance;

        float forceDir = (travelL - travelR);

        if(leftWheelColider.isGrounded == true)
            leftWheelColider.attachedRigidbody.AddForceAtPosition(leftWheelColider.transform.up  * -forceDir * antiRollForce, leftWheelColider.transform.position);
        if (rightWheelColider.isGrounded == true)
            rightWheelColider.attachedRigidbody.AddForceAtPosition(rightWheelColider.transform.up * forceDir * antiRollForce, rightWheelColider.transform.position);
    }
    private void ApplyDownForce()
    {
        if (leftWheelColider.isGrounded == true)
            leftWheelColider.attachedRigidbody.AddForceAtPosition(leftWheelHit.normal * -additionalWheelDownForce *
                leftWheelColider.attachedRigidbody.velocity.magnitude, leftWheelColider.transform.position);
        if (rightWheelColider.isGrounded == true)
            rightWheelColider.attachedRigidbody.AddForceAtPosition(rightWheelHit.normal * -additionalWheelDownForce *
                rightWheelColider.attachedRigidbody.velocity.magnitude, rightWheelColider.transform.position);
    }
    private void CorrectStifness()
    {
        WheelFrictionCurve leftForward = leftWheelColider.forwardFriction;
        WheelFrictionCurve rightForward = rightWheelColider.forwardFriction;

        WheelFrictionCurve leftSideways = leftWheelColider.sidewaysFriction;
        WheelFrictionCurve rightSideways = rightWheelColider.sidewaysFriction;

        leftForward.stiffness = baseForwardStifness + Mathf.Abs(leftWheelHit.forwardSlip) * stabilityForwardFactor;
        rightForward.stiffness = baseForwardStifness + Mathf.Abs(rightWheelHit.forwardSlip) * stabilityForwardFactor;

        leftSideways.stiffness = baseSidewaysStifness + Mathf.Abs(leftWheelHit.forwardSlip) * stabilitySideWaysFactor;
        rightSideways.stiffness = baseSidewaysStifness + Mathf.Abs(rightWheelHit.forwardSlip) * stabilitySideWaysFactor;

        leftWheelColider.forwardFriction = leftForward;
        rightWheelColider.forwardFriction = rightForward;

        leftWheelColider.sidewaysFriction = leftSideways;
        rightWheelColider.sidewaysFriction = rightSideways;
    }
    public void ApplySteerAngle(float steerAngle, float wheelBaseLength)
    {
        if (isSteer == false) return;

        float radius = Mathf.Abs(wheelBaseLength * Mathf.Tan(Mathf.Deg2Rad * (90 - Mathf.Abs(steerAngle))));
        float angleSing = Mathf.Sign(steerAngle);

        if(steerAngle > 0)
        {
            leftWheelColider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius + (wheelWidth * 0.5f))) * angleSing;
            rightWheelColider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius - (wheelWidth * 0.5f))) * angleSing;
        }
        else if (steerAngle < 0)
        {
            leftWheelColider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius - (wheelWidth * 0.5f))) * angleSing;
            rightWheelColider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(wheelBaseLength / (radius + (wheelWidth * 0.5f))) * angleSing;
        }
        else
        {
            leftWheelColider.steerAngle = 0;
            rightWheelColider.steerAngle = 0;
        }
    }
    public void ApplyMotorTorque(float motorTorque)
    {
        if (isMotor == false) return;

        leftWheelColider.motorTorque = motorTorque;
        rightWheelColider.motorTorque = motorTorque;
    }
    public void ApplyBreakTorque(float brakeTorque)
    {
        leftWheelColider.brakeTorque = brakeTorque;
        rightWheelColider.brakeTorque = brakeTorque;
    }
    public float GetAverageRpm()
    {
        return (leftWheelColider.rpm + rightWheelColider.rpm) * 0.5f;
    }
    public float GetRadius()
    {
        return leftWheelColider.radius;
    }
    private void SyncMeshTransform()
    {
        UpdateWheelTransform(leftWheelColider, leftWheelMesh);
        UpdateWheelTransform(rightWheelColider, rightWheelMesh);
    }
    private void UpdateWheelTransform(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 position;
        Quaternion rotation;
        wheelCollider.GetWorldPose(out position, out rotation);

        wheelTransform.position = position;
        wheelTransform.rotation = rotation;
    }
}
