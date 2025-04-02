using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "SO/BadgeInfo", order = 4)]
public class BadgeInfoSO : ScriptableObject
{
    public string badgeID;
    public JobType type;
    public Sprite icon;
    public string title;
    public string description;
    public string pointOfAttention;

    public bool locked;
}
