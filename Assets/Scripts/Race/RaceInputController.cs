using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceInputController : MonoBehaviour,IDependency<RaceStateTracker>,IDependency<CarInputControll>
{
    
    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

    private CarInputControll carControll;
    public void Construct(CarInputControll obj) => carControll = obj;

    private void Start()
    {
        raceStateTracker.Started += OnRaceStarted;
        raceStateTracker.Completed += OnRaceFinished;

        carControll.enabled = false;
    }
    private void OnDestroy()
    {
        raceStateTracker.Started -= OnRaceStarted;
        raceStateTracker.Completed -= OnRaceFinished;
    }
    private void OnRaceStarted()
    {
        carControll.enabled = true;
    }
    private void OnRaceFinished()
    {
        carControll.Stop();
        carControll.enabled = false;
    }
}
