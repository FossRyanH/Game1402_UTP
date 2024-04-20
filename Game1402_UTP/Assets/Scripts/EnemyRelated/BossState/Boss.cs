using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    #region Components
    [field:SerializeField]
    public Health Player { get; private set; }
    #endregion

    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
