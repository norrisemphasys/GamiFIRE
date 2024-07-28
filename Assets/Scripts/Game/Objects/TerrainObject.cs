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

    private void Awake()
    {
        terrain = transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        
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

    public void UpdateTerrain() 
    {
        Vector3 pos = terrain.position;
        pos.z -= Time.deltaTime * speed;

        if (pos.z < playerDistance)
            pos.z = maxDistance;

        terrain.position = pos;
    }
}
