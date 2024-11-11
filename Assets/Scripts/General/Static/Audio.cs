using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Audio 
{
    public static float bgmVolume = 0.3f;
    public static float sfxVolume = 1f;


    public static void PlaySFXStageClick()
    {
        MundoSound.Play(AddressableManager.GetClip("SFXStageClick"), sfxVolume);
    }

    public static void PlaySFXClick()
    {
        MundoSound.Play(AddressableManager.GetClip("SFXClick"), sfxVolume);
    }

    public static void PlaySFXClick2()
    {
        MundoSound.Play(AddressableManager.GetClip("SFXClick2"), sfxVolume);
    }

    public static void PlaySFXPopup()
    {
        MundoSound.Play(AddressableManager.GetClip("SFXPopup"), sfxVolume);
    }

    public static void PlayBGMLogin()
    {
        MundoSound.Play(AddressableManager.GetClip("BGMLogin"), bgmVolume, true);
    }

    public static void PlayBGMPort()
    {
        MundoSound.Play(AddressableManager.GetClip("BGMPort"), bgmVolume, true);
    }

    public static void PlayBGMSea()
    {
        MundoSound.Play(AddressableManager.GetClip("BGMSea"), bgmVolume, true);  
    }

    public static void PlaySFXWalk(int idx, bool useWorld = false, Transform t = null)
    {
        if(useWorld)
            MundoSound.Play(AddressableManager.GetClip(idx == 0 ? "SFXWalkLeft" : "SFXWalkRight"), sfxVolume, t.position, false);
        else
            MundoSound.Play(AddressableManager.GetClip(idx == 0 ? "SFXWalkLeft" : "SFXWalkRight"), sfxVolume);
    }

    public static void PlaySFXJump(Gender type)
    {
        MundoSound.Play(AddressableManager.GetClip(type == Gender.MALE ? "SFXMaleJump" : "SFXFemaleJump"), sfxVolume);
    }

    public static void PlaySFXLand(Gender type)
    {
        MundoSound.Play(AddressableManager.GetClip(type == Gender.MALE ? "SFXMaleLand" : "SFXFemaleLand"), sfxVolume);
    }

    public static void PlaySFXPortal()
    {
        MundoSound.Play(AddressableManager.GetClip("SFXPortal"), sfxVolume);
    }

    public static void PlayBGMIslandTrip()
    {
        MundoSound.Play(AddressableManager.GetClip("BGMIslandTrip"), bgmVolume, true);
    }

    public static void PlaySFXStream()
    {
        MundoSound.Play(AddressableManager.GetClip("SFXStream"), sfxVolume, true);
    }

    public static void PlaySFXEngine()
    {
        MundoSound.Play(AddressableManager.GetClip("SFXEngine"), sfxVolume, true);
    }

    public static void PlaySFXBoatHit()
    {
        MundoSound.Play(AddressableManager.GetClip("SFXHit2"), sfxVolume);
    }

    public static void PlaySFXCoin()
    {
        MundoSound.Play(AddressableManager.GetClip("SFXCoin"), sfxVolume);
    }

    public static void PlaySFXMove()
    {
        MundoSound.Play(AddressableManager.GetClip("SFXMove"), sfxVolume);
    }

    public static void PlaySFXGameOverWin()
    {
        MundoSound.Play(AddressableManager.GetClip("SFXGameOverWin"), sfxVolume);
    }

    public static void PlaySFXGameOverLose()
    {
        MundoSound.Play(AddressableManager.GetClip("SFXGameOverLose"), sfxVolume);
    }

    public static void PlaySFXCounter()
    {
        MundoSound.Play(AddressableManager.GetClip("SFXCounter"), sfxVolume);
    }

    public static void PlayBGMStudentIsland()
    {
        MundoSound.Play(AddressableManager.GetClip("BGMIslandStudent"), bgmVolume, true);
    }

    public static void PlaySFXRoll()
    {
        MundoSound.Play(AddressableManager.GetClip("SFXRoll"), sfxVolume);
    }

    public static void PlaySFXRollResult()
    {
        MundoSound.Play(AddressableManager.GetClip("SFXRollResult"), sfxVolume);
    }

    public static void PlaySFXSpin()
    {
        MundoSound.Play(AddressableManager.GetClip("SFXSpin"), sfxVolume);
    }

    public static void PlaySFXSpinResult()
    {
        MundoSound.Play(AddressableManager.GetClip("SFXSpinResult"), sfxVolume);
    }

    public static void PlayBGMMGOne()
    {
        MundoSound.Play(AddressableManager.GetClip("BGMMGOne"), bgmVolume, true);
    }

    public static void PlayBGMMGTwo()
    {
        MundoSound.Play(AddressableManager.GetClip("BGMMGTwo"), bgmVolume, true);
    }

    public static void PlaySFXMGCollect()
    {
        MundoSound.Play(AddressableManager.GetClip("SFXMGCollect"), sfxVolume);
    }

    public static void PlaySFXMGJump()
    {
        MundoSound.Play(AddressableManager.GetClip("SFXMGJump"), sfxVolume);
    }

    public static void PlaySFXMGWin()
    {
        MundoSound.Play(AddressableManager.GetClip("SFXMGWin"), sfxVolume);
    }

    public static void PlaySFXMGLose()
    {
        MundoSound.Play(AddressableManager.GetClip("SFXMGLose"), sfxVolume);
    }

    public static void PlaySFXMGDrop()
    {
        MundoSound.Play(AddressableManager.GetClip("SFXMGDrop"), sfxVolume);
    }

    public static void PlaySFXFinishCollectCoin()
    {
        MundoSound.Play(AddressableManager.GetClip("SFXCollectCoin"), sfxVolume);
    }

    public static void PlaySFXResult()
    {
        MundoSound.Play(AddressableManager.GetClip("SFXResult"), sfxVolume);
    }

    public static void PlayBGMScenarioQuestion()
    {
        MundoSound.Play(AddressableManager.GetClip("BGMScenarioQuestion"), bgmVolume, true);
    }

    public static void StopBGMScenarioQuestion()
    {
        MundoSound.Stop("BGMScenarioQuestion");
    }

    public static void StopBGMIslandTrip()
    {
        MundoSound.Stop("BGMIslandTrip");
        MundoSound.Stop("SFXStream");
        MundoSound.Stop("SFXEngine");
    }

    public static void StopBGMStudentIsland()
    {
        MundoSound.Stop("BGMIslandStudent");
        MundoSound.Stop("BGMSea");
    }

    public static void StopBGMMGOne()
    {
        MundoSound.Stop("BGMMGOne");
    }

    public static void StopBGMMGTwo()
    {
        MundoSound.Stop("BGMMGTwo");
    }
}
