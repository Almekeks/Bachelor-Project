using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interaction_manager_1 : MonoBehaviour
{
    public bool generatorK1;
    public bool generatorK2;
    public bool steelPipes;
    public bool couldrins;
    public bool redButton;
    public bool door;
    public bool leaveScene;

    [Space]

    [SerializeField] private Transform Generator;
    [SerializeField] private GameObject GeneratorKable1;
    [SerializeField] private Transform Computer;
    [SerializeField] private GameObject GeneratorKable2;
    [SerializeField] private Transform Pipes;
    [SerializeField] private Transform BubbleEffect;
    [SerializeField] private Transform RedButton;
    [SerializeField] private Transform Doors;
    [SerializeField] private Transform Leave;
    [SerializeField] private GameObject Chemical;

    [Space]

    [SerializeField] private float timer;
    [SerializeField] private float wait;
    private float i;
    private float j;

    [Space]

    public GameObject[] secondLights;
    public GameObject[] doorLights;
    public GameObject[] cameraLights;
    public GameObject[] screenCovers;

    void Start()
    {
        //saves all abjects that need simple activation for later
        secondLights = new GameObject[3];
        for (int i = 1; i <= 3; i++)
        {
            secondLights[i - 1] = GameObject.Find("Light_Second " + i);
        }

        doorLights = new GameObject[2];
        for (int i = 1; i <= 2; i++)
        {
            doorLights[i - 1] = GameObject.Find("DoorLights" + i);
        }

        cameraLights = new GameObject[2];
        for (int i = 1; i <= 2; i++)
        {
            cameraLights[i - 1] = GameObject.Find("CameraLamp" + i);
        }

        screenCovers = new GameObject[7];
        for (int i = 0; i <= 6; i++)
        {
            screenCovers[i] = GameObject.Find("ScreenCover" + i);
        }

        //deactivates saved objects exept covers
        for (int i = 0; i < 3; i++)
        {
            secondLights[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < 2; i++)
        {
            doorLights[i].gameObject.SetActive(false);
            cameraLights[i].gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            StartCoroutine(LightsOn());
        }

    }

    public void Interact()
    {
        if (leaveScene == true)
        {
            Invoke("LoadNextScene", 5f);
        }
        Invoke("ActivateInteraction", timer * 0.2f);
    }

    [System.Obsolete]
    private void ActivateInteraction()
    {

        // enables first mini computer
        if (generatorK1 == true)
        {
            Generator.GetComponent<Sound_Effects>().ActivateSound();
            GeneratorKable1.GetComponent<Sound_Effects>().ActivateSound();
            GeneratorKable1.GetComponent<ParticleSystem>().enableEmission = true;
            screenCovers[1].SetActive(false);
            StartCoroutine(Deactivate());
        }

        // enables main electricity
        if (generatorK2 == true)
        {
            GeneratorKable2.GetComponent<Sound_Effects>().ActivateSound();
            GeneratorKable2.GetComponent<ParticleSystem>().enableEmission = true;
            StartCoroutine(LightsOn());
            StartCoroutine(Deactivate());
        }

        // enables the couldrins
        if (couldrins == true)
        {
            BubbleEffect.GetComponent<Sound_Effects>().ActivateSound();

            BubbleEffect.GetChild(0).gameObject.SetActive(true);
            BubbleEffect.GetChild(1).gameObject.SetActive(true);
            BubbleEffect.GetChild(2).gameObject.SetActive(true);
            BubbleEffect.GetChild(3).gameObject.SetActive(true);
            BubbleEffect.GetChild(4).gameObject.SetActive(true);
            j = 2f;
            while (j >= 0)
            {
                j -= Time.deltaTime;
            }
            BubbleEffect.transform.GetChild(5).gameObject.SetActive(true);
            j = 0;
            
        }

        // checks if the couldrins are active and enables transmistion to big container
        if (couldrins == true && steelPipes == true)
        {
            Pipes.GetComponent<Sound_Effects>().ActivateSound();
            StartCoroutine(ActivateSteam());
        }

        //activate doors
        if (door == true)
        {
            RedButton.GetComponent<Animator>().SetBool("Press", true);
            RedButton.GetComponent<Animator>().SetBool("Press", true);
            RedButton.GetComponent<Sound_Effects>().ActivateSound();
            Doors.GetComponent<Animator>().SetBool("Open", true);
            Leave.gameObject.SetActive(true);
        }
    }

    //deactivates generator
    [System.Obsolete]
    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(timer);

        generatorK1 = false;
        generatorK2 = false;
        steelPipes = false;

        GeneratorKable1.GetComponent<ParticleSystem>().enableEmission = false;
        GeneratorKable2.GetComponent<ParticleSystem>().enableEmission = false;

        GeneratorKable1.transform.GetChild(0).GetComponent<AudioSource>().enabled = false;
        GeneratorKable2.transform.GetChild(0).GetComponent<AudioSource>().enabled = false;
        print("Stop!");
    }

    // deactivates couldrins
    IEnumerator Deactivate2()
    {
        BubbleEffect.GetComponent<Sound_Effects>().EndSoundForcfull();
        Pipes.GetComponent<Sound_Effects>().EndSoundForcfull();
        yield return new WaitForSeconds(5.2f);

        int k = 0;
        while (k <= 5)
        {
            BubbleEffect.GetChild(k).gameObject.SetActive(false);           
            k++;
        }

        k = 0;
        while (k <= 7)
        {
            Pipes.GetChild(k).gameObject.SetActive(false);
            k++;
        }

        couldrins = false;
        steelPipes = false;

        RedButton.GetChild(1).gameObject.SetActive(true);
        GameObject.Find("Button_Cover").GetComponent<Animator>().enabled = true;
        Chemical.SetActive(true);
        GameObject.Find("BA_Character").GetComponent<Interaction_Manager_2>().FillFlask();
    }

    IEnumerator ActivateSteam()
    {
        int g = 0;
        while (g <= 7)
        {
            yield return new WaitForSeconds(wait);
            Pipes.GetChild(g).gameObject.SetActive(true);
            g++;
        }

        StartCoroutine(Deactivate2());
    }

    IEnumerator LightsOn()
    {
        yield return new WaitForSeconds(2);

        int l = 0;
        while (l < 7)
        {
            yield return new WaitForSeconds(0.5f);
            screenCovers[l].SetActive(false);
            l++;
        }

        l = 0;
        while (l < 2)
        {
            yield return new WaitForSeconds(0.5f);
            doorLights[l].SetActive(true);
            cameraLights[l].SetActive(true);
            l++;
        }
        l = 0;

        while (l < 3)
        {
            yield return new WaitForSeconds(0.5f);
            secondLights[l].SetActive(true);
            l++;
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene("2nd_Scene");
    }
}
