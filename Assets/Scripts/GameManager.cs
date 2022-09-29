using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public int matchedCount;
    public List<List<int>> matchedCheckIndexList = new List<List<int>>();
    public List<int> matchedSeries = new List<int>();

    public void FillMatchedCheckIndexList(int x, int y)
    {
        List<int> data = new List<int>();
        data.Add(x);
        data.Add(y);

        if (matchedCheckIndexList.Count == 0) matchedCheckIndexList.Add(data);
        else
        {
            int arrayCounter = 0;

            for (int i = 0; i < matchedCheckIndexList.Count; i++)
            {
                int xCounter = 0;
                int yCounter = 0;

                if (xCounter == 0 && matchedCheckIndexList[i][0] == x) xCounter++;
                if (yCounter == 0 && matchedCheckIndexList[i][1] == y) yCounter++;

                if (xCounter > 0 && yCounter > 0) //Bu index listede varmýþ!
                {
                    arrayCounter++;
                    break;
                }
            }

            if (arrayCounter == 0) matchedCheckIndexList.Add(data); //listede olmayan index eklendi
        }

        Debug.Log(matchedCheckIndexList.Count);
    }

    public void FillMatchedSeriesList(int index)
    {
        if (!matchedSeries.Contains(index)) matchedSeries.Add(index);
    }

    //public void CheckIsPlayerMatched()
    //{
    //    if (matchedCheckIndexList.Count >= 3)
    //    {
    //        for (int i = matchedCheckIndexList.Count - 1; i >= 0; i--)
    //        {
    //            MapCreator.Instance.mapArrayList[matchedCheckIndexList[i][0], matchedCheckIndexList[i][1]].ClearButton();
    //            matchedCheckIndexList.RemoveAt(i);
    //            Debug.Log("Sildim!");
    //        }

    //        matchedCount++;
    //        UIManager.Instance.UpdateMatchCountText(matchedCount);
    //    }
    //}
}
