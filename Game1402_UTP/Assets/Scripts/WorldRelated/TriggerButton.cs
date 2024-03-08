using UnityEngine;
using UnityEngine.Events;

public class TriggerButton : MonoBehaviour
{
    public UnityEvent button;
    DoorInteractable doorScript;

    private void Start()
    {
        doorScript = GameObject.FindGameObjectWithTag("LockedDoor").GetComponent<DoorInteractable>();
        button.AddListener(doorScript.Interact);
    }

    void OnTriggerEnter(Collider other)
    {
        button.Invoke();
    }

    void OnTriggerExit(Collider other)
    {
        button.Invoke();
    }
}
