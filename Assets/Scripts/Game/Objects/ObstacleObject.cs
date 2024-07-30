using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleObject : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] List<Transform> obstacle;

    [SerializeField] List<GameObject> coins = new List<GameObject>();

    List<int> indexes = new List<int>();

    private Transform coinParent;

    private Transform obstacleTransform;
    private bool startUpdate = false;

    private float playerDistance;
    // Start is called before the first frame update
    void Awake()
    {
        obstacleTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (startUpdate)
            UpdateTerrain();
    }

    void EnableObstacle(bool enable)
    {
        for (int i = 0; i < obstacle.Count; i++)
            obstacle[i].gameObject.SetActive(enable);
    }

    void CreateIndex()
    {
        indexes.Clear();

        indexes.Add(Random.Range(0, 2));
        indexes.Add(1);
        indexes.Add(0);

        Utils.Shuffle(indexes);
    }

    public void ShowObstacle(Vector3 position, float distance)
    {
        if (obstacleTransform == null)
            obstacleTransform = transform;

        obstacleTransform.position = position;
        playerDistance = distance;

        obstacleTransform.gameObject.SetActive(true);

        Utils.Shuffle(obstacle);

        EnableObstacle(false);
        CreateIndex();

        for (int i = 0; i < 3; i++)
        {
            obstacle[i].localPosition = new Vector3( (i - 1) * 12f, Random.Range(-2f, 0f), 0);
            obstacle[i].localRotation = Quaternion.Euler(new Vector3(0,Random.Range(0, 360), 0)); 
            obstacle[i].gameObject.SetActive(indexes[i] == 1);
        }

        CreateCoin();
    }

    void CreateCoin()
    {
        coins.Clear();

        for (int i = 0; i < indexes.Count; i++)
        {
            if (indexes[i] == 0)
            {
                bool createCoin = Random.Range(0, 10) < 2;
                if(createCoin)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        GameObject coin = PoolManager.instance.GetObject("Coin");
                        if (coinParent == null)
                            coinParent = coin.transform.parent;

                        coin.transform.SetParent(this.transform);
                        coin.transform.localPosition = new Vector3((i - 1) * 10f, 1.25f, (j - 2) * 4f);

                        coins.Add(coin);
                    }
                }
            }
        }
    }

    public void StartUpdate(bool start)
    {
        startUpdate = start;

        if(!start)
        {
            for (int i = 0; i < coins.Count; i++)
            {
                coins[i].SetActive(false);
                if (coinParent != null)
                    coins[i].transform.SetParent(coinParent);
            }
                
        }
    }

    public void UpdateTerrain()
    {
        Vector3 pos = obstacleTransform.position;
        pos.z -= Time.deltaTime * speed;

        if (pos.z < playerDistance)
            StartUpdate(false);

        obstacleTransform.position = pos;
    }
}
