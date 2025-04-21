using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
class EngineIndicatorColor
{
    public float MaxRPM;
    public Color Color;
}
public class CarEngineIndicator : MonoBehaviour
{
    [SerializeField] private Car car;
    [SerializeField] private Image image;
    [SerializeField] private EngineIndicatorColor[] colors;

    private void Update()
    {
        image.fillAmount = car.EngineRPM / car.EngineMaxRPM;
        for(int i = 0; i< colors.Length; i++)
        {
            if(car.EngineRPM <= colors[i].MaxRPM)
            {
                image.color = colors[i].Color;
                break;
            }
        }
    }
}
