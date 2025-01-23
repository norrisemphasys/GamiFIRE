using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CameraType cameraType;

    public Camera isoCamera;
    public Camera thirdPersonCamera;
    public Camera minigameCamera;

    [SerializeField] Transform thirdPersonCameraTarget;
    [SerializeField] Transform isoCameraTraget;

    Transform isoCameraTransform;
    Transform thirdPersonCameraTransform;

    [Header("Third Person Camera Property")]
    [SerializeField] float distance = 10f;
    [SerializeField] float height = 1f;
    [SerializeField] float cameraSpeed = 5f;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        thirdPersonCamera = GameManager.instance.characterController.GetPlayer().thirdPersonCamera;
        thirdPersonCameraTransform = GameManager.instance.characterController.GetPlayer().lookAtTarget;
        isoCameraTransform = isoCamera.transform;

        SetCamera(cameraType);
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraType == CameraType.THIRD_PERSON)
        {
            /*  Vector3 targetPosition = new Vector3( thirdPersonCameraTarget.position.x, 
                  thirdPersonCameraTarget.position.y + height, 
                  thirdPersonCameraTarget.position.z + distance);

              thirdPersonCameraTransform.position = targetPosition;// Vector3.Lerp(thirdPersonCameraTransform.position, 
                  //targetPosition, Time.fixedDeltaTime * cameraSpeed);*/
            thirdPersonCameraTransform.LookAt(thirdPersonCameraTarget.position);
        }
    }
    public void SetCamera(CameraType type)
    {
        cameraType = type;

        isoCamera.gameObject.SetActive(cameraType == CameraType.ISO);
        thirdPersonCamera.gameObject.SetActive(cameraType == CameraType.THIRD_PERSON);
        minigameCamera.gameObject.SetActive(cameraType == CameraType.MINI_GAME);

        Debug.LogError("camera type " + type);
    }
}

public enum CameraType
{
    ISO,
    THIRD_PERSON,
    MINI_GAME
}