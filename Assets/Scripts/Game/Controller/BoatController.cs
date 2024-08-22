using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoatController : MonoBehaviour
{
    [SerializeField] Transform boatModel;
    [SerializeField] Transform boatTransform;
    [SerializeField] Transform cameraTransform;

    [SerializeField] float distance;
    [SerializeField] float moveSpeed;
    [SerializeField] float titlSpeed;
    [SerializeField] float lerpDirectionSpeed;

    [SerializeField] float bounceSpeed;
    [SerializeField] float bounceRange;

    [SerializeField] float multiplier;

    [SerializeField] float tiltX;
    [SerializeField] float tiltY;

    private Collider boatCollider;

    private float _direction;
    private float _currentDirection;
    private float _lerpingDirection;

    private float _lerpTemp;

    private float _bounceTimer;

    private Vector3 _boatModelStartPosition;

    Animator animator;

    GameManager gameManager;

    private void Awake()
    {
        boatTransform = transform;
        _currentDirection = 0;
        _lerpingDirection = 0;

        boatCollider = GetComponent<Collider>();
        animator = GetComponent<Animator>();

        GameEvents.OnChangeTerrainSpeed.AddListener(OnChangeSpeed);
    }
    // Start is called before the first frame update
    void Start()
    {
        _boatModelStartPosition = boatModel.transform.position;

        gameManager = GameManager.instance;
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

        _lerpingDirection = Mathf.Lerp(_lerpingDirection, 0f, Time.deltaTime * lerpDirectionSpeed);

        Vector3 pos = boatTransform.position;

        pos.x = Mathf.Lerp(pos.x, _currentDirection * distance, Time.deltaTime * moveSpeed);
        pos.x = Mathf.Clamp(pos.x, -distance, distance);

        boatTransform.position = pos;

        Vector3 euler = boatModel.eulerAngles;

        euler.x = _lerpingDirection * tiltX;
        euler.y = Mathf.Lerp(euler.y, 90 + _lerpingDirection * tiltY, Time.deltaTime * titlSpeed);

        boatModel.eulerAngles = euler;

        Vector3 boatPos = new Vector3(boatModel.position.x, _boatModelStartPosition.y, boatModel.position.z);
        _bounceTimer += Time.deltaTime * bounceSpeed;
        boatPos.y += Mathf.Sin(_bounceTimer) * bounceRange;
        boatModel.position = boatPos;
    }

    void OnChangeSpeed(float modfiedSpeed)
    {
        if (modfiedSpeed == gameManager.terrainController.TerrainSpeed)
        {
            PlayIdle();
            boatCollider.enabled = true;
        }
        else
            boatCollider.enabled = false;
    }

    void PlayHitEffect()
    {
        animator.Play("Hit");
        cameraTransform.DOShakePosition(1);
    }

    void PlayIdle()
    {
        animator.Play("Idle");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigegr " + other.gameObject.name);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision " + collision.gameObject.name);

        if (collision.gameObject.tag.Equals("Coin"))
        {
            GameObject hitEffect = PoolManager.instance.GetObject("CoinHitEffect");
            hitEffect.transform.position = collision.transform.position;

            collision.gameObject.SetActive(false);

            GameEvents.OnCoinCollected.Invoke(1);
        }
        else if (collision.gameObject.tag.Equals("Obstacle"))
        {
            GameObject hitEffect = PoolManager.instance.GetObject("CoinSplashEffect");
            hitEffect.transform.position = transform.position;
            gameManager.terrainController.ReduceSpeed();
            PlayHitEffect();

            GameEvents.OnCoinCollected.Invoke(-3);
        }
    }

    private void OnDestroy()
    {
        GameEvents.OnChangeTerrainSpeed.RemoveListener(OnChangeSpeed);
    }
}
