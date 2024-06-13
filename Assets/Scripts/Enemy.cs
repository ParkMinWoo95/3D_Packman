using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : RecycleObject
{
    private Animator Anim;

    public Transform target;

    NavMeshAgent nmAgent;
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    private static readonly int IdleState = Animator.StringToHash("Base Layer.idle");
    private static readonly int MoveState = Animator.StringToHash("Base Layer.move");

    [SerializeField] private SkinnedMeshRenderer[] MeshR;
    [SerializeField] private float Speed = 4;
    void Start()
    {
        Anim = this.GetComponent<Animator>();

        nmAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        nmAgent.SetDestination(target.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Boom"))
        {
            this.gameObject.SetActive(false);
        }
    }
}