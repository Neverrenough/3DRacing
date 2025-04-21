using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SceneDependenciesContainer : Dependency
{
    [SerializeField] private RaceStateTracker raceStateTracker;
    [SerializeField] private CarInputControll carInputControll;
    [SerializeField] private TrackPointCircuit trackpointCircuit;
    [SerializeField] private Car car;
    [SerializeField] private CarCameraController carCameraController;
    [SerializeField] private RaceTimeTracker raceTimeTracker;
    [SerializeField] private RaceResultTime raceResultTime;
    protected override void BindAll(MonoBehaviour mono)
    {
        Bind<RaceStateTracker>(raceStateTracker, mono);
        Bind<CarInputControll>(carInputControll, mono);
        Bind<TrackPointCircuit>(trackpointCircuit, mono);
        Bind<Car>(car, mono);
        Bind<CarCameraController>(carCameraController, mono);
        Bind<RaceTimeTracker>(raceTimeTracker, mono);
        Bind<RaceResultTime>(raceResultTime, mono);
    }
    private void Awake()
    {
        FindAllObjectToBind();
    }
}
