using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerObject : MonoBehaviour
{
    [SerializeField] UIState uiState;
    [SerializeField] string objectTag;

    [SerializeField] UnityEvent OnEnterTrigger;
    [SerializeField] UnityEvent OnExitTrigger;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals(objectTag))
        {
            if(uiState != UIState.NONE)
                GameManager.instance.uiController.ShowHidePreviousState(uiState);

            if(uiState == UIState.LEADERBOARD_MENU)
            {
               /* GameManager.instance.playerController.SetPause(true);
                PopupManager.instance.ShowPopup(PopupMessage.InfoPopup("Here you can see your score and compare it to other players! The content is not ready yet but make sure to come back and check it!", () =>
                {
                    GameManager.instance.playerController.SetPause(false);
                    GameManager.instance.uiController.Show(UIState.PORT_INGAME);
                }));*/
            }
            else if (uiState == UIState.CUSTOMIZATION_MENU)
            {
                GameManager.instance.playerController.SetPause(true);
                PopupManager.instance.ShowPopup(PopupMessage.InfoPopup("Here you can customize your player to your taste! The content is not ready yet but make sure to come back and check it!", () =>
                {
                    GameManager.instance.playerController.SetPause(false);
                    GameManager.instance.uiController.Show(UIState.PORT_INGAME);
                }));
            }
            else if(uiState == UIState.GAMEINFO_MENU)
            {
                GameManager.instance.playerController.SetPause(true);
                PopupManager.instance.ShowPopup(PopupMessage.InfoPopup("Here you will get to learn more about financial literacy! The content is not ready yet but make sure to come back and check it!", () =>
                {
                    GameManager.instance.playerController.SetPause(false);
                    GameManager.instance.uiController.Show(UIState.PORT_INGAME);
                }));
            }

            OnEnterTrigger?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals(objectTag))
        {
            OnExitTrigger?.Invoke();
        }   
    }
}
