using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGPlayer : MonoBehaviour
{
    [SerializeField] float offset;

    Transform playerTransform;
    Animator animator;

    private void Awake()
    {
        playerTransform = transform;
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerPosition(Vector3 pos)
    {
        playerTransform.position = new Vector3(pos.x, pos.y + offset,pos.z);
    }

    public void MoveAnimation()
    {
        if(animator != null)
            animator.SetTrigger("Move");
    }
}
