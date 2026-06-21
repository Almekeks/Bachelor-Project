using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLines_Manager : MonoBehaviour
{
    [SerializeField] private GameObject[] voicesDirection;
    [SerializeField] private GameObject[] voicesDirectionNice;
    [SerializeField] private GameObject[] voicesNextStep;
    [SerializeField] private GameObject[] voicesNextStepNextScene;
    [SerializeField] private GameObject[] voicesPowerUp;
    [SerializeField] private GameObject voicesStopComplaining;
    private Transform VoiceLines;
    [SerializeField] private Transform Camera;

    public float timer;
    [SerializeField] private float time;

    private bool noPower;
    public bool complain = true;
    public bool stopedComplaining;

    // Start is called before the first frame update
    void Start()
    {
        VoiceLines = transform;

        voicesDirection = new GameObject[VoiceLines.GetChild(0).childCount];
        for (int i = 0; i < VoiceLines.GetChild(0).childCount; i++)
        {
            voicesDirection[i] = VoiceLines.GetChild(0).GetChild(i).gameObject;
            voicesDirection[i].SetActive(false);
        }

        voicesDirectionNice = new GameObject[VoiceLines.GetChild(1).childCount];
        for (int i = 0; i < VoiceLines.GetChild(1).childCount; i++)
        {
            voicesDirectionNice[i] = VoiceLines.GetChild(1).GetChild(i).gameObject;
            voicesDirectionNice[i].SetActive(false);
        }

        voicesNextStep = new GameObject[VoiceLines.GetChild(2).childCount];
        for (int i = 0; i < VoiceLines.GetChild(2).childCount; i++)
        {
            voicesNextStep[i] = VoiceLines.GetChild(2).GetChild(i).gameObject;
            voicesNextStep[i].SetActive(false);
        }

        voicesPowerUp = new GameObject[VoiceLines.GetChild(3).childCount];
        for (int i = 0; i < VoiceLines.GetChild(3).childCount; i++)
        {
            voicesPowerUp[i] = VoiceLines.GetChild(3).GetChild(i).gameObject;
            voicesPowerUp[i].SetActive(false);
        }

        voicesNextStepNextScene = new GameObject[VoiceLines.GetChild(5).childCount];
        for (int i = 0; i < VoiceLines.GetChild(5).childCount; i++)
        {
            voicesNextStepNextScene[i] = VoiceLines.GetChild(2).GetChild(i).gameObject;
            voicesNextStepNextScene[i].SetActive(false);
        }

        voicesStopComplaining = VoiceLines.GetChild(4).gameObject;

        time = timer;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera);

        time -= Time.deltaTime;

        if (time <= 0)
        {
            if (complain == false && stopedComplaining == false)
            {
                voicesStopComplaining.SetActive(true);
                stopedComplaining = true;
                return;
            }
            else if (Random.Range(0, 30) % 2 == 0)
            {
                if (TransferVariables.nextLevel == false)
                {
                    voicesNextStep[Random.Range(0, voicesNextStep.Length)].SetActive(true);
                }
                else
                {
                    voicesNextStepNextScene[Random.Range(0, voicesNextStepNextScene.Length)].SetActive(true);
                }
                
            }
            StartCoroutine(DeactivateVoiceLine());

            time = timer;
        }

        if (TransferVariables.shockCount > 10)
        {
            complain = false;
        }
    }

    IEnumerator DeactivateVoiceLine()
    {
        yield return new WaitForSeconds(10);
        for (int i = 0; i < voicesDirection.Length; i++)
        {
            voicesDirection[i].SetActive(false);
        }

        for (int i = 0; i < voicesDirectionNice.Length; i++)
        {
            voicesDirectionNice[i].SetActive(false);
        }

        for (int i = 0; i < voicesNextStep.Length; i++)
        {
            voicesNextStep[i].SetActive(false);
        }

        for (int i = 0; i < voicesPowerUp.Length; i++)
        {
            voicesPowerUp[i].SetActive(false);
        }
    }

    public void TimerRestart()
    {
        time = timer;
    }

    public void DirectionVoiceLine()
    {
        if (Random.Range(0, 30) % 3 == 0)
        {
            if (stopedComplaining == true)
            {
                voicesDirectionNice[Random.Range(0, voicesDirectionNice.Length)].SetActive(true);
            }
            else
            {
                voicesDirection[Random.Range(0, voicesDirection.Length)].SetActive(true);
            }
            StartCoroutine(DeactivateVoiceLine());
        }
    }
}
