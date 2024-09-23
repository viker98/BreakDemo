using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sense : MonoBehaviour
{
    [SerializeField] private bool bDrawDebug = true;
    [SerializeField] private float forgetTime = 3f;

    static HashSet<Stimuli> _registeredStimuliSet = new HashSet<Stimuli>();

    private HashSet<Stimuli> _currentSensiableStimuliSet = new HashSet<Stimuli>();

    private Dictionary<Stimuli, Coroutine> _forgettingCoroutines = new Dictionary<Stimuli, Coroutine>();

    public static void RegisterStimuli(Stimuli stimuli)
    {
        _registeredStimuliSet.Add(stimuli);
    }

    public static void UnRegisterStimuli(Stimuli stimuli)
    {
        _registeredStimuliSet.Remove(stimuli);
    }

    protected abstract bool IsStimuliSensable(Stimuli stimuli);

    private void Update()
    {
        foreach(Stimuli stimuli in _registeredStimuliSet)
        {
            if(IsStimuliSensable(stimuli))
            {
                HandleSensibleStimuli(stimuli);
            }
            else
            {
                HandleNoneSensibleStimuli(stimuli);
            }
        }
    }

    private void HandleNoneSensibleStimuli(Stimuli stimuli)
    {
        // cant sense if null but didnt sense before, nothing need to be done
        if (!_currentSensiableStimuliSet.Contains(stimuli))
        {
            return;
        }

        _currentSensiableStimuliSet.Remove(stimuli);

        Coroutine forgettingCoroutine = StartCoroutine(ForgetStimuli(stimuli));
        _forgettingCoroutines.Add(stimuli, forgettingCoroutine);
    }

    private IEnumerator ForgetStimuli(Stimuli stimuli)
    {
        yield return new WaitForSeconds(forgetTime);
        _forgettingCoroutines.Remove(stimuli);
        Debug.Log($"i Just lost track of: {stimuli.gameObject.name}");
    }

    private void HandleSensibleStimuli(Stimuli stimuli)
    {
        // we can sense it now and before, nothing needs to be done
        if (_currentSensiableStimuliSet.Contains(stimuli))
        {
            return;
        }

        _currentSensiableStimuliSet.Add(stimuli);
        
        if (_forgettingCoroutines.ContainsKey(stimuli))
        {
            StopCoroutine(_forgettingCoroutines[stimuli]);
            _forgettingCoroutines.Remove(stimuli);
            return;
        }

        Debug.Log($"i Just sensed: {stimuli.gameObject.name}");
    }

    private void OnDrawGizmos()
    {
        if (bDrawDebug)
        {
            OnDrawDebug();
        }
    }

    protected virtual void OnDrawDebug()
    {
        //override in child class to draw debug
    }
}
