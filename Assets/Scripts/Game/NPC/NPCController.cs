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
    [SerializeField] NPCDialogueController dialogueController;

    private NavMeshAgent navMeshAgent;
    private Animator animator;

    public enum NPCAnimState
    {
        WALK,
        IDLE,
        TALK
    }

    [SerializeField] private NPCAnimState animState;

    private Transform npcTransform;
    private Transform currentDestination;

    private float idleTimer = 0;
    private float timer = 0;

    private PlayerController playerController;
    private bool isAvailable = false;

    private void Awake()
    {
        npcTransform = transform;

        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        dialogueController = FindObjectOfType<NPCDialogueController>();

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
        if(animState != NPCAnimState.TALK)
        {
            float distance = Vector3.Distance(npcTransform.position, currentDestination.position);

            if (distance < 0.4f)
                RandomizeAnimState();
        }

        if(animState == NPCAnimState.WALK)
        {
            navMeshAgent.SetDestination(currentDestination.position);
        }
        else if(animState  == NPCAnimState.IDLE)
        {
            if (timer + idleTimer < Time.time)
                RandomizeAnimState();
        }
        else if(animState == NPCAnimState.TALK)
        {
            TalkState();
        }
    }

    private void FixedUpdate()
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, 1.5f, 1 << 7);

        if (collider.Length > 0)
        {
            if (animState != NPCAnimState.TALK)
                playerController = collider[0].GetComponent<PlayerController>();

            if(playerController != null && !playerController.IsOccupied && !isAvailable)
            {
                if(dialogueController != null)
                    dialogueController.Show(true, new Vector3(0, 2.15f, 0f), npcTransform);
                playerController.IsOccupied = isAvailable = true;
            }

            if (isAvailable)
            {
                Vector3 dir = collider[0].transform.position - transform.position;
                Quaternion rot = Quaternion.LookRotation(dir, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, 5f);
            }

            animState = NPCAnimState.TALK;

        }
        else
        {
            if (animState == NPCAnimState.TALK)
            {
                isAvailable = false;
                if (playerController != null)
                    playerController.IsOccupied = false;

                if (dialogueController != null)
                    dialogueController.Show(false, Vector3.zero);
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

    void TalkState()
    {
        animator.SetFloat("Speed", 0);
        navMeshAgent.speed = 0f;
    }

    void ShowCharacter(int idx)
    {
        for(int i = 0; i < npcCharacter.Length; i++)
            npcCharacter[i].SetActive(i == idx);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + transform.up, 2);
    }
}
