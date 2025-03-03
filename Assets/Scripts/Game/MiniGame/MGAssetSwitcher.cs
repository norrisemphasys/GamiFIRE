using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGAssetSwitcher : MonoBehaviour
{
    [SerializeField] SpriteRenderer bg;
    [SerializeField] GameObject[] assets;
    [SerializeField] Color[] color;

    public SpriteRenderer currentSpriteRenderer;

    // Start is called before the first frame update
    private void Awake()
    {
        int idx = 0;//(int)GameManager.instance.IslandType;
        if (bg != null)
            bg.color = color[idx];

        if(assets.Length > 0)
        {
            for (int i = 0; i < assets.Length; i++)
                assets[i].SetActive(i == idx);
        }

        currentSpriteRenderer = assets[idx].GetComponent<SpriteRenderer>();
    }
}
