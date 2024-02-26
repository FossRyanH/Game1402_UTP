using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class ViewController : MonoBehaviour
{

    [SerializeField]
    Transform targetTransform;

    Camera mainCamera;
    private RaycastHit[] pastHits;
    //RaycastHit[] hits;                                                            // Approach 1
    private RaycastHit[] hits = new RaycastHit[5];                                  // Approach 2

    void Awake()
    {
        pastHits = Array.Empty<RaycastHit>();
        mainCamera = Camera.main;
    }
    
    private void FixedUpdate()
    {
        StartCoroutine(CheckCameraViewBlockage());
        //CheckCameraViewBlockage();
    }

    IEnumerator CheckCameraViewBlockage()
    //private void CheckCameraViewBlockage()
    {
        Vector3 target = targetTransform.position;
        Ray ray = new(target, mainCamera.transform.position - target);
        Debug.DrawRay(target, mainCamera.transform.position - target, Color.red);

        //hits = Physics.RaycastAll(ray);                                           // Approach 1
        int amountOfHits = Physics.RaycastNonAlloc(ray, hits);                      // Approach 2

        //for (int i = 0; i < hits.Length; i++)                                     // Approach 1
        for (int i = 0; i < amountOfHits; i++)                                      // Approach 2
        {
            RaycastHit hit = hits[i];
            Renderer rend = hit.transform.GetComponent<Renderer>();

            if (rend)
            {
                // Change the material color
                Color tempColor = rend.material.color;
                tempColor.a = 0.3F;
                rend.material.color = tempColor;
            }
        }

        for (int i = 0; i < pastHits.Length; i++)
        {
            RaycastHit pastHit = pastHits[i];
            Renderer rend = pastHit.transform.GetComponent<Renderer>();

            if (rend && !hits.Contains(pastHit))
            {
                // Change the material color
                Color tempColor = rend.material.color;
                tempColor.a = 1F;
                rend.material.color = tempColor;
            }
        }

        //pastHits = hits;                                                          // Approach 1
        pastHits = Array.FindAll(hits, hit => hit.transform != null);               // Approach 2


        //yield return null;
        yield return new WaitForEndOfFrame();
    }

}
