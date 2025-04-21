using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum RaceState
{
    Preparation,
    CoundDown,
    Race,
    Passed
}
public class RaceStateTracker : MonoBehaviour, IDependency<TrackPointCircuit>
{
    public event UnityAction PreparationStarted;
    public event UnityAction Started;
    public event UnityAction Completed;
    public event UnityAction<TrackPoint> TrackPointPassed;
    public event UnityAction<int> LapCompleted;

    private TrackPointCircuit trackPointCircuit;
    public void Construct(TrackPointCircuit trackPointCircuit) => this.trackPointCircuit = trackPointCircuit;

    [SerializeField] private Timer countdownTimer;
    [SerializeField] private int lapsToCompleted;

    public Timer CountdownTimer => countdownTimer;

    private RaceState state;
    public RaceState State => state;
    private void StartState(RaceState state)
    {
        this.state = state;
    }
    

    private void Start()
    {
        StartState(RaceState.Preparation);

        countdownTimer.enabled = false;

        countdownTimer.Finished += OnCountdownTimerFinished;

        trackPointCircuit.TrackPointTriggered += OnTrackPointTriggered;
        trackPointCircuit.LapCompleted += OnLapCompleted;
    }
    private void OnDestroy()
    {

        countdownTimer.Finished -= OnCountdownTimerFinished;
        trackPointCircuit.TrackPointTriggered -= OnTrackPointTriggered;
        trackPointCircuit.LapCompleted -= OnLapCompleted;
    }
    private void OnCountdownTimerFinished()
    {
        StartRace();
    }
    private void OnTrackPointTriggered(TrackPoint trackPoint)
    {
        TrackPointPassed?.Invoke(trackPoint);
    }
    private void OnLapCompleted(int lapAmount)
    {
        if(trackPointCircuit.Type == TrackType.Sprint)
        {
            CompleteRace();
        }

        if(trackPointCircuit.Type == TrackType.Cicrular)
        {
            if (lapAmount == lapsToCompleted)
                CompleteRace();
            else
                CompleteLap(lapAmount);
        }
    }
    public void LaunchPreparationStart()
    {
        if (state != RaceState.Preparation) return;

        StartState(RaceState.CoundDown);

        countdownTimer.enabled = true;

        PreparationStarted?.Invoke();
    }
    private void StartRace()
    {
        if (state != RaceState.CoundDown) return;

        StartState(RaceState.Race);

        Started?.Invoke();
    }
    private void CompleteRace()
    {
        if (state != RaceState.Race) return;

        StartState(RaceState.Passed);

        Completed?.Invoke();
    }
    private void CompleteLap(int lapAmount)
    {
        LapCompleted?.Invoke(lapAmount);
    }

    
}
