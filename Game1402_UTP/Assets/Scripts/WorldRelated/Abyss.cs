using UnityEngine;

public class Abyss : MonoBehaviour
{
    GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        // Restart game when falling into the abyss
        gameManager.Restart();
    }

}