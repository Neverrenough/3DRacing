using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiCoutDownTimer : MonoBehaviour, IDependency<RaceStateTracker>
{
    
    [SerializeField] private Text text;
    private Timer countDownTimer;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;
    private void Start()
    {
        raceStateTracker.PreparationStarted += OnPreparationStarted;
        raceStateTracker.Started += OnRaceStarted;

        text.enabled = false;
    }
    private void OnDestroy()
    {
        raceStateTracker.PreparationStarted -= OnPreparationStarted;
        raceStateTracker.Started -= OnRaceStarted;
    }
    private void OnRaceStarted()
    {
        text.enabled = false;
        enabled = false;
    }
    private void OnPreparationStarted()
    {
        text.enabled = true;
        enabled = true;
    }
    private void Update()
    {
        text.text = raceStateTracker.CountdownTimer.Value.ToString("F0");
        if(text.text == "0")
        {
            text.text = "GO!";
        }
    }

    
}
