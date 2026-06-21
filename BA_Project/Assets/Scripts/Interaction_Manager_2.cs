using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction_Manager_2 : MonoBehaviour
{
    [SerializeField] private RectTransform Mask;
    [SerializeField] private RectTransform liquidImage;
    [SerializeField] private RectTransform Canvas;
    [SerializeField] private Transform Camera;
    [SerializeField] private Transform Toilet;
    [SerializeField] private Transform Floor;
    [SerializeField] private GameObject Bubbles;
    private bool Flask;

    [Space]

    [SerializeField] private Animator animator;
    [SerializeField] private Material CermicClean;

    private void Start()
    {
        Mask.anchoredPosition = new Vector2(0, -TransferVariables.liquidQuantity);
        liquidImage.anchoredPosition = new Vector2(0, TransferVariables.liquidQuantity);
    }

    // Update is called once per frame
    void Update()
    {
        Canvas.LookAt(Camera);
        if (Input.GetKeyDown("a"))
        {
            Bubbles.SetActive(true);
            StartCoroutine(ChangeColor());
        }
    }

    public void LiquidDecrice()
    {
        float move = Random.Range(0.5f, 1.2f);

        Mask.anchoredPosition -= new Vector2(0, move);
        liquidImage.anchoredPosition += new Vector2(0, move);
        print(Mask.anchoredPosition3D.y);

        if (Mask.anchoredPosition3D.y < -1.9f /*&& Flask == true*/)
        {
            GetComponent<Charater_Behavior>().GameOverStart();
        }
    }

    public void FillFlask()
    {
        Mask.anchoredPosition = new Vector2(0, 0);
        liquidImage.anchoredPosition = new Vector2(0, 0);
        Flask = true;
    }

    public void MemorizeFlask()
    {
        TransferVariables.liquidQuantity = liquidImage.anchoredPosition.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Toilet")
        {
            Bubbles.SetActive(true);
            animator.SetBool("pourLiquid", true);
            StartCoroutine(ChangeColor());
        }
    }

    IEnumerator ChangeColor()
    {
        yield return new WaitForSeconds(7);
        animator.SetBool("pourLiquid", false);

        yield return new WaitForSeconds(8);
        for (int i = 0; i < Floor.childCount; i++)
        {
            Floor.GetChild(i).GetComponent<MeshRenderer>().material = CermicClean;
        }

        for (int i = 1; i < Toilet.childCount; i++)
        {
            Toilet.GetChild(i).GetComponent<MeshRenderer>().material = CermicClean;
        }

        yield return new WaitForSeconds(10);
        ExitScene();
    }

    public void ExitScene()
    {
        Camera.GetChild(0).GetComponent<Animator>().SetBool("ExitScene", true);
        GameObject.Find("XR Origin").GetComponent<Cutscene_Transition>().enabled = true;
    }
}
