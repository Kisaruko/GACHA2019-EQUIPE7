using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClockTimer : MonoBehaviour
{
    #region Attributes

    [SerializeField] private Transform _hand = null;
    [SerializeField] private int _duration = 60;
    [SerializeField] private float _tickDuration = 1;

    [SerializeField] private UnityEvent _onTimerEnd = null;


    #endregion


    #region Accessors

    public int Duration
    {
        get { return _duration; }
        set { _duration = Mathf.Abs(value); }
    }

    public float TickDuration
    {
        get { return _tickDuration; }
        set { _tickDuration = Mathf.Abs(value); }
    }

    #endregion


    #region Methods

    #region Lifecycle

    private void Start()
    {
        StartCoroutine("Timer");

    }

    #endregion

    #region Private

    private IEnumerator Timer()
    {
        SetHandRoll(_duration); //Sets the hand at his original roll
        for (int i = _duration; i >= 0; i--)
        {
            SetHandRoll(i);
            yield return new WaitForSeconds(_tickDuration);
        }

        EndTimer();
        yield return null;
    }

    /// <summary>
    /// Sets the hand roll depending the remaining time
    /// </summary>
    /// <param name="timeRemain"></param>
    private void SetHandRoll(int timeRemain)
    {
        if(_hand == null) { return;}

        float newRoll = (timeRemain * 360 / 60) % 360;
        _hand.localEulerAngles = Vector3.forward * newRoll;
    }


    private void EndTimer()
    {
       _onTimerEnd?.Invoke();
    }

    #endregion

    #region Public

    /// <summary>
    /// Stops the actual timer
    /// </summary>
    public void StopTimer()
    {
        StopAllCoroutines();
    }

    /// <summary>
    /// Starts timer with a given duration
    /// </summary>
    /// <param name="time"></param>
    public void StartTimer(int time)
    {
        Duration = time;
        StartCoroutine("Timer");
    }

    #endregion

    #endregion
}
