using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] GameObject[] character;

    private void Awake()
    {
        User currentUser = UserManager.instance.currentUser;
        int islandType = 0;

        if (currentUser != null)
            islandType = currentUser.JobType;

        for (int i = 0; i < character.Length; i++)
            character[i].SetActive(i == islandType);
    }

    public PlayerController GetPlayerController()
    {
        User currentUser = UserManager.instance.currentUser;
        int idx = 0;

        if (currentUser != null)
            idx = currentUser.JobType;

        return character[idx].GetComponent<PlayerController>();
    }

    public PlayerCustomizer GetPlayerCustomizer()
    {
        User currentUser = UserManager.instance.currentUser;
        int idx = 0;

        if (currentUser != null)
            idx = currentUser.JobType;

        return character[idx].GetComponent<PlayerCustomizer>();
    }
}
