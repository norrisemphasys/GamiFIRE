using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAt : MonoBehaviour
{
    [SerializeField] Transform targetLookAt;

    [SerializeField] bool startLookAt = false;

    Transform cameraTransform;

    private void Awake()
    {
        cameraTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(startLookAt)
            cameraTransform.LookAt(targetLookAt);
    }
}
