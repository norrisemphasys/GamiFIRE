using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] CellController cellController;

    [SerializeField] int maxGuideCount;
    [SerializeField] Animator animator;

    List<Vector3> guidePosition = new List<Vector3>();
    GameObject guideParent;

    bool startJump = false;

    PlayerAudio playerAudio;

    // Start is called before the first frame update
    void Start()
    {
        guideParent = new GameObject("GuideParent");
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<PlayerAudio>();

        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) && !startJump)
        {
            FollowGuide(()=> 
            { 
                cellController.UpdateCellIndex();
                CreateGuide();
            });
        }
    }

    public void Init()
    {
        //cellController.ActivateNextCell();
        UpdateLookAt();
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
        startJump = true;
        StartCoroutine(FollowGuideEnum(callback));
    }

    IEnumerator FollowGuideEnum(UnityAction callback = null)
    {
        animator.SetTrigger("Jump");
        playerAudio.PlayJump();
        Cell currentCell = cellController.GetCurrentCell();
        Cell nextCell = cellController.GetNextCell(2);

        Vector3 nextCellPosition = new Vector3(nextCell.transform.position.x, 10, nextCell.transform.position.z);
        Vector3 direction = nextCellPosition - currentCell.transform.position;

        int guideCount = guidePosition.Count;

        for(int i = 0; i < guideCount; i++)
        {
            float t = i  / (float)guideCount;
            float eastTime = Utils.InOutQuad(t);

            transform.position = Vector3.Lerp(transform.position, guidePosition[i], eastTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), eastTime);

            yield return new WaitForSeconds(0.025f);
        }

        playerAudio.PlayLand();

        callback?.Invoke();
        startJump = false;
    }

    public void CreateGuide()
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

    public void StartMove(int count, UnityAction callback = null)
    {
        StartCoroutine(StartMoveEnum(count, callback));
    }

    IEnumerator StartMoveEnum(int count, UnityAction callback = null)
    {
        float moveDelay = 0.5f;
        Debug.LogError("MOVE COUNT " + count);
        cellController.ActivateNextCell();
        CreateGuide();

        for (int i = 0; i < count; i++)
        {
            FollowGuide(() =>
            {
                bool activateCell = i < count - 1;
                cellController.UpdateCellIndex(activateCell);
                if (activateCell)
                    CreateGuide();
            });

            yield return new WaitUntil(() => !startJump);
            yield return new WaitForSeconds(moveDelay);
        }

        DisableGuide();
        callback?.Invoke();
    }

    public void DisableGuide()
    {
        PoolManager.instance.ResetAllObjectList("Guide");
        guidePosition.Clear();
    }
}
