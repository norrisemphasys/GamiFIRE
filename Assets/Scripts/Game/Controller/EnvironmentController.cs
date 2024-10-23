using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnvironmentController : MonoBehaviour
{
    [SerializeField] IslandObjects[] environments;

    private GameManager gameManager;

    public void Init()
    {
        for (int i = 0; i < environments.Length; i++)
        {
            for (int j = 0; j < environments[i].environments.Length; j++)
            {
                environments[i].environments[j].SetActive(false);
                environments[i].environments[j].transform.localScale = Vector3.zero;
            }
        }

        UpdateEnvironment(true);
    }

    public void UpdateEnvironment(bool force = false)
    {
        gameManager = GameManager.instance;

        int islandTypeIdx = (int)gameManager.IslandType;
        int totalCell = gameManager.sceneController.cellController.maxCellCount;
        int currentCell = gameManager.sceneController.cellController.CurrentCellIndex;
        int environmentLength = environments[islandTypeIdx].environments.Length;
        int ratioCount = totalCell / environmentLength;

        if(force)
            AnimateIsland(environments[islandTypeIdx].environments[0], 1);
        else
        {

            Debug.LogError("currentCell " + currentCell);
            int ctr = 0;
            for (int i = 0; i < currentCell; i++)
            {
                int enableIdx = i / ratioCount;
                Debug.LogError("e idx " + enableIdx + " ratio cnt " + ratioCount);

                if (environments[islandTypeIdx].environments[enableIdx] != null &&
                    !environments[islandTypeIdx].environments[enableIdx].activeSelf)
                {
                    AnimateIsland(environments[islandTypeIdx].environments[enableIdx], ctr);

                    ctr++;
                }
            }
        }
    }

    public void AnimateIsland(GameObject go, float delay = 0)
    {
        go.transform.DOKill();
        go.SetActive(true);
        go.transform.DOScale(Vector3.one, 1f).SetEase(Ease.InOutBounce).SetDelay(delay);
    }
}

[System.Serializable]
public class IslandObjects
{
    public JobType type;
    public GameObject[] environments;
}