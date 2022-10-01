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
    public List<List<int>> matchedSeries = new List<List<int>>();//public List<int> matchedSeries = new List<int>();

    public void FillMatchedCheckIndexList(int x, int y)
    {
        List<int> data = new List<int>();
        data.Add(x);
        data.Add(y);

        ListFiller(matchedCheckIndexList, data);

        //if (matchedCheckIndexList.Count == 0) matchedCheckIndexList.Add(data);
        //else
        //{
        //    int arrayCounter = 0;

        //    for (int i = 0; i < matchedCheckIndexList.Count; i++)
        //    {
        //        int xCounter = 0;
        //        int yCounter = 0;

        //        if (xCounter == 0 && matchedCheckIndexList[i][0] == x) xCounter++;
        //        if (yCounter == 0 && matchedCheckIndexList[i][1] == y) yCounter++;

        //        if (xCounter > 0 && yCounter > 0) //Bu index listede varmýþ!
        //        {
        //            arrayCounter++;
        //            break;
        //        }
        //    }

        //    if (arrayCounter == 0) matchedCheckIndexList.Add(data); //listede olmayan index eklendi
        //}

        //Debug.Log(matchedCheckIndexList.Count);
    }

    private void ListFiller(List<List<int>> list, List<int> addData)
    {
        if (list.Count == 0) list.Add(addData);
        else
        {
            int arrayCounter = 0;

            for (int i = 0; i < list.Count; i++)
            {
                int xCounter = 0;
                int yCounter = 0;

                if (xCounter == 0 && list[i][0] == addData[0]) xCounter++;
                if (yCounter == 0 && list[i][1] == addData[1]) yCounter++;

                if (xCounter > 0 && yCounter > 0) //Bu index listede varmýþ!
                {
                    arrayCounter++;
                    break;
                }
            }

            if (arrayCounter == 0) matchedCheckIndexList.Add(addData); //listede olmayan index eklendi
        }
    }

    public void FillMatchedSeriesList(int index)
    {
        //if (!matchedSeries.Contains(index)) matchedSeries.Add(index);

        List<int> indexData = new List<int>();
        indexData.Add(index);

        if (matchedSeries.Count == 0) matchedSeries.Add(indexData);
        else if (!matchedSeries[matchedSeries.Count - 1].Contains(index)) { matchedSeries[matchedSeries.Count - 1].Add(index); Debug.Log(matchedSeries.Count - 1 + ". indisde " + matchedSeries[matchedSeries.Count - 1].Count + " tane eleman var!"); };
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
