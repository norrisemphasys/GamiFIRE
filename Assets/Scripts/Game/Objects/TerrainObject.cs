using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainObject : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Renderer terrainRenderer;

    private Transform terrain;
    private bool startUpdate = false;

    private float maxDistance;
    private float playerDistance;

    private float currentSpeed;

    private bool recycle = true;

    private void Awake()
    {
        terrain = transform;
        GameEvents.OnChangeTerrainSpeed.AddListener(OnChangeSpeed);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnDestroy()
    {
        GameEvents.OnChangeTerrainSpeed.RemoveListener(OnChangeSpeed);
    }

    void OnChangeSpeed(float modfiedSpeed)
    {
        currentSpeed = modfiedSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (startUpdate)
            UpdateTerrain();
    }

    public void SetMaterial(Material mat)
    {
        terrainRenderer.sharedMaterial = mat;
    }

    public void Initialized(float returningDistance, 
        float recycleDistance)
    {
        maxDistance = returningDistance;
        playerDistance = recycleDistance;
    }

    public void StartUpdate(bool start)
    {
        startUpdate = start;
    }

    public void StopRecyling()
    {
        recycle = false;
    }

    public void UpdateTerrain() 
    {
        Vector3 pos = terrain.position;
        pos.z -= Time.deltaTime * currentSpeed;

        if (pos.z < playerDistance && recycle)
            pos.z = maxDistance;

        if(!recycle)
        {
            if (terrain.position.z > 280)
                gameObject.SetActive(false);
        }

        terrain.position = pos;
    }
}
