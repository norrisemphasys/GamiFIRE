using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    Gender type;

    private void Start()
    {
        User user = UserManager.instance.currentUser;
        if (user != null)
            type = (Gender)user.Gender;
    }

    public void PlayWalkLeft()
    {
        Audio.PlaySFXWalk(0);
    }

    public void PlayWalkRight()
    {
        Audio.PlaySFXWalk(1);
    }

    public void PlayJump()
    {
        Audio.PlaySFXJump(type);
    }

    public void PlayLand()
    {
        Audio.PlaySFXLand(type);
    }
}
