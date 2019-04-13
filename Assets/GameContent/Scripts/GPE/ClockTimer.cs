using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClockTimer : MonoBehaviour
{
    #region Attributes

    [SerializeField] private Transform _hand = null;
    [SerializeField] private int _duration = 60;

    [SerializeField] private UnityEvent _onTimerEnd = null; 


    #endregion

    #region Methods

    #region Lifecycle

    private void Start()
    {
        StartCoroutine(Timer());

    }

    #endregion

    #region Private

    private IEnumerator Timer()
    {
        SetHandRoll(_duration); //Sets the hand at his original roll
        for (int i = _duration; i >= 0; i--)
        {
            SetHandRoll(i);
            yield return new WaitForSecondsRealtime(1f);
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


       if(_onTimerEnd != null) _onTimerEnd.Invoke();
    }


    #endregion
    #endregion
}
