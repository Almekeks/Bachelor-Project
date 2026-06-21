using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene_Transition : MonoBehaviour
{
    public float time;
    public string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;

        if (time < 0)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
