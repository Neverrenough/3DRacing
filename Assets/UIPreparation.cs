using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPreparation : MonoBehaviour, IDependency<RaceStateTracker>
{
    [SerializeField] private GameObject canvasPreparation;
    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;
    private void Start()
    {
        canvasPreparation.SetActive(true);
        raceStateTracker.PreparationStarted += OnRaceCoundDown;
    }
    private void OnRaceCoundDown()
    {
        canvasPreparation.SetActive(false);
    }
}
