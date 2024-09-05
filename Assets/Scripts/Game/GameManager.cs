using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public UIController uiController { get { return FindObjectOfType<UIController>(); } }
    public PlayerController playerController { get { return FindObjectOfType<PlayerController>(); } }
    public TerrainController terrainController { get { return FindObjectOfType <TerrainController>();} }
    public SceneController sceneController { get { return FindObjectOfType<SceneController>(); } }

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

    void OnStartState() 
    {

    }

    private bool _winITMode = false;
    public bool WinITMode { get { return _winITMode; } set { _winITMode = value; }  }

    private JobType _islandType = JobType.STUDENT;
    public JobType IslandType { get { return _islandType; } }
    public void SetIslandType(JobType type) { _islandType = type; }
    
}
