using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField] bool useLocal;
    [SerializeField] float range;
    [SerializeField] float speed;

    private Transform target;

    private Vector3 startPosition;
    private float timer;


    private void Awake()
    {
        target = transform;
        startPosition = target.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        startPosition = target.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = startPosition;

        timer += Time.deltaTime * speed;
        pos.y += Mathf.Sin(timer) * range;
        target.position = pos;
    }
}
