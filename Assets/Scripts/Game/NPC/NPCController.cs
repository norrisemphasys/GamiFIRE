using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    [SerializeField] private NPCPath npcPath;
    [SerializeField] GameObject[] npcCharacter;
    [SerializeField] int characterIndex = -1;
    [SerializeField] float walkSpeed = 0.5f;

    private NavMeshAgent navMeshAgent;
    private Animator animator;

    public enum NPCAnimState
    {
        WALK,
        IDLE
    }

    [SerializeField] private NPCAnimState animState;

    private Transform npcTransform;
    private Transform currentDestination;

    private float idleTimer = 0;
    private float timer = 0;

    private void Awake()
    {
        npcTransform = transform;

        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        ShowCharacter(characterIndex);
    }

    // Start is called before the first frame update
    void Start()
    {
        RandomizeAnimState();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(npcTransform.position, currentDestination.position);

        if(distance < 0.4f)
            RandomizeAnimState();

        if(animState == NPCAnimState.WALK)
        {
            navMeshAgent.SetDestination(currentDestination.position);
        }
        else if(animState  == NPCAnimState.IDLE)
        {
            if (timer + idleTimer < Time.time)
            {
                RandomizeAnimState();
            }
        }
    }

    void RandomizeAnimState()
    {
        animState = (NPCAnimState)Random.Range(0, 2);

        if (animState == NPCAnimState.IDLE)
        {
            idleTimer = Random.Range(5, 15);
            timer = Time.time;

            animator.SetFloat("Speed", 0);
            navMeshAgent.speed = 0f;
        }
        else if(animState == NPCAnimState.WALK)
        {
            timer = Time.time;
            animator.SetFloat("Speed", 0.15f);
            navMeshAgent.speed = walkSpeed;
        }

        currentDestination = npcPath.paths[Random.Range(0, npcPath.paths.Length)];
    }

    void ShowCharacter(int idx)
    {
        for(int i = 0; i < npcCharacter.Length; i++)
            npcCharacter[i].SetActive(i == idx);
    }
}
