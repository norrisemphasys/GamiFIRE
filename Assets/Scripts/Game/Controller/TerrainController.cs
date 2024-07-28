using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] float terrainDistance;
    [SerializeField] float terrainOffset;

    [SerializeField] Material terrainMaterial;

    private List<GameObject> terrainPool;

    private void Awake()
    {
        ResourceManager.instance.IntializeIslandObjectData();
    }

    // Start is called before the first frame update
    void Start()
    {
        terrainPool = PoolManager.instance.GetObjectPool("Terrain");

        InitializeTerrain();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void InitializeTerrain()
    {
        float maxDistance = ( terrainPool.Count - 1 ) * terrainDistance;
        for (int i = 0; i < terrainPool.Count; i++)
        {
            terrainPool[i].SetActive(true);

            Vector3 pos = terrainPool[i].transform.position;
            pos.z += i * terrainDistance;
            terrainPool[i].transform.position = pos;

            TerrainObject to = terrainPool[i].GetComponent<TerrainObject>();

            to.SetMaterial(terrainMaterial);
            to.Initialized(maxDistance, -(terrainDistance + terrainOffset));
            to.StartUpdate(true);
        }
    }
}
