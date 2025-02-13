using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "SO/GameInfo", order = 4)]
public class GameInfoSO : ScriptableObject
{
    public TextAsset textAsset;
    public List<GameInfo> infos;

    public void LoadText()
    {
        infos = new List<GameInfo>();
        infos.Clear();

        string[] data = textAsset.text.Split("\n");
        Debug.LogError("len " + data.Length);

        for(int i = 0; i < data.Length; i++)
        {
            string[] dataInfo = data[i].Split(",*");
            GameInfo info = new GameInfo
            {
                Title = dataInfo[0],
                Content = dataInfo[1]
            };
            infos.Add(info);
        }
    }
}

[System.Serializable]
public class GameInfo
{
    public string Title;
    public string Content;
}