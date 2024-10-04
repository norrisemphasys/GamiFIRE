using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneController : MonoBehaviour
{
    public QuestionController questionController { get { return FindObjectOfType<QuestionController>(); } }
    public CellController cellController { get { return FindObjectOfType<CellController>(); } }
    public CameraController cameraController { get { return FindObjectOfType<CameraController>(); } }
    public MiniGameController minigameController { get { return FindObjectOfType<MiniGameController>(); } }

    public int MoveCounter { get { return _moveCounter; } set { _moveCounter = value; } }
    private int _moveCounter = 1;

    private bool _startGame = false;
    public bool StartGame { get { return _startGame; } set { _startGame = value; } }

    private int _incrementer = 0;
    private int _questionCounter = 0;

    // Temporary need to instantiate player programatically.
    public Player player;

    public int QuestionCounter { get { return _questionCounter; } }

    public int totalQuestionCount;

    List<QuestionSO> currentQuestionBank;

    private void Awake()
    {
        questionController.Init();
        cellController.Init();
        minigameController.Init();

        ScoreManager.instance.SetBonus(0, PrizeType.NONE);

        currentQuestionBank = questionController.GetShuffledQuestionByType(GameManager.instance.IslandType);
        totalQuestionCount = currentQuestionBank.Count;

        _questionCounter = 0;
        _incrementer = 0;

        Time.timeScale = 1;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Q))
            GameManager.instance.uiController.ShowHidePreviousState(UIState.SQ_MENU);
#endif
    }

    public void UpdateQuestionCounter() 
    {
        _incrementer++;
        _questionCounter = _incrementer % totalQuestionCount;
    }

    public QuestionSO GetCurrentQuestion()
    {
        return currentQuestionBank[_questionCounter];
    }

    public void StartPlayerCellMove(UnityAction callback = null)
    {
        player.StartMove(MoveCounter, callback);
    }
}
