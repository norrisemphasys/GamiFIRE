using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public Canvas mainCanvas;
    public UIData[] UIData;

    private Dictionary<UIState, GameObject> uiDictionary = new Dictionary<UIState, GameObject>();

    void Awake()
    {
        for (int i = 0; i < UIData.Length; i++)
            uiDictionary.Add(UIData[i].state, UIData[i].panel);
    }

    void Start()
    {
        //Show(UIState.MainMenu);
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
            GetContoller<StageController>(state).OnEnter();
        }
    }

    public void Hide(UIState state)
    {
        if (uiDictionary.ContainsKey(state))
        {
            GetContoller<StageController>(state).OnExit();
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
    STAGE_SELECT
}