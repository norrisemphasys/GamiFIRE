using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameController : MonoBehaviour
{
    public UIController uiController;
    public MGIGOneController mgigOneController { get { return FindObjectOfType<MGIGOneController>(); } }
    public MGDropController mgDropController { get { return FindObjectOfType<MGDropController>(); } }

    public Dictionary<MiniGameType, MiniGame> miniGameBank = new Dictionary<MiniGameType, MiniGame>();

    [SerializeField] MiniGame[] miniGame;
    [SerializeField] MiniGameType initMiniGameType;
    [SerializeField] bool useInitUIState = false;

    private MiniGameType _currentType;
    public MiniGameType CurrentType { get { return _currentType; } }

    int counter = 1;

    void Start()
    {
        if (useInitUIState)
        {
            //Init();
            Load(initMiniGameType);
        }
    }

    public T GetMiniGame<T>(MiniGameType type) where T : Component
    {
        if (miniGameBank.ContainsKey(type))
            return miniGameBank[type].GetComponent<T>();

        return null;
    }

    public void Init()
    {
        miniGameBank.Add(MiniGameType.MG_ONE, miniGame[0]);
        miniGameBank.Add(MiniGameType.MG_TWO, miniGame[1]);

        for (int i = 0; i < miniGame.Length; i++)
            miniGame[i].main.SetActive(false);

        uiController.ShowCanvas(false);
        counter = 1;
    }

    public void Load(MiniGameType type)
    {
        if (miniGameBank.ContainsKey(type))
        {
            _currentType = type;
            Debug.LogError(type);

            switch (type)
            {
                case MiniGameType.MG_ONE: GetMiniGame<MGOne>(type).OnEnter(); break;
                case MiniGameType.MG_TWO: GetMiniGame<MGTwo>(type).OnEnter(); break;
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
                case MiniGameType.MG_TWO: GetMiniGame<MGTwo>(type).OnExit(); break;
            }
        }
    }

    public void RandomLoad()
    {
        // Load Random
        //MiniGameType type = (MiniGameType)Random.Range(1, 3);

        MiniGameType type = (MiniGameType)counter;
        Load(type);

        counter++;

        if (counter == 3)
            counter = 1;
    }

    public MiniGame GetCurrentMiniGame()
    {
        if (miniGameBank.ContainsKey(_currentType))
            return miniGameBank[_currentType];

        return null;
    }
}

public enum MiniGameType
{
    MG_ONE = 1,
    MG_TWO,
    MG_THREE
}