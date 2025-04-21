using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarSpeedIndicator : MonoBehaviour
{
    [SerializeField] private Car car;
    [SerializeField] private Text text;
    private void Update()
    {
        text.text = car.LinearVelocity.ToString("F0");
    }
}
