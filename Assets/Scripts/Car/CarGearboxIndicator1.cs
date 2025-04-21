using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarGearboxIndicator1 : MonoBehaviour
{
    [SerializeField] private Car car;
    [SerializeField] private Text text;

    private void Start()
    {
        car.GearChanged += OnGearChanged;
    }
    private void OnDestroy()
    {
        car.GearChanged -= OnGearChanged;
    }
    private void OnGearChanged(string gearName)
    {
        text.text = gearName;
    }
}
