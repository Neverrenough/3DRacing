using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRaceItogi : MonoBehaviour, IDependency<RaceResultTime>, IDependency<RaceStateTracker>, IDependency<RaceTimeTracker>
{
    [SerializeField] private GameObject canvasItogi;
    [SerializeField] private Text playerRecordTime;
    [SerializeField] private Text nowTime;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;
    private RaceResultTime raceResultTime;
    public void Construct(RaceResultTime obj) => raceResultTime = obj;
    private RaceTimeTracker raceTimeTracker;
    public void Construct(RaceTimeTracker obj) => raceTimeTracker = obj;

    private void Start()
    {
        raceStateTracker.Completed += OnRaceCompleted;

        canvasItogi.SetActive(false);
    }
    private void OnRaceCompleted()
    {
        canvasItogi.SetActive(true);
        playerRecordTime.text = StringTime.SecondToTimeString(raceResultTime.PlayerRecordTime);
        nowTime.text = StringTime.SecondToTimeString(raceTimeTracker.CurrentTime);
    }
}
