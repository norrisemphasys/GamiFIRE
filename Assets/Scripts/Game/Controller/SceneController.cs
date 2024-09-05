using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public QuestionController questionController { get { return FindObjectOfType<QuestionController>(); } }
    public CellController cellController { get { return FindObjectOfType<CellController>(); } }

    public int MoveCounter { get { return _moveCounter; } set { _moveCounter = value; } }
    private int _moveCounter = 0;

    private int _incrementer = 0;
    private int _questionCounter = 0;
    public int QuestionCounter { get { return _questionCounter; } }

    public int totalQuestionCount;

    List<QuestionSO> currentQuestionBank;

    private void Awake()
    {
        questionController.Init();
        cellController.Init();

        currentQuestionBank = questionController.GetShuffledQuestionByType(GameManager.instance.IslandType);
        totalQuestionCount = currentQuestionBank.Count;

        _questionCounter = 0;
        _incrementer = 0;
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
}
