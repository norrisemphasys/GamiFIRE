using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] CellController cellController;
    [SerializeField] GameObject guidePrefab;

    [SerializeField] int maxGuideCount;
    [SerializeField] Animator animator;

    List<Vector3> guidePosition = new List<Vector3>();
    GameObject guideParent;

    // Start is called before the first frame update
    void Start()
    {
        guideParent = new GameObject("GuideParent");
        animator = GetComponent<Animator>();

        cellController.ActivateNextCell();

        UpdateLookAt();

        CreateGuide();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            FollowGuide(()=> 
            { 
                cellController.UpdateCellIndex();
                CreateGuide();
                UpdateLookAt();
            });
        }
    }

    void UpdateLookAt()
    {
        Cell currentCell = cellController.GetCurrentCell();
        Cell nextCell = cellController.GetNextCell();

        Vector3 nextCellPosition = new Vector3(nextCell.transform.position.x, 10, nextCell.transform.position.z);
        Vector3 direction = nextCellPosition - currentCell.transform.position;

        transform.rotation = Quaternion.LookRotation(direction);
        transform.position = currentCell.transform.position;
    }

    void FollowGuide(UnityAction callback = null)
    {
        StartCoroutine(FollowGuideEnum(callback));
    }

    IEnumerator FollowGuideEnum(UnityAction callback = null)
    {
        animator.SetTrigger("Jump");

        int guideCount = guidePosition.Count;

        for(int i = 0; i < guideCount; i++)
        {
            float t = i  / (float)guideCount;
            float eastTime = Utils.InOutQuad(t);

            transform.position = Vector3.Lerp(transform.position, guidePosition[i], eastTime);

            yield return new WaitForSeconds(0.025f);
        }

        callback?.Invoke();
    }

    void CreateGuide()
    {
        guidePosition.Clear();

        Cell currentCell = cellController.GetCurrentCell();
        Cell nextCell = cellController.GetNextCell();

        Vector3 nextCellPosition = new Vector3(nextCell.transform.position.x, 10, nextCell.transform.position.z);

        float midHeight = 10f;
        Vector3 GetMidPosition = Utils.GetMiddlePoint(currentCell.transform.position,
            nextCellPosition, 0.5f) + Vector3.up * midHeight;

        for (int i = 0; i < maxGuideCount; i++)
        {
            float t = (i+1) / (float)(maxGuideCount+1);

            GameObject guide = PoolManager.instance.GetObject("Guide");

            guide.SetActive(true);
            guide.transform.SetParent(guideParent.transform);
            guide.transform.position = Utils.GetPointInPath(currentCell.transform.position, 
                GetMidPosition, nextCellPosition, t);

            guidePosition.Add(guide.transform.position);
        }

        guidePosition.Add(nextCellPosition);
    }
}
