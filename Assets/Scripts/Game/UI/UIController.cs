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
        {
            ShowCanvas(useInitUIState);
            Show(initialUIState);
        }
    }

    public void ManualLoad()
    {
        ShowCanvas(true);
        Show(initialUIState);
    }

    public void ShowCanvas(bool show)
    {
        mainCanvas.gameObject.SetActive(show);
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

    public void ShowHidePreviousState(UIState state)
    {
        Hide(GameManager.instance.currentState);
        Show(state);
    }

    public void Show(UIState state)
    {
        if (uiDictionary.ContainsKey(state))
        {
            switch(state)
            {
                // PORT
                case UIState.STAGE_SELECT: GetContoller<StageController>(state).OnEnter(); break;
                case UIState.PORT_INGAME: GetContoller<InGamePortController>(state).OnEnter(); break;
                case UIState.PORT_TUTORIAL_MENU: GetContoller<PortTutotrialController>(state).OnEnter(); break;
                case UIState.LEADERBOARD_MENU: GetContoller<LeaderboardController>(state).OnEnter(); break;
                case UIState.CUSTOMIZATION_MENU: GetContoller<CustomizationController>(state).OnEnter(); break;

                // ISLAND TRIP
                case UIState.IT_INGAME: GetContoller<InGameITController>(state).OnEnter(); break;
                case UIState.IT_TUTORIAL: GetContoller<TutorialITController>(state).OnEnter(); break;
                case UIState.IT_PAUSE: GetContoller<PauseITController>(state).OnEnter(); break;
                case UIState.IT_GAMEOVER: GetContoller<GameOverITController>(state).OnEnter(); break;

                // ISLAND
                case UIState.ROLL_MENU: GetContoller<RollMenuController>(state).OnEnter(); break;
                case UIState.SPIN_MENU: GetContoller<SpinMenuController>(state).OnEnter(); break;
                case UIState.ISLAND_MENU: GetContoller<IslandUIController>(state).OnEnter(); break;
                case UIState.SQ_MENU: GetContoller<SQUIController>(state).OnEnter(); break;
                case UIState.RESULT_MENU: GetContoller<ResultController>(state).OnEnter(); break;
                case UIState.BUILDING_MENU: GetContoller<BuildingUIController>(state).OnEnter(); break;
                case UIState.ISLAND_TUTORIAL_MENU: GetContoller<IslandTutorialController>(state).OnEnter(); break;
                case UIState.POINTS_MENU: GetContoller<PointsController>(state).OnEnter(); break;

                // MINI GAME
                case UIState.MGIGONE_MENU: GetContoller<MGIGOneController>(state).OnEnter(); break;
                case UIState.MGIGTWO_MENU: GetContoller<MGIGTwoController>(state).OnEnter(); break;
                case UIState.MGIGTHREE_MENU: GetContoller<MGIGThreeController>(state).OnEnter(); break;
                case UIState.MGMM_MENU: GetContoller<MGMMController>(state).OnEnter(); break;
                case UIState.MGGO_MENU: GetContoller<MGGOController>(state).OnEnter(); break;
                case UIState.MGT_MENU: GetContoller<MGTController>(state).OnEnter(); break;

                // MAIN
                case UIState.SPLASH_MENU: GetContoller<SplashMenuController>(state).OnEnter(); break;
                case UIState.LOGIN: GetContoller<LoginController>(state).OnEnter(); break;
                case UIState.GENDER_MENU: GetContoller<GenderMenuController>(state).OnEnter(); break;
                case UIState.JOB_MENU: GetContoller<JobMenuController>(state).OnEnter(); break;
            }   
        }
    }

    public void Hide(UIState state)
    {
        Debug.LogError("HIDE " + state);
        if (uiDictionary.ContainsKey(state))
        {
            switch (state)
            {
                // PORT
                case UIState.STAGE_SELECT: GetContoller<StageController>(state).OnExit(); break;
                case UIState.PORT_INGAME: GetContoller<InGamePortController>(state).OnExit(); break;
                case UIState.PORT_TUTORIAL_MENU: GetContoller<PortTutotrialController>(state).OnExit(); break;
                case UIState.LEADERBOARD_MENU: GetContoller<LeaderboardController>(state).OnExit(); break;
                case UIState.CUSTOMIZATION_MENU: GetContoller<CustomizationController>(state).OnExit(); break;

                // ISLAND TRIP
                case UIState.IT_INGAME: GetContoller<InGameITController>(state).OnExit(); break;
                case UIState.IT_TUTORIAL: GetContoller<TutorialITController>(state).OnExit(); break;
                case UIState.IT_PAUSE: GetContoller<PauseITController>(state).OnExit(); break;
                case UIState.IT_GAMEOVER: GetContoller<GameOverITController>(state).OnExit(); break;

                // ISLAND
                case UIState.ROLL_MENU: GetContoller<RollMenuController>(state).OnExit(); break;
                case UIState.SPIN_MENU: GetContoller<SpinMenuController>(state).OnExit(); break;
                case UIState.ISLAND_MENU: GetContoller<IslandUIController>(state).OnExit(); break;
                case UIState.SQ_MENU: GetContoller<SQUIController>(state).OnExit(); break;
                case UIState.RESULT_MENU: GetContoller<ResultController>(state).OnExit(); break;
                case UIState.BUILDING_MENU: GetContoller<BuildingUIController>(state).OnExit(); break;
                case UIState.ISLAND_TUTORIAL_MENU: GetContoller<IslandTutorialController>(state).OnExit(); break;
                case UIState.POINTS_MENU: GetContoller<PointsController>(state).OnExit(); break;

                // MINI GAME
                case UIState.MGIGONE_MENU: GetContoller<MGIGOneController>(state).OnExit(); break;
                case UIState.MGIGTWO_MENU: GetContoller<MGIGTwoController>(state).OnExit(); break;
                case UIState.MGIGTHREE_MENU: GetContoller<MGIGThreeController>(state).OnExit(); break;
                case UIState.MGMM_MENU: GetContoller<MGMMController>(state).OnExit(); break;
                case UIState.MGGO_MENU: GetContoller<MGGOController>(state).OnExit(); break;
                case UIState.MGT_MENU: GetContoller<MGTController>(state).OnExit(); break;

                // MAIN
                case UIState.SPLASH_MENU: GetContoller<SplashMenuController>(state).OnExit(); break;
                case UIState.LOGIN: GetContoller<LoginController>(state).OnExit(); break;
                case UIState.GENDER_MENU: GetContoller<GenderMenuController>(state).OnExit(); break;
                case UIState.JOB_MENU: GetContoller<JobMenuController>(state).OnExit(); break;
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

    ROLL_MENU,
    SPIN_MENU,
    ISLAND_MENU,
    SQ_MENU,

    //MINI GAME
    MGIGONE_MENU,
    MGMM_MENU,
    MGGO_MENU,

    // MAIN,
    LOGIN,
    JOB_MENU,
    GENDER_MENU,

    // PORT
    PORT_INGAME,

    MGIGTWO_MENU,
    MGIGTHREE_MENU,

    RESULT_MENU,

    MGT_MENU,

    LEADERBOARD_MENU,
    GAMEINFO_MENU,
    CUSTOMIZATION_MENU,

    SPLASH_MENU,
    PORT_TUTORIAL_MENU,

    BUILDING_MENU,
    ISLAND_TUTORIAL_MENU,

    POINTS_MENU,

    NONE
}