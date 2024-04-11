using UnityEngine;

public class Dungeon : MonoBehaviour
{
    protected bool isActive = false;

    void Start()
    {
        DeActivate();
    }

    protected virtual void Activate()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {

            transform.GetChild(i).gameObject.SetActive(true); //go through all the children of the dungeon and turn them on
        }
    }
    protected virtual void DeActivate()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).gameObject.SetActive(false); //go through all the children of the dungeon and turn them off
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        if (other.GetComponentInParent<PlayerController>()) //when the player enters a dungeon, it turns the dungeon on by activating its children
        {
            isActive = true;

            Activate();
        }

    }
    protected virtual void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit");
        if (other.GetComponentInParent<PlayerController>()) //when the player exits a dungeon, it turns the dungeon off by deactivating its children
        {
            isActive = true;

            DeActivate();
        }
    }
}
