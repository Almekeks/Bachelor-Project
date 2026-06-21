using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Menu : MonoBehaviour
{
    [SerializeField] private GameObject RightHandLayout;
    [SerializeField] private GameObject LeftHandLayout;

    [Space]

    [SerializeField] private GameObject Camera;
    [SerializeField] private RectTransform Canvas;
    [SerializeField] private Transform CanvasPosition;
    [SerializeField] private string SceneName;
    [SerializeField] private string SceneNamecheat;
    [SerializeField] private float MenuDistance;

    private void Start()
    {
        if (TransferVariables.rightHanded == true)
        {
            RightHandLayout.SetActive(true);
            LeftHandLayout.SetActive(false);
        }
        else if (TransferVariables.leftHanded == true)
        {
            LeftHandLayout.SetActive(true);
            RightHandLayout.SetActive(false);
        }
    }

    public void SwitchToLeftHand()
    {
        LeftHandLayout.SetActive(true);
        RightHandLayout.SetActive(false);
    }
    public void SwitchToRightHand()
    {
        RightHandLayout.SetActive(true);
        LeftHandLayout.SetActive(false);
    }

    public void PlaySceneRightHand()
    {
        SceneManager.LoadScene("Beginning");
        TransferVariables.rightHanded = true;
        TransferVariables.leftHanded = false;
    }

    public void PlaySceneLeftHand()
    {
        SceneManager.LoadScene("Beginning");
        TransferVariables.rightHanded = false;
        TransferVariables.leftHanded = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void CheatScene()
    {
        SceneManager.LoadScene(SceneNamecheat);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Start_Menu");
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(SceneName);
    }

    public void OpenMenu()
    {
        CanvasPosition.position = Camera.transform.position;
        CanvasPosition.Translate(new Vector3(0, 0, MenuDistance), Camera.transform);
        //print("Open Menu");
        Canvas.gameObject.SetActive(true);
        Vector3 newPosition = CanvasPosition.transform.position;
        Canvas.anchoredPosition3D = newPosition;
        Canvas.LookAt(Camera.transform);
        //Canvas.anchoredPosition3D += new Vector3(0, 0, -7);
        //Canvas.LookAt(Camera.transform);

    }

    public void CloseMenu()
    {
        Canvas.gameObject.SetActive(false);
    }

}
