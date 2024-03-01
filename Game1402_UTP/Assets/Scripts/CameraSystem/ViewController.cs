using UnityEngine;

public class ViewController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Renderer rend = other.gameObject.GetComponent<Renderer>();
        Color tempColor = rend.material.color;
        tempColor.a = 0.3F;
        rend.material.color = tempColor;
    }

    private void OnTriggerExit(Collider other)
    {
        Renderer rend = other.gameObject.GetComponent<Renderer>();
        Color tempColor = rend.material.color;
        tempColor.a = 1F;
        rend.material.color = tempColor;
    }
}
