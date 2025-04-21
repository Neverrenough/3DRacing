using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITrackTimer : MonoBehaviour, IDependency<RaceTimeTracker>,IDependency<RaceStateTracker>
{
    [SerializeField] private Text text;

    private RaceTimeTracker raceTimeTracker;
    public void Construct(RaceTimeTracker obj) => raceTimeTracker = obj;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;
    private void Start()
    {
        raceStateTracker.Started += OnRaceStarted;
        raceStateTracker.Completed += OnRaceCompleted;

        text.enabled = false;
    }
    private void OnDestroy()
    {
        raceStateTracker.Started -= OnRaceStarted;
        raceStateTracker.Completed -= OnRaceCompleted;
    }
    private void Update()
    {
        text.text = StringTime.SecondToTimeString(raceTimeTracker.CurrentTime);
    }
    private void OnRaceStarted()
    {
        text.enabled = true;
    }
    private void OnRaceCompleted()
    {
        text.enabled = false;
    }
}
