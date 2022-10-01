using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MapPartButtonInfo : MonoBehaviour
{
    [Header("Button Info")]
    public int xRowInMapIndex;
    public int yColumnInMapIndex;
    public bool isPlayerChecked;

    [Header("Needed Components")]
    [SerializeField] private Image buttonImage;
    [SerializeField] private Button button;
    public TextMeshProUGUI text;
 
    [Header("Button Images")]
    [SerializeField] private Sprite emptyButtonImage;
    [SerializeField] private Sprite filledButtonImage;

    public void ButtonClick()
    {
        isPlayerChecked = true;
        ButtonImageChanger();
        CheckPoint();
    }
    
    private void CheckPoint() //silinecek olan X'leri kontrol edip silidðimiz yer
    {
        GameManager gameManagerSC = GameManager.Instance;
        MapPartButtonInfo[,] mapList = MapCreator.Instance.mapArrayList;
        int maxRow = mapList.GetLength(0);
        int maxColumn = mapList.GetLength(1);

        gameManagerSC.FillMatchedCheckIndexList(xRowInMapIndex, yColumnInMapIndex);

        //Check Is Player Matched
        CheckIsPlayerMatched(gameManagerSC, maxRow, maxColumn, mapList);
    }

    private bool IsRightNeighbor(int maxIndex, int currentX, int currentY, int nextX, int nextY)
    {
        if (currentY == nextY && currentX + 1 < maxIndex && currentX + 1 == nextX) return true;
        return false;
    }

    private bool IsLeftNeighbor(int currentX, int currentY, int nextX, int nextY)
    {
        if (currentY == nextY && currentX - 1 >= 0 && currentX - 1 == nextX) return true;
        return false;
    }

    private bool IsUpNeighbor(int maxIndex, int currentX, int currentY, int nextX, int nextY)
    {
        if (currentX == nextX && currentY + 1 < maxIndex && currentY + 1 == nextY) return true;
        return false;
    }

    private bool IsDownNeighbor(int currentX, int currentY, int nextX, int nextY)
    {
        if (currentX == nextX && currentY - 1 >= 0 && currentY - 1 == nextY) return true;
        return false;
    }

    public void CheckIsPlayerMatched(GameManager gameManagerSC, int maxRow, int maxColumn, MapPartButtonInfo[,] mapList)
    {
        if (gameManagerSC.matchedCheckIndexList.Count >= 3)
        {
            gameManagerSC.matchedSeries.Clear();

            for (int i = 0; i < gameManagerSC.matchedCheckIndexList.Count; i++)
            {
                for (int j = 0; j < gameManagerSC.matchedCheckIndexList.Count; j++)
                {
                    if (IsRightNeighbor(maxRow, gameManagerSC.matchedCheckIndexList[i][0], gameManagerSC.matchedCheckIndexList[i][1], gameManagerSC.matchedCheckIndexList[j][0], gameManagerSC.matchedCheckIndexList[j][1])) 
                    { 
                        gameManagerSC.FillMatchedSeriesList(i);
                        gameManagerSC.FillMatchedSeriesList(j);
                    }
                    if (IsLeftNeighbor(gameManagerSC.matchedCheckIndexList[i][0], gameManagerSC.matchedCheckIndexList[i][1], gameManagerSC.matchedCheckIndexList[j][0], gameManagerSC.matchedCheckIndexList[j][1]))
                    {
                        gameManagerSC.FillMatchedSeriesList(i);
                        gameManagerSC.FillMatchedSeriesList(j);
                    }
                    if (IsUpNeighbor(maxColumn, gameManagerSC.matchedCheckIndexList[i][0], gameManagerSC.matchedCheckIndexList[i][1], gameManagerSC.matchedCheckIndexList[j][0], gameManagerSC.matchedCheckIndexList[j][1]))
                    {
                        gameManagerSC.FillMatchedSeriesList(i);
                        gameManagerSC.FillMatchedSeriesList(j);
                    }
                    if (IsDownNeighbor(gameManagerSC.matchedCheckIndexList[i][0], gameManagerSC.matchedCheckIndexList[i][1], gameManagerSC.matchedCheckIndexList[j][0], gameManagerSC.matchedCheckIndexList[j][1]))
                    {
                        gameManagerSC.FillMatchedSeriesList(i);
                        gameManagerSC.FillMatchedSeriesList(j);
                    }
                }
                List<int> emptyList = new List<int>();
                gameManagerSC.matchedSeries.Add(emptyList);
            }
        }

        int matchCounter = 0;

        for (int i = 0; i < gameManagerSC.matchedSeries.Count; i++)
        {
            if (gameManagerSC.matchedSeries[i].Count >= 3)
            {
                for (int j = 0; j < gameManagerSC.matchedSeries[i].Count; j++)
                {
                    mapList[gameManagerSC.matchedCheckIndexList[gameManagerSC.matchedSeries[i][j]][0], gameManagerSC.matchedCheckIndexList[gameManagerSC.matchedSeries[i][j]][1]].ClearButton();
                }

                matchCounter++;
            }
        }

        if (matchCounter > 0)
        {
            gameManagerSC.matchedCount++;
            UIManager.Instance.UpdateMatchCountText(gameManagerSC.matchedCount);
        }

        gameManagerSC.matchedSeries.Clear();

        ClearMatchedCheckListAndFillAgain(gameManagerSC, mapList);
    }

    void ClearMatchedCheckListAndFillAgain(GameManager gmSC, MapPartButtonInfo[,] array)
    {
        gmSC.matchedCheckIndexList.Clear();

        for (int x = 0; x < array.GetLength(0); x++)
            for (int y = 0; y < array.GetLength(1); y++)
                if (array[x, y].isPlayerChecked) gmSC.FillMatchedCheckIndexList(x, y);
    }

    private void ButtonImageChanger()
    {
        if (isPlayerChecked) buttonImage.sprite = filledButtonImage;
        else buttonImage.sprite = emptyButtonImage;
    }

    public void ClearButton()
    {
        isPlayerChecked = false;
        ButtonImageChanger();
    }
}
