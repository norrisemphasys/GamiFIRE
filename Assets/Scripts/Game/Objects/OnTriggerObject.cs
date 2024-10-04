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
