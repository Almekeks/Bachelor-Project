using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cable_Controll : MonoBehaviour
{
    public GameObject[] cables;
    public float timer;
    public float passedTime;
    int Bigchance = 0;
    private int chance;

    // Start is called before the first frame update
    void Start()
    {
        //for (int i = 1; i < 8; i++)
        //{
        //    cables[i - 1] = GameObject.Find("Cable_" + i);
        //}
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        passedTime -= Time.deltaTime;

        if (Mathf.RoundToInt(passedTime) % 2 == 0)
        {
            Bigchance = Random.Range(1, 20);
        }
        
        if (passedTime <= 0)
        {
            ActivateCables();
            passedTime = timer;
        }
    }

    [System.Obsolete]
    private void ActivateCables()
    {
        if (Bigchance > 1)
        {
            int chance = 0;

            if (passedTime <= 0)
            {
                chance = Random.Range(1, 10);
            }


            if (chance == 1)
            {
                for (int i = 0; i < 7; i++)
                {
                    cables[i].GetComponent<ParticleSystem>().enableEmission = true;
                    StartCoroutine(StopCables());
                }
            }
            else if (chance != 1)
            {
                int cab1 = Random.Range(0, 6);
                int cab2 = Random.Range(0, 6);
                int cab3 = Random.Range(0, 6);

                cables[cab1].GetComponent<ParticleSystem>().enableEmission = true;
                cables[cab1].GetComponent<Shock_Active>().shock = true;
                cables[cab1].transform.GetChild(0).gameObject.SetActive(true);

                cables[cab2].GetComponent<ParticleSystem>().enableEmission = true;
                cables[cab2].GetComponent<Shock_Active>().shock = true;
                cables[cab2].transform.GetChild(0).gameObject.SetActive(true);

                cables[cab3].GetComponent<ParticleSystem>().enableEmission = true;
                cables[cab3].GetComponent<Shock_Active>().shock = true;
                cables[cab3].transform.GetChild(0).gameObject.SetActive(true);
                StartCoroutine(StopCables());
            }
        }
        else if (Bigchance < 1 && passedTime == 5 || passedTime <= 0)
        {
            for (int i = 0; i < 7; i++)
            {
                cables[i].GetComponent<ParticleSystem>().enableEmission = true;
                StartCoroutine(StopCablesNow());
            }
            print("Surprize!!");
        }
    }

    [System.Obsolete]
    IEnumerator StopCables()
    {
        yield return new WaitForSeconds(5);
        for (int i = 0; i < 7; i++)
        {
            cables[i].GetComponent<ParticleSystem>().enableEmission = false;
            cables[i].GetComponent<Shock_Active>().shock = false;
            cables[i].transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    [System.Obsolete]
    IEnumerator StopCablesNow()
    {
        yield return new WaitForSeconds(3);
        for (int i = 0; i < 7; i++)
        {
            cables[i].GetComponent<ParticleSystem>().enableEmission = false;
        }
    }

}
