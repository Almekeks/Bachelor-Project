using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Charater_Behavior : MonoBehaviour
{
    public bool move = false;
    public bool shock = false;
    private GameObject goal;
    public GameObject InteractionManager;
    public float speed;
    public string NextScene;
    [SerializeField] private Animator animations;
    [SerializeField] private GameObject Shock;
    [SerializeField] private AudioSource Steps;
    [SerializeField] private AudioSource Electicity;

    [Space]

    private bool gen1;
    private bool gen2;
    private bool containers;
    private bool button;
    private bool leave;

    void Update()
    {
        if (move == true && shock == false)
        {
            MoveToGoal();
            animations.SetBool("Walk", true);
            Steps.enabled = true;
            animations.SetBool("Computer", false);
        }
    }

    void MoveToGoal()
    {
        goal = GameObject.Find("Pointer(Clone)");

        transform.LookAt(goal.transform);

        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        

        if(other.gameObject.tag == "Interactable" || other.gameObject.tag == "Wall")
        {
            move = false;
            Destroy(GameObject.Find("Pointer(Clone)"));
            animations.SetBool("Walk", false);
            Steps.enabled = false;
            transform.GetChild(7).GetComponent<VoiceLines_Manager>().TimerRestart();
        }

        if (other.gameObject.tag == "Cable" && other.gameObject.GetComponent<Shock_Active>().shock == true)
        {
            move = false;
            Destroy(GameObject.Find("Pointer(Clone)"));
            animations.SetBool("Walk", false);
            Steps.enabled = false;

            shock = true;
            Shock.SetActive(true);
            other.gameObject.GetComponent<MeshCollider>().enabled = false;
            
            Electicity.enabled = true;
            StartCoroutine(DeactivateSock());
            TransferVariables.shockCount++;
            GetComponent<Interaction_Manager_2>().LiquidDecrice();
            animations.SetBool("Shock", true);
        }

        if (other.gameObject.name == "OpenGate")
        {
            GameObject.Find("DoorExit").GetComponent<Animator>().enabled = true;
            GameObject.Find("DoorExit").GetComponent<AudioSource>().enabled = true;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.name == "Leave2")
        {
            StartCoroutine(LeaveScene());
        }

        if (other.gameObject.name == "Leave" && leave == true)
        {
            GetComponent<Interaction_Manager_2>().MemorizeFlask();
            TransferVariables.nextLevel = true;
            StartCoroutine(LeaveScene());
        }
        else if (other.gameObject.name == "Button" && button == true)
        {
            InteractionManager.GetComponent<Interaction_manager_1>().door = true;
            InteractionManager.GetComponent<Interaction_manager_1>().Interact();
            move = false;
            leave = true;
            animations.SetBool("Button", true);
            StartCoroutine(DeactivateInteraction());
        }
        else if(other.gameObject.name == "ConsoleSingle" && containers == true)
        {
            InteractionManager.GetComponent<Interaction_manager_1>().steelPipes = true;
            InteractionManager.GetComponent<Interaction_manager_1>().Interact();
            move = false;
            button = true;
            animations.SetBool("Computer", true);
            StartCoroutine(DeactivateInteraction());
        }
        //turn bubbles on
        else if (other.gameObject.name == "Base" && gen2 == true)
        {
            InteractionManager.GetComponent<Interaction_manager_1>().couldrins = true;
            InteractionManager.GetComponent<Interaction_manager_1>().Interact();
            move = false;
            containers = true;
            animations.SetBool("Computer", true);
            StartCoroutine(DeactivateInteraction());
        }
        // turn big PC on
        else if (other.gameObject.name == "Cube" && gen1 == true)
        {
            InteractionManager.GetComponent<Interaction_manager_1>().generatorK2 = true;
            InteractionManager.GetComponent<Interaction_manager_1>().Interact();
            move = false;
            gen2 = true;
            animations.SetBool("Computer", true);
            StartCoroutine(DeactivateInteraction());

            Destroy(GameObject.Find("Pointer(Clone)"));
            return;
        }
        //turn generator on + lights
        else if (other.gameObject.name == "Generator" && gen1 == false)
        {
            InteractionManager.GetComponent<Interaction_manager_1>().generatorK1 = true;
            InteractionManager.GetComponent<Interaction_manager_1>().Interact();
            move = false;
            gen1 = true;
            animations.SetBool("Button", true);
            StartCoroutine(DeactivateInteraction());

            Destroy(GameObject.Find("Pointer(Clone)"));
            return;
        }

        //check if either cable is active and trigger Shock if YES
        if (other.gameObject.name == "NurbsPath.007" && InteractionManager.GetComponent<Interaction_manager_1>().generatorK1 == true)
        {
            move = false;
            Destroy(GameObject.Find("Pointer(Clone)"));
            animations.SetBool("Walk", false);
            Steps.enabled = false;

            shock = true;
            Shock.SetActive(true);
            other.gameObject.GetComponent<MeshCollider>().enabled = false;
            
            Electicity.enabled = true;
            StartCoroutine(DeactivateSock());
            TransferVariables.shockCount++;
            GetComponent<Interaction_Manager_2>().LiquidDecrice();
            animations.SetBool("Shock", true);
        }
        else if (other.gameObject.name == "NurbsPath.006" && InteractionManager.GetComponent<Interaction_manager_1>().generatorK2 == true)
        {
            move = false;
            Destroy(GameObject.Find("Pointer(Clone)"));
            animations.SetBool("Walk", false);
            Steps.enabled = false;

            shock = true;
            Shock.SetActive(true);
            other.gameObject.GetComponent<MeshCollider>().enabled = false;
            
            Electicity.enabled = true;
            StartCoroutine(DeactivateSock());
            TransferVariables.shockCount++;
            GetComponent<Interaction_Manager_2>().LiquidDecrice();
            animations.SetBool("Shock", true);

        }
    }

    public void GameOverStart()
    {
        StartCoroutine(GameOver());
    }

    IEnumerator DeactivateSock()
    {
        yield return new WaitForSeconds(6);
        animations.SetBool("Shock", false);
        shock = false;

        yield return new WaitForSeconds(6);
        Shock.SetActive(false);
        Electicity.enabled = false;
    }

    public IEnumerator DeactivateInteraction()
    {
        yield return new WaitForSeconds(1.5f);
        animations.SetBool("Button", false);
        yield return new WaitForSeconds(4.2f);
        animations.SetBool("Computer", false);
    }

    IEnumerator LeaveScene()
    {
        GameObject.Find("Main Camera").transform.GetChild(0).GetComponent<Animator>().SetBool("ExitScene", true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(NextScene);
    }

    IEnumerator GameOver()
    {
        animations.SetBool("Shock", false);
        animations.SetBool("GameOver", true);
        yield return new WaitForSeconds(3);
        GameObject.Find("Main Camera").transform.GetChild(0).GetComponent<Animator>().SetBool("ExitScene", true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Start_Menu");
    }
}
