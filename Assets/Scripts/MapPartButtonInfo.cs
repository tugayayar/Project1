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
    
    private void CheckPoint() //silinecek olan X'leri kontrol edip silid�imiz yer
    {
        Debug.Log("Adli ��lemler Ba�lat�ld�");

        GameManager gameManagerSC = GameManager.Instance;
        MapPartButtonInfo[,] mapList = MapCreator.Instance.mapArrayList;
        int maxRow = mapList.GetLength(0);
        int maxColumn = mapList.GetLength(1);

        /*if (gameManagerSC.matchedCheckIndexList.Count == 0) */gameManagerSC.FillMatchedCheckIndexList(xRowInMapIndex, yColumnInMapIndex);

        //for (int i = 0; i < 1; i++) //e�er i�aretli bir kom�usu varsa kontrol etmeye devam etmesin diye!
        //{
        //    if (CheckRight(gameManagerSC, maxRow, maxColumn, mapList)) break;
        //    if (CheckLeft(gameManagerSC, maxRow, maxColumn, mapList)) break;
        //    if (CheckUp(gameManagerSC, maxRow, maxColumn, mapList)) break;
        //    if (CheckDown(gameManagerSC, maxRow, maxColumn, mapList)) break;
        //}

        //Check Is Player Matched
        CheckIsPlayerMatched(gameManagerSC, maxRow, maxColumn, mapList);
    }

    //private bool CheckRight(GameManager gameManagerSC, int maxRow, int maxColumn, MapPartButtonInfo[,] mapList)
    //{
    //    if (xRowInMapIndex + 1 < maxRow && mapList[xRowInMapIndex + 1, yColumnInMapIndex].isPlayerChecked) //right
    //    {
    //        //gameManagerSC.FillMatchedCheckIndexList(xRowInMapIndex, yColumnInMapIndex);//gameManagerSC.FillMatchedCheckIndexList(xRowInMapIndex + 1, yColumnInMapIndex);
    //        return true;
    //    }
    //    return false;
    //}

    //private bool CheckLeft(GameManager gameManagerSC, int maxRow, int maxColumn, MapPartButtonInfo[,] mapList)
    //{
    //    if (xRowInMapIndex - 1 >= 0 && mapList[xRowInMapIndex - 1, yColumnInMapIndex].isPlayerChecked) //left
    //    {
    //        gameManagerSC.FillMatchedCheckIndexList(xRowInMapIndex, yColumnInMapIndex);//gameManagerSC.FillMatchedCheckIndexList(xRowInMapIndex - 1, yColumnInMapIndex);
    //        return true;
    //    }
    //    return false;
    //}

    //private bool CheckUp(GameManager gameManagerSC, int maxRow, int maxColumn, MapPartButtonInfo[,] mapList)
    //{
    //    if (yColumnInMapIndex + 1 < maxColumn && mapList[xRowInMapIndex, yColumnInMapIndex + 1].isPlayerChecked) //up
    //    {
    //        gameManagerSC.FillMatchedCheckIndexList(xRowInMapIndex, yColumnInMapIndex);//gameManagerSC.FillMatchedCheckIndexList(xRowInMapIndex, yColumnInMapIndex + 1);
    //        return true;
    //    }
    //    return false;
    //}

    //private bool CheckDown(GameManager gameManagerSC, int maxRow, int maxColumn, MapPartButtonInfo[,] mapList)
    //{
    //    if (yColumnInMapIndex - 1 >= 0 && mapList[xRowInMapIndex, yColumnInMapIndex - 1].isPlayerChecked) //down
    //    {
    //        gameManagerSC.FillMatchedCheckIndexList(xRowInMapIndex, yColumnInMapIndex);//gameManagerSC.FillMatchedCheckIndexList(xRowInMapIndex, yColumnInMapIndex - 1);
    //        return true;
    //    }
    //    return false;
    //}
    
    private bool IsRightNeighbor(int maxIndex, int currentX, int nextX)
    {
        if (currentX + 1 < maxIndex && currentX + 1 == nextX) return true;
        return false;
    }

    private bool IsLeftNeighbor(int currentX, int nextX)
    {
        if (currentX - 1 >= 0 && currentX - 1 == nextX) return true;
        return false;
    }

    private bool IsUpNeighbor(int maxIndex, int currentY, int nextY)
    {
        if (currentY + 1 < maxIndex && currentY + 1 == nextY) return true;
        return false;
    }

    private bool IsDownNeighbor(int currentY, int nextY)
    {
        if (currentY - 1 >= 0 && currentY - 1 == nextY) return true;
        return false;
    }

    public void CheckIsPlayerMatched(GameManager gameManagerSC, int maxRow, int maxColumn, MapPartButtonInfo[,] mapList)
    {
        //Listedeki ilk eleman 2.ye kom�u mu
        //E�er kom�u ise 2. s�radaki elelman 3. ye kom�u mu

        if (gameManagerSC.matchedCheckIndexList.Count >= 3)
        {
            for (int i = 0; i < gameManagerSC.matchedCheckIndexList.Count; i++)
            {
                for (int j = 0; j < gameManagerSC.matchedCheckIndexList.Count; j++)
                {
                    if (i != j)
                    {
                        for (int k = 0; k < 1; k++)//e�er i�aretli bir kom�usu varsa kontrol etmeye devam etmesin diye!
                        {
                            if (IsRightNeighbor(maxRow, gameManagerSC.matchedCheckIndexList[i][0], gameManagerSC.matchedCheckIndexList[j][0])) 
                            { 
                                gameManagerSC.FillMatchedSeriesList(i);
                                gameManagerSC.FillMatchedSeriesList(j);
                                break; 
                            }
                            if (IsLeftNeighbor(gameManagerSC.matchedCheckIndexList[i][0], gameManagerSC.matchedCheckIndexList[j][0]))
                            {
                                gameManagerSC.FillMatchedSeriesList(i);
                                gameManagerSC.FillMatchedSeriesList(j);
                                break;
                            }
                            if (IsUpNeighbor(maxColumn, gameManagerSC.matchedCheckIndexList[i][1], gameManagerSC.matchedCheckIndexList[j][1]))
                            {
                                gameManagerSC.FillMatchedSeriesList(i);
                                gameManagerSC.FillMatchedSeriesList(j);
                                break;
                            }
                            if (IsDownNeighbor(gameManagerSC.matchedCheckIndexList[i][1], gameManagerSC.matchedCheckIndexList[j][1]))
                            {
                                gameManagerSC.FillMatchedSeriesList(i);
                                gameManagerSC.FillMatchedSeriesList(j);
                                break;
                            }
                        }

                    }
                }
            }
        }

        if (gameManagerSC.matchedSeries.Count >= 3)
        {
            gameManagerSC.matchedSeries.Reverse(); //b�y�kten k����e s�ralamak i�in

            for (int i = 0; i < gameManagerSC.matchedSeries.Count; i++)
            {
                Debug.Log("say�: " + gameManagerSC.matchedSeries[i]);
                MapCreator.Instance.mapArrayList[gameManagerSC.matchedCheckIndexList[i][0], gameManagerSC.matchedCheckIndexList[i][1]].ClearButton();
                gameManagerSC.matchedCheckIndexList.RemoveAt(i);
            }

            gameManagerSC.matchedCount++;
            UIManager.Instance.UpdateMatchCountText(gameManagerSC.matchedCount);

            gameManagerSC.matchedSeries.Clear();
        }
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