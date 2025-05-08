using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    public float TerrainSpeed { get { return terrainsSpeed; } }


    [SerializeField] PlayerController playerController;
    [SerializeField] float terrainDistance;
    [SerializeField] float terrainOffset;

    [SerializeField] float spawnTimer;
    [SerializeField] Material terrainMaterial;

    [SerializeField] Transform obstacleSpawnPosition;

    [SerializeField] bool startSpawnObstacle = false;

    [SerializeField] float terrainsSpeed;
    [SerializeField] float reduceTerainSpeed;

    [SerializeField] float reduceSpeedTime;
    [SerializeField] Material waterMaterial;

    private List<GameObject> terrainPool;
    private List<GameObject> obstaclePool;
    private List<GameObject> coinPool;
    private float timer;

    private int currentObstacleIndex = 0;

    private float speedTimer;
    private bool isSpeedReduce = false;

    private InGameITController inGameController;


    private void Awake()
    {
        ResourceManager.instance.IntializeIslandObjectData();
    }

    // Start is called before the first frame update
    void Start()
    {
        inGameController = FindObjectOfType<InGameITController>();

        terrainPool = PoolManager.instance.GetObjectPool("Terrain");
        obstaclePool = PoolManager.instance.GetObjectPool("Obstacle");
        coinPool = PoolManager.instance.GetObjectPool("Coin");

        timer = 0f;
        speedTimer = 0f;

        InitializeTerrain();

        GameEvents.OnChangeTerrainSpeed.Invoke(terrainsSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSpawnObstacle();
        UpdateSpeed();
    }

    public void StartSpawnObstacle(bool start)
    {
        startSpawnObstacle = start;
    }

    public void StopRecycling()
    {
        startSpawnObstacle = false;
        for (int i = 0; i < terrainPool.Count; i++)
        {
            TerrainObject to = terrainPool[i].GetComponent<TerrainObject>();
            to.StopRecyling();
        }
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

    void UpdateSpawnObstacle()
    {
        if (!startSpawnObstacle)
            return;

        if(timer + spawnTimer < Time.time)
        {
            SpawnObstacle();
            timer = Time.time;
        }
    }

    void SpawnObstacle()
    {
        if (currentObstacleIndex >= obstaclePool.Count)
            currentObstacleIndex = 0;

        GameObject obstacle = obstaclePool[currentObstacleIndex];

        ObstacleObject oo = obstacle.GetComponent<ObstacleObject>();

        oo.ShowObstacle(obstacleSpawnPosition.position, 
            -(terrainDistance + terrainOffset));
        oo.StartUpdate(true);

        currentObstacleIndex++;
    }

    public void ReduceSpeed()
    {
        isSpeedReduce = true;
        speedTimer = Time.time;

        GameEvents.OnChangeTerrainSpeed.Invoke(reduceTerainSpeed);
    }

    public void UpdateSpeed()
    {
        if (isSpeedReduce)
        {
            if (speedTimer + reduceSpeedTime < Time.time)
            {
                GameEvents.OnChangeTerrainSpeed.Invoke(terrainsSpeed);
                speedTimer = Time.time;
                isSpeedReduce = false;
            }
        }
    }

    public void Easy()
    {
        spawnTimer = 2f;

        inGameController.maxTravelTime = 90;

        terrainsSpeed = 30;
        reduceTerainSpeed = 10;

        waterMaterial.SetFloat("_FoamSpeed", -3);

        GameEvents.OnChangeTerrainSpeed.Invoke(terrainsSpeed);
    }

    public void Medium()
    {
        spawnTimer = 1.2f;
        inGameController.maxTravelTime = 60;

        terrainsSpeed = 50;
        reduceTerainSpeed = 30;

        waterMaterial.SetFloat("_FoamSpeed", -5);

        GameEvents.OnChangeTerrainSpeed.Invoke(terrainsSpeed);
    }

    public void Hard()
    {
        spawnTimer = 1.2f; 
        inGameController.maxTravelTime = 30;
        terrainsSpeed = 70;
        reduceTerainSpeed = 50;

        waterMaterial.SetFloat("_FoamSpeed", -7);

        GameEvents.OnChangeTerrainSpeed.Invoke(terrainsSpeed);
    }
}
