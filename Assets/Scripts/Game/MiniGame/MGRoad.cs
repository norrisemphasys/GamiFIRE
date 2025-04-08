using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGRoad : MonoBehaviour
{
    public enum RoadType
    {
        NORMAL,
        LAVA,
        POWERUP,
        NONE
    }

    public enum RoadSide
    {
        LEFT,
        RIGHT
    }

    public RoadSide side;
    public RoadType type;
    public float speed;

    private List<MGEnemy> enemyList = new List<MGEnemy>();
    private List<MGLog> boneList = new List<MGLog>();
    private PoolManager pool;

    [SerializeField] Color[] color;
    [SerializeField] SpriteRenderer sprRenderer;

    public MGPowerUpController powerupController;

    private void Awake()
    {
        sprRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init()
    {
        pool = PoolManager.instance;
        int colorIDX = type == RoadType.LAVA ? 1 : 0;
        sprRenderer.color = color[colorIDX];

        if (type == RoadType.NORMAL)
            SpawnEnemy();
        else if (type == RoadType.LAVA)
            SpawnBone();
        else if (type == RoadType.POWERUP)
            SpawnPowerUp();
    }

    private void Update()
    {
        
    }

    public void SpawnPowerUp()
    {
        powerupController.SpawnPowerUp(new Vector3(Random.Range(-3, 3), -0.26f, 0f), 
            (PickUpType)Random.Range(0, 3), false, 0, transform);
    }

    public void SpawnEnemy()
    {
        int cnt = 0;
        int maxCnt = Random.Range(2, 4);

        ResetEnemy();
        enemyList.Clear();

        for (int i = -3; i < 3; i++)
        {
            int randSpawn = Random.Range(0, 2);

            if (cnt < maxCnt && randSpawn <= 0)
            {
                GameObject enemy = pool.GetObject("Enemy");
                MGEnemy mgEnemy = enemy.GetComponent<MGEnemy>();

                enemy.transform.SetParent(transform);
                enemy.transform.localPosition = new Vector3(i, 0f, 0f);

                mgEnemy.side = side;
                mgEnemy.speed = speed;
                mgEnemy.Move();
                mgEnemy.SetIcon(Random.Range(0, 6));
                mgEnemy.SetSide();

                enemyList.Add(mgEnemy);

                cnt++;
                Debug.LogError("enemy count " + cnt);
            }
        }
    }

    public void SpawnBone()
    {
        int cnt = 0;
        int maxCnt = Random.Range(2, 4);

        ResetBone();
        boneList.Clear();

        for (int i = -3; i < 3; i++)
        {
            int randSpawn = Random.Range(0, 2);

            if (cnt < maxCnt && randSpawn <= 0)
            {
                GameObject bone = pool.GetObject("Bone");
                MGLog mgBone = bone.GetComponent<MGLog>();

                bone.transform.SetParent(transform);
                bone.transform.localPosition = new Vector3(i, 0f, 0f);

                mgBone.side = side;
                mgBone.speed = speed;
                mgBone.SetSafe(true);
                mgBone.Move();

                boneList.Add(mgBone);

                cnt++;
                Debug.LogError("bone count " + cnt);
            }
            else
            {
                GameObject bone = pool.GetObject("Bone");
                MGLog mgBone = bone.GetComponent<MGLog>();

                bone.transform.SetParent(transform);
                bone.transform.localPosition = new Vector3(i, 0f, 0f);

                mgBone.side = side;
                mgBone.speed = speed;
                mgBone.SetSafe(false);
                mgBone.Move();

                boneList.Add(mgBone);
            }
        }

        SpawnSingleBone(-4);
        SpawnSingleBone(4);
    }

    void SpawnSingleBone(float pos)
    {
        GameObject bone = pool.GetObject("Bone");
        MGLog mgBone = bone.GetComponent<MGLog>();

        bone.transform.SetParent(transform);
        bone.transform.localPosition = new Vector3(pos, 0f, 0f);

        mgBone.side = side;
        mgBone.speed = speed;
        mgBone.SetSafe(false);
        mgBone.Move();

        boneList.Add(mgBone);
    }

    void ResetBone()
    {
        if(boneList.Count > 0)
        {
            for(int i = 0; i < boneList.Count; i++)
            {
                boneList[i].Stop();
                boneList[i].gameObject.SetActive(false);
            }
        }
    }

    void ResetEnemy()
    {
        if (enemyList.Count > 0)
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                enemyList[i].Stop();
                enemyList[i].gameObject.SetActive(false);
            }
        }
    }
}
