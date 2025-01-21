using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "SO/CustomItem", order = 3)]
public class CustomItemSO : ScriptableObject
{
    public JobType type;

    public int price;

    public bool isLock;
    public bool isSelected;

    public int selectedIndex;
}
