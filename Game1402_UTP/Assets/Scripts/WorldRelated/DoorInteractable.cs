using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorInteractable : Interactable
{
    [SerializeField]
     TMP_Text _doorText;

     bool _isOpen = false;
     BoxCollider _doorCollider;

     void Awake()
     {
        _doorText = GetComponentInChildren<TMP_Text>();
        _doorText.enabled = false;
        _doorText.text = "Open Door";

        _doorCollider = GetComponent<BoxCollider>();
     }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            _doorText.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            _doorText.enabled = false;
        }
    }

    public override void Interact()
    {
        _isOpen = !_isOpen;
        StartCoroutine(AnimateDoor());

        if (_isOpen)
        {
            AudioManager.Instance.PlaySFX("DoorOpen");
            _doorCollider.enabled = false;
            _doorText.text = "Close Door";
        }
        else
        {
            AudioManager.Instance.PlaySFX("DoorClose");
            _doorCollider.enabled = true;
            _doorText.text = "Open Door";
        }

    }

    IEnumerator AnimateDoor()
    {
        float startTime = Time.time;
        float duration = 1f;

        Vector3 startingPos = transform.position;
        Vector3 targetPos;

        if (_isOpen)
        {
            targetPos = startingPos + Vector3.up * 2f;
        }
        else
        {
            targetPos = startingPos + Vector3.down * 2f;
        }

        while (Time.time - startTime < duration)
        {
            float step = (Time.time - startTime) / duration;
            transform.position = Vector3.Lerp(transform.position, targetPos, step);

            yield return null;
        }

        transform.position = targetPos;
    }
}
