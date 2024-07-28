using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    [SerializeField] Transform boatModel;
    [SerializeField] Transform boatTransform;

    [SerializeField] float distance;
    [SerializeField] float moveSpeed;
    [SerializeField] float titlSpeed;

    [SerializeField] float bounceSpeed;
    [SerializeField] float bounceRange;

    [SerializeField] float multiplier;

    [SerializeField] float tiltX;
    [SerializeField] float tiltY;

    private float _direction;
    private float _currentDirection;
    private float _lerpingDirection;

    private float _lerpTemp;

    private float _bounceTimer;

    private Vector3 _boatModelStartPosition;

    private void Awake()
    {
        boatTransform = transform;
        _currentDirection = 0;
        _lerpingDirection = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        _boatModelStartPosition = boatModel.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _direction = -1f;

            if (_currentDirection == -1 && _direction == -1) { }
            else
                _lerpingDirection = _direction * multiplier;

            _currentDirection += _direction;
            _currentDirection = Mathf.Clamp(_currentDirection, -1, 1);
        }
           
        if (Input.GetKeyDown(KeyCode.D))
        {
            _direction = 1f;

            if (_currentDirection == 1 && _direction == 1) { }
            else
                _lerpingDirection = _direction * multiplier;

            _currentDirection += _direction;
            _currentDirection = Mathf.Clamp(_currentDirection, -1, 1);
        }

        bool isMaxLeftLane = _direction == 1;
        bool isMaxRightLane = _direction == -1;

        _lerpingDirection = Mathf.Lerp(_lerpingDirection, 0f, Time.deltaTime * titlSpeed);

        Vector3 pos = boatTransform.position;

        pos.x = Mathf.Lerp(pos.x, _currentDirection * distance, Time.deltaTime * moveSpeed);
        pos.x = Mathf.Clamp(pos.x, -distance, distance);

        boatTransform.position = pos;

        Vector3 euler = boatModel.eulerAngles;

        euler.x = _lerpingDirection * tiltX;
        euler.y = 90 + (_lerpingDirection * tiltY);
     
        boatModel.eulerAngles = euler;

        Vector3 boatPos = new Vector3(boatModel.position.x, _boatModelStartPosition.y, boatModel.position.z) ;
        _bounceTimer += Time.deltaTime * bounceSpeed;
        boatPos.y += Mathf.Sin(_bounceTimer) * bounceRange;
        boatModel.position = boatPos;
    }
}
