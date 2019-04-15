using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClockTimer : MonoBehaviour
{
    #region Attributes

    [SerializeField] private Transform _hand = null;
    [SerializeField] private int _duration = 60;
    [SerializeField] private int _timeRemainAlert = 10;
    [SerializeField] private float _tickDuration = 1;

    [SerializeField] private UnityEvent _onTimerEnd = null;
    private DissolveControler[] _dissolveMaterials;


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

    private void Awake()
    {
        _dissolveMaterials = FindObjectsOfType<DissolveControler>();
    }

    private void Start()
    {
        StartCoroutine("Timer");

    }

    #endregion

    #region Private

    private IEnumerator Timer()
    {
        SetHandRoll(_duration); //Sets the hand at his original roll
        for (int i = _duration; i > 0; i--)
        {
            SetHandRoll(i);
           
            if (i>10)
            {
                AkSoundEngine.PostEvent("Gacha_Objects_Pendulum_Timer_Normal_LP", gameObject);
            }
            else if (i<=_timeRemainAlert && i > 0) 
            {
                AkSoundEngine.PostEvent("Gacha_Amb_Object_Pendulum_Last_10_seconds_OS", gameObject);
            }

            if(_dissolveMaterials.Length > 0)
            {
                SetDissolveAmount(i);
            }

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

    
    private void SetDissolveAmount(int timeRemain)
    {
        float t = (float)timeRemain / (float)_duration;
        float amount = Mathf.Lerp(1, 0, t);

        for (int i = 0; i < _dissolveMaterials.Length; i++)
        {
            if (_dissolveMaterials[i].general)
            {
                _dissolveMaterials[i].SetDissolveAmount(amount);
            }
        }
    }



    private void EndTimer()
    {

        for (int i = 0; i < _dissolveMaterials.Length; i++)
        {
            if (_dissolveMaterials[i].general)
            {
                _dissolveMaterials[i].SetDissolveAmount(1);
            }
        }

        AkSoundEngine.PostEvent("Gacha_Amb_Objects_Pendulum_Death_OS", gameObject);
    
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
