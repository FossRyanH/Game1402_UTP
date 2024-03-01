using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{

    [SerializeField]
    Transform targetTransform;

    Camera mainCamera;
    private RaycastHit[] hits = new RaycastHit[5]; //size of elements raycast can hit and store on buffer

    List<Transform> currentObjectsHitted;
    List<Transform> pastObjectsHitted;

    void Awake()
    {
        pastObjectsHitted = new List<Transform>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        CheckCameraViewBlockage();
    }

    private void CheckCameraViewBlockage()
    {
        currentObjectsHitted = new List<Transform>();

        Vector3 target = targetTransform.position;
        Ray ray = new(target, mainCamera.transform.position - target);
        Debug.DrawRay(target, mainCamera.transform.position - target, Color.red);

        int amountOfHits = Physics.RaycastNonAlloc(ray, hits);

        for (int i = 0; i < amountOfHits; i++)
        {
            RaycastHit hit = hits[i];
            Renderer rend = hit.transform.GetComponent<Renderer>();

            if (rend)
            {
                Color tempColor = rend.material.color;
                tempColor.a = 0.3f;
                rend.material.color = tempColor;
                currentObjectsHitted.Add(hit.transform);
            }
        }
 
        for (int i = 0; i < pastObjectsHitted.Count; i++)
        {
            Transform pastHit = pastObjectsHitted[i];
            Renderer rend = pastHit.GetComponent<Renderer>();

            if (rend && !currentObjectsHitted.Contains(pastHit))
            {
                Color tempColor = rend.material.color;
                tempColor.a = 1f;
                rend.material.color = tempColor;
            }
        }

        pastObjectsHitted = currentObjectsHitted;
    }

}
