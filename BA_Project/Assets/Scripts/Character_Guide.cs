using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction;

public class Character_Guide : MonoBehaviour
{
    [SerializeField] private Transform point;
    [SerializeField] private Transform pointVisual;

    [SerializeField] private GameObject Character;
    [SerializeField] private Animator Hand;



    public void AddPointGuide()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, 3))
        {
            Destroy(GameObject.Find("Pointer(Clone)"));
            print(hit.transform.gameObject.name);

            // check if it's an interactable and spawn the pointer
            if (hit.transform.tag == "Interactable" && hit.transform.tag != "Floor")
            {
                Vector3 pos = hit.transform.GetChild(0).transform.position;
                Instantiate(point, new Vector3(pos.x, 0, pos.z), Quaternion.identity);
                GameObject.Find("Pointer(Clone)").transform.GetChild(0).transform.position = pos;
                Character.GetComponent<Charater_Behavior>().move = true;

                return;
            }
            
            // check if it's the floor
            if (hit.transform.tag == "Floor")
            {
                Instantiate(point, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);

                Character.GetComponent<Charater_Behavior>().move = true;
            }
        }

        
    }

    public void HandAnimation()
    {
        StartCoroutine(AniamtionTransition());
    }

    IEnumerator AniamtionTransition()
    {
        Hand.SetBool("Point", true);
        yield return new WaitForSeconds(0.2f);
        Hand.SetBool("Point", false);
        Hand.enabled = false;
    }
}
