using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] Vector3 axis;
    [SerializeField] float speed;

    Transform objectTransform;

    private void Awake()
    {
        objectTransform = transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 euler = objectTransform.eulerAngles;
        euler += axis;

        objectTransform.rotation = Quaternion.Lerp(objectTransform.rotation, 
            Quaternion.Euler(euler), Time.deltaTime * speed);
    }
}
