using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameController : MonoBehaviour
{
    public UIController uiController;
    public MGIGOneController mgigOneController { get { return FindObjectOfType<MGIGOneController>(); } }

    public Dictionary<MiniGameType, MiniGame> miniGameBank = new Dictionary<MiniGameType, MiniGame>();

    [SerializeField] MiniGame[] miniGame;

    public T GetMiniGame<T>(MiniGameType type) where T : Component
    {
        if (miniGameBank.ContainsKey(type))
            return miniGameBank[type].GetComponent<T>();

        return null;
    }
    private void Start()
    {
        Init();
    }

    public void Init()
    {
        miniGameBank.Add(MiniGameType.MG_ONE, miniGame[0]);

        for (int i = 0; i < miniGame.Length; i++)
            miniGame[i].main.SetActive(false);

        uiController.ShowCanvas(false);
    }

    public void Load(MiniGameType type)
    {
        if (miniGameBank.ContainsKey(type))
        {
            switch (type)
            {
                case MiniGameType.MG_ONE: GetMiniGame<MGOne>(type).OnEnter(); break;
            }
        }
    }

    public void UnLoad(MiniGameType type)
    {
        if (miniGameBank.ContainsKey(type))
        {
            switch (type)
            {
                case MiniGameType.MG_ONE: GetMiniGame<MGOne>(type).OnExit(); break;
            }
        }
    }
}

public enum MiniGameType
{
    MG_ONE,
    MG_TWO,
    MG_THREE
}