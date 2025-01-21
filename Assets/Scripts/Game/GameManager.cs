using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public UIController uiController 
    { 
        get 
        {
            GameObject uiGO = GameObject.Find("_Controllers");

            if (uiGO != null)
                return uiGO.GetComponent<UIController>();

            return FindObjectOfType<UIController>(); 
        } 
    }

    public CharacterController characterController { get { return FindObjectOfType<CharacterController>(); } }
    public PlayerController playerController { get { return characterController.GetPlayerController(); } }
    public TerrainController terrainController { get { return FindObjectOfType <TerrainController>();} }
    public SceneController sceneController { get { return FindObjectOfType<SceneController>(); } }
    public MiniGameController miniGameController { get { return FindObjectOfType<MiniGameController>(); } }

    public UIState _currentState;
    public UIState currentState { get { return _currentState; } }

    private UIState _previousState;
    public UIState previousState { get { return _previousState; } }
    public void SetGameState(UIState state)
    {
        UIState prevState = _currentState;
        _currentState = state;
        _previousState = prevState;

        OnStartState();
#if UNITY_EDITOR
        Debug.LogError("prev state " + _previousState.ToString()
                       + " current state " + _currentState.ToString());
#endif
    }

    public override void Init()
    {
        base.Init();

        currentScene = (SCENE_TYPE)UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
    }

    void OnStartState() 
    {

    }

    private bool _winITMode = false;
    public bool WinITMode { get { return _winITMode; } set { _winITMode = value; }  }

    private bool _winIslandMode = false;
    public bool WinIslandMode { get { return _winIslandMode; } set { _winIslandMode = value; } }

    private JobType _islandType = JobType.STUDENT;
    public JobType IslandType { get { return _islandType; } }
    public void SetIslandType(JobType type) { _islandType = type; }

    private SCENE_TYPE _currentScene = SCENE_TYPE.LOGIN_SCENE;
    public SCENE_TYPE currentScene { get { return _currentScene; } set { _currentScene = value; } }
    
}


public enum SCENE_TYPE
{
    LOGIN_SCENE = 0,
    PORT_SCENE,
    ISLAND_TRIP_SCENE,
    ISLAND_SCENE
}