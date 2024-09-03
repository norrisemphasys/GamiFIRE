using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CellController : MonoBehaviour
{
    public List<Cell> cellList = new List<Cell>();

    [SerializeField] GameObject cellPrefab;
    [SerializeField] Transform cellParent;

    [SerializeField] int maxCellCount;
    [SerializeField] float distance;
    [SerializeField] float startYPosition;

    int currentCellIndex = 0;
    int cellCounterIndex = 0;

    private void Awake()
    {
        Init();
    }

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
        currentCellIndex = 0;
        cellCounterIndex = 0;

        ResourceManager.instance.IntializeStudentIslandObjectData();

        for(int i = 0; i < maxCellCount; i++)
        {
            GameObject cellGO = GameObject.Instantiate(cellPrefab);
            Cell cell = cellGO.GetComponent<Cell>();

            float posIndex = (i / (float)maxCellCount) * Mathf.PI * 2f;

            Debug.Log(posIndex);

            float x = Mathf.Sin(posIndex) * distance;
            float z = Mathf.Cos(posIndex) * distance;

            cellGO.transform.SetParent(cellParent);
            cellGO.transform.localPosition = new Vector3(x, i == 0 ? startYPosition : 0, z);

            cellList.Add(cell);
        }
    }

    public void ActivateNextCell()
    {
        Cell nextCell = GetNextCell();
        nextCell.transform.DOLocalMoveY(10, 0.5f);
    }

    public void UpdateCellIndex()
    {
        cellCounterIndex++;
        currentCellIndex = cellCounterIndex % cellList.Count;

        ActivateNextCell();
    }

    public Cell GetCurrentCell()
    {
        return cellList[currentCellIndex];
    }

    public Cell GetNextCell(int increment = 1)
    {
        int idx = (currentCellIndex + increment) % cellList.Count;
        return cellList[idx];
    }

    public Cell GetPreviousCell()
    {
        int idx = currentCellIndex - 1 % cellList.Count;
        return cellList[idx];
    }
}

public enum CellType
{
    SCENARIO,
    SPINNER,
    MINIGAME
}