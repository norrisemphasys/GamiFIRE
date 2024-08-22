using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public Canvas mainCanvas;
    public UIData[] UIData;

    private Dictionary<UIState, GameObject> uiDictionary = new Dictionary<UIState, GameObject>();

    [SerializeField] UIState initialUIState;
    [SerializeField] bool useInitUIState = false;

    void Awake()
    {
        for (int i = 0; i < UIData.Length; i++)
            uiDictionary.Add(UIData[i].state, UIData[i].panel);
    }

    void Start()
    {
        if(useInitUIState)
            Show(initialUIState);
    }

    public T GetContoller<T>(UIState state) where T : Component
    {
        if (uiDictionary.ContainsKey(state))
            return uiDictionary[state].GetComponent<T>();

        return null;
    }

    public void Show(UIState state, float time)
    {
        if (uiDictionary.ContainsKey(state))
            uiDictionary[state].GetComponent<BasicController>().OnEnter();
    }

    public void Hide(UIState state, float time)
    {
        if (uiDictionary.ContainsKey(state))
            uiDictionary[state].GetComponent<BasicController>().OnExit();
    }

    public void Show(UIState state)
    {
        if (uiDictionary.ContainsKey(state))
        {
            switch(state)
            {
                case UIState.STAGE_SELECT: GetContoller<StageController>(state).OnEnter(); break;
                case UIState.IT_INGAME: GetContoller<InGameITController>(state).OnEnter(); break;
                case UIState.IT_TUTORIAL: GetContoller<TutorialITController>(state).OnEnter(); break;
                case UIState.IT_PAUSE: GetContoller<PauseITController>(state).OnEnter(); break;
                case UIState.IT_GAMEOVER: GetContoller<GameOverITController>(state).OnEnter(); break;
            }   
        }
    }

    public void Hide(UIState state)
    {
        if (uiDictionary.ContainsKey(state))
        {
            switch (state)
            {
                case UIState.STAGE_SELECT: GetContoller<StageController>(state).OnExit(); break;
                case UIState.IT_INGAME: GetContoller<InGameITController>(state).OnExit(); break;
                case UIState.IT_TUTORIAL: GetContoller<TutorialITController>(state).OnExit(); break;
                case UIState.IT_PAUSE: GetContoller<PauseITController>(state).OnExit(); break;
                case UIState.IT_GAMEOVER: GetContoller<GameOverITController>(state).OnExit(); break;
            }
        }
    }
}

[System.Serializable]
public class UIData
{
    public UIState state;
    public GameObject panel;
}

public enum UIState
{
    STAGE_SELECT,

    IT_INGAME,
    IT_TUTORIAL,
    IT_PAUSE,
    IT_GAMEOVER,

    NONE
}