using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputNavigatorHelper : MonoBehaviour
{
    EventSystem system;

    void Start()
    {
        system = EventSystem.current;// EventSystemManager.currentSystem;
    }

    // Update is called once per frame
    void Update()
    {
/*        if(Input.GetKey(KeyCode.LeftShift))
        {
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                if (system.currentSelectedGameObject != null)
                {
                    Selectable prev = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();

                    if (prev != null)
                    {

                        InputField inputfield = prev.GetComponent<InputField>();
                        if (inputfield != null)
                            inputfield.OnPointerClick(new PointerEventData(system));  //if it's an input field, also set the text caret

                        system.SetSelectedGameObject(prev.gameObject, new BaseEventData(system));
                    }
                    else Debug.Log("next nagivation element not found");
                }
            }
        }*/
        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (system.currentSelectedGameObject != null)
            {
                Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();

                if (next != null)
                {

                    InputField inputfield = next.GetComponent<InputField>();
                    if (inputfield != null)
                        inputfield.OnPointerClick(new PointerEventData(system));  //if it's an input field, also set the text caret

                    system.SetSelectedGameObject(next.gameObject, new BaseEventData(system));
                }
                else Debug.Log("next nagivation element not found");
            }
        }
    }
}