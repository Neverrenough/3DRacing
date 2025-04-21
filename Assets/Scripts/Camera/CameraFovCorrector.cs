using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFovCorrector : CarCameraComponent
{
    [SerializeField] private float minFieldOfView;
    [SerializeField] private float maxFieldofView;

    private float defaultFov;

    private void Start()
    {
        camera.fieldOfView = defaultFov;
    }
    private void Update()
    {
        camera.fieldOfView = Mathf.Lerp(minFieldOfView, maxFieldofView, car.NormalizeLinearVelocity);
    }
}
