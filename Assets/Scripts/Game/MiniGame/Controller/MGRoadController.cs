using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MGRoadController : MonoBehaviour
{
    PoolManager poolManager;
    public List<float> roadTypeProbabilityList = new List<float>();

    [SerializeField] MGPowerUpController powerUpController;

    [SerializeField] Transform parentRoad;
    private List<MGRoad> roadList = new List<MGRoad>();

    private int _roadCount = 0;
    private float _offset = 1.25f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {
        poolManager = PoolManager.instance;
        powerUpController.Init();

        InitRoad();
    }

    public void InitRoad()
    {
        roadList.Clear();

        for(int i = 0; i < 8; i++)
            SpawnRoad();
    }

    public void SpawnRoad()
    {
        
        GameObject road = poolManager.GetObject("Road");

        road.transform.SetParent(parentRoad);
        road.transform.localPosition = new Vector3(0f, _offset * (_roadCount + 1f), 0);

        MGRoad mgRoad = road.GetComponent<MGRoad>();
        mgRoad.powerupController = powerUpController;

        mgRoad.side = (MGRoad.RoadSide)Random.Range(0, 2);
        /* int lava = Random.Range(0, 10);
         if (lava > 7)
             mgRoad.type = (MGRoad.RoadType)1;*/
        mgRoad.type = (MGRoad.RoadType)Utils.GetPrizeByProbability(roadTypeProbabilityList);
        mgRoad.speed = Random.Range(1, 2);
        mgRoad.Init();

        roadList.Add(mgRoad);

        _roadCount++;
    }
    
    public void MoveRoad(float dir)
    {
        Vector2 pos = parentRoad.position;
        pos.y -= (_offset * dir);
        pos.y = Mathf.Clamp(pos.y, -1000f, -5.67f);
        //parentRoad.position = pos;
        parentRoad.DOMove(pos, 0.1f);
    }

    public void ResetPool()
    {
        poolManager.ResetAllObjectList("Road");
        poolManager.ResetAllObjectList("Enemy");
        poolManager.ResetAllObjectList("Bone");

        powerUpController.ResetPowerUp();
    }
}
