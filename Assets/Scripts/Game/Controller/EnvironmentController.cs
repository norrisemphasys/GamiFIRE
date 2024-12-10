using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnvironmentController : MonoBehaviour
{
    [SerializeField] IslandObjects[] environments;

    public bool hasBuildingSelected = false;
    public bool showPopupOnce = false;

    public GameObject particle;

    private GameManager gameManager;

    public List<BuildingData> selectedBuildingData = new List<BuildingData>();

    bool startAnimation = false;

    public void Init()
    {
        gameManager = GameManager.instance;
        showPopupOnce = false;

        for (int i = 0; i < environments.Length; i++)
        {
            for (int j = 0; j < environments[i].environments.Length; j++)
            {
                environments[i].environments[j].SetActive(false);
                environments[i].environments[j].transform.localScale = Vector3.zero;
            }
        }

        particle.SetActive(false);
        // UpdateEnvironment(true);
    }

    public void ClearBuildingData()
    {
        selectedBuildingData.Clear();
    }

    public void UpdateEnvironment(bool force = false)
    {
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

                if(enableIdx < environments[islandTypeIdx]?.environments.Length)
                {
                    if (environments[islandTypeIdx].environments[enableIdx] != null &&
                        !environments[islandTypeIdx].environments[enableIdx].activeSelf)
                    {
                        AnimateIsland(environments[islandTypeIdx].environments[enableIdx], ctr);

                        ctr++;
                    }
                }
            }
        }
    }

    public void AnimateBuildingList()
    {
        StartCoroutine(AnimateBuildingListEnum());
    }

    IEnumerator AnimateBuildingListEnum()
    {
        for (int i = 0; i < selectedBuildingData.Count; i++)
        {
            AnimateBuilding(selectedBuildingData[i].building, i);
            yield return new WaitUntil(()=> !startAnimation);
        }

        hasBuildingSelected = false;
        ClearBuildingData();
    }

    public void AnimateBuilding(GameObject go, float delay = 0)
    {
        startAnimation = true;

        go.transform.DOKill();
        go.SetActive(true);
        go.transform.localScale = new Vector3(1, 0, 1);
        go.transform.DOScaleY(1, 2f).SetEase(Ease.InOutBounce).SetDelay(delay).OnComplete(()=> 
        {
            startAnimation = false;
            particle.SetActive(false);
        });

        particle.transform.position = go.transform.position;
        particle.SetActive(true);
    }

    public void AnimateIsland(GameObject go, float delay = 0)
    {
        go.transform.DOKill();
        go.SetActive(true);
        go.transform.DOScale(Vector3.one, 1f).SetEase(Ease.InOutBounce).SetDelay(delay);

        string message = go.name + " has been added to the island.";
        PopupManager.instance.ShowNotification(message);
    }

    public int GetLowestBuildPrice()
    {
        int islandTypeIdx = (int)gameManager.IslandType;
        int lowestPrice = int.MaxValue;

        for(int i = 0; i < environments[islandTypeIdx].environments.Length; i++)
        {
            BuildingListView view = environments[islandTypeIdx].view[i];

            if (lowestPrice > view.data.Price && view.data.IsLock)
                lowestPrice = view.data.Price;
        }

        return lowestPrice;
    }
}

[System.Serializable]
public class IslandObjects
{
    public JobType type;
    public GameObject[] environments;
    public BuildingListView[] view;
}