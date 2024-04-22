using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    #region Components
    [field: SerializeField]
    public Health Player { get; private set; }
    public BossStateMachine StateMachine { get; private set; }
    [field: SerializeField]
    public Health BossHealth { get; private set; }
    [field: SerializeField]
    public NavMeshAgent Agent;
    [field: SerializeField]
    public CharacterController Controller;
    [field: SerializeField]
    public ForceReciever ForceReciever { get; private set; }
    public Animator Animator { get; private set; }
    #endregion
    
    #region Combat Variables
    [field: SerializeField]
    public float PlayerAttackRange = 4f;
    [field: SerializeField]
    public bool IsInPhaseOne = false;
    [field: SerializeField]
    public bool IsInPhaseTwo = false;
    #endregion
    
    #region Movement Variables
    public float MoveSpeed;
    [field: SerializeField]
    public float WanderSpeed { get; private set; } = 3f;
    [field: SerializeField]
    public float ChargeSpeed { get; private set; } = 9f;
    [field: SerializeField]
    public GameObject Target;
    [field: SerializeField]
    public GameObject[] WanderPoints;
    [field: SerializeField]
    public float WanderRadius { get; private set; } = 2f;
    #endregion

    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

        if (StateMachine == null)
        {
            StateMachine = new BossStateMachine();
        }

        BossHealth = GetComponent<Health>();
        Agent = GetComponent<NavMeshAgent>();
        ForceReciever = GetComponent<ForceReciever>();
        Controller = GetComponent<CharacterController>();
        Animator = GetComponent<Animator>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        StateMachine.InitializeState(new BossWanderState(this));
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine.Update();
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, PlayerAttackRange);
    }
}
