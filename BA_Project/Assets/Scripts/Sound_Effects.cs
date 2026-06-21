using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Effects : MonoBehaviour
{
    [SerializeField] private AudioSource SFX_Start;
    [SerializeField] private AudioSource SFX_Run;
    [SerializeField] private AudioSource SFX_Stop;

    public float timer_0;
    public float timer_1;
    public float timer_2;
    public float timer_3;

    public bool wait;
    public string message;

    public void ActivateSound()
    {
        StartCoroutine(StartSound());
    }

    public void EndSoundForcfull()
    {
        StopCoroutine(RunSound());
        StartCoroutine(EndSound());
    }

    IEnumerator StartSound()
    {
        yield return new WaitForSeconds(timer_0);
        SFX_Start.enabled = true;
        print(message);
        yield return new WaitForSeconds(timer_1);

        if (SFX_Run == null)
        {
            StartCoroutine(EndSound());
            yield break;
        }
        else
        {
            StartCoroutine(RunSound());
        }
    }

    IEnumerator RunSound()
    {
        SFX_Start.enabled = false;
        SFX_Run.enabled = true;

        if (wait == false)
        {
            yield return new WaitForSeconds(timer_2);
            StartCoroutine(EndSound());
        }

    }

    IEnumerator EndSound()
    {
        if (SFX_Stop != null)
        {
            SFX_Stop.enabled = true;
            yield return new WaitForSeconds(timer_3);
        }
        SFX_Start.enabled = false;
        SFX_Run.enabled = false;
        SFX_Stop.enabled = false;
        
    }
}
