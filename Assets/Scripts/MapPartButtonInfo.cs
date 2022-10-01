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
        Debug.Log("Adli Ýþlemler Baþlatýldý");

        GameManager gameManagerSC = GameManager.Instance;
        MapPartButtonInfo[,] mapList = MapCreator.Instance.mapArrayList;
        int maxRow = mapList.GetLength(0);
        int maxColumn = mapList.GetLength(1);

        /*if (gameManagerSC.matchedCheckIndexList.Count == 0) */gameManagerSC.FillMatchedCheckIndexList(xRowInMapIndex, yColumnInMapIndex);

        //for (int i = 0; i < 1; i++) //eðer iþaretli bir komþusu varsa kontrol etmeye devam etmesin diye!
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
        //Listedeki ilk eleman 2.ye komþu mu
        //Eðer komþu ise 2. sýradaki elelman 3. ye komþu mu

        if (gameManagerSC.matchedCheckIndexList.Count >= 3)
        {
            gameManagerSC.matchedSeries.Clear();
            for (int i = 0; i < gameManagerSC.matchedCheckIndexList.Count; i++)
            {
                for (int j = 0; /*(i != j) && */j < gameManagerSC.matchedCheckIndexList.Count; j++)
                {
                    //if (i != j)
                    //{
                        //for (int k = 0; k < 1; k++)//eðer iþaretli bir komþusu varsa kontrol etmeye devam etmesin diye!
                        //{
                            if (IsRightNeighbor(maxRow, gameManagerSC.matchedCheckIndexList[i][0], gameManagerSC.matchedCheckIndexList[i][1], gameManagerSC.matchedCheckIndexList[j][0], gameManagerSC.matchedCheckIndexList[j][1])) 
                            { 
                                gameManagerSC.FillMatchedSeriesList(i);
                                gameManagerSC.FillMatchedSeriesList(j);
                                //break; 
                            }
                            if (IsLeftNeighbor(gameManagerSC.matchedCheckIndexList[i][0], gameManagerSC.matchedCheckIndexList[i][1], gameManagerSC.matchedCheckIndexList[j][0], gameManagerSC.matchedCheckIndexList[j][1]))
                            {
                                gameManagerSC.FillMatchedSeriesList(i);
                                gameManagerSC.FillMatchedSeriesList(j);
                                //break;
                            }
                            if (IsUpNeighbor(maxColumn, gameManagerSC.matchedCheckIndexList[i][0], gameManagerSC.matchedCheckIndexList[i][1], gameManagerSC.matchedCheckIndexList[j][0], gameManagerSC.matchedCheckIndexList[j][1]))
                            {
                                gameManagerSC.FillMatchedSeriesList(i);
                                gameManagerSC.FillMatchedSeriesList(j);
                                //break;
                            }
                            if (IsDownNeighbor(gameManagerSC.matchedCheckIndexList[i][0], gameManagerSC.matchedCheckIndexList[i][1], gameManagerSC.matchedCheckIndexList[j][0], gameManagerSC.matchedCheckIndexList[j][1]))
                            {
                                gameManagerSC.FillMatchedSeriesList(i);
                                gameManagerSC.FillMatchedSeriesList(j);
                                //break;
                            }
                        //}

                    //}
                }
                List<int> emptyList = new List<int>();
                gameManagerSC.matchedSeries.Add(emptyList);
            }
        }

        //if (gameManagerSC.matchedSeries.Count >= 3)
        //{
            ////ReverseList(gameManagerSC.matchedSeries);//gameManagerSC.matchedSeries.Reverse(); //büyükten küçüðe sýralamak için //null referans hatasýna sebeb olan satýr burasý!!!
                                                     //3-4-2-1-0 þeklinde sýralama yapýyor. RemoveAt ile birlikte 3 silindikten sonra 4 silinmeye çalýþýlýyor ve hata veriyor

            //for (int i = 0; i < gameManagerSC.matchedSeries.Count; i++)
            //{
                ////Debug.Log("sayý: " + gameManagerSC.matchedSeries[i]);
                //mapList[gameManagerSC.matchedCheckIndexList[gameManagerSC.matchedSeries[i]][0], gameManagerSC.matchedCheckIndexList[gameManagerSC.matchedSeries[i]][1]].ClearButton();
                ////gameManagerSC.matchedCheckIndexList.RemoveAt(gameManagerSC.matchedSeries[i]);
            //}

            ////for (int i = 0; i < gameManagerSC.matchedSeries.Count; i++)
            ////{
            ////    Debug.Log("Silinecek Indis: " + gameManagerSC.matchedSeries[i] + "\n Indis Ýçeriði: " + gameManagerSC.matchedCheckIndexList[gameManagerSC.matchedSeries[i]][0] + " , " + gameManagerSC.matchedCheckIndexList[gameManagerSC.matchedSeries[i]][1]);
            ////    gameManagerSC.matchedCheckIndexList.RemoveAt(gameManagerSC.matchedSeries[i]);
            ////    gameManagerSC.matchedCheckIndexList.TrimExcess();
            ////}

            //ClearMatchedCheckListAndFillAgain(gameManagerSC, mapList);

            //gameManagerSC.matchedCount++;
            //UIManager.Instance.UpdateMatchCountText(gameManagerSC.matchedCount);

            //gameManagerSC.matchedSeries.Clear();
        //}

        for (int i = 0; i < gameManagerSC.matchedSeries.Count; i++)
        {
            if (gameManagerSC.matchedSeries[i].Count >= 3)
            {
                for (int j = 0; j < gameManagerSC.matchedSeries[i].Count; j++)
                {
                    mapList[gameManagerSC.matchedCheckIndexList[gameManagerSC.matchedSeries[i][j]][0], gameManagerSC.matchedCheckIndexList[gameManagerSC.matchedSeries[i][j]][1]].ClearButton();
                }

                gameManagerSC.matchedCount++;
                UIManager.Instance.UpdateMatchCountText(gameManagerSC.matchedCount);
            }
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

    //void ReverseList(List<int> list) //Bu metod .Reverse() "3-4-2-1-0" þeklinde sýralama yaptýðý için yazýldý!
    //{
    //    int temp = 0;

    //    for (int i = 0; i <= list.Count - 1; i++)
    //    {
    //        for (int j = i + 1; j < list.Count; j++)
    //        {
    //            if (list[i] < list[j])
    //            {
    //                temp = list[i];
    //                list[i] = list[j];
    //                list[j] = temp;
    //            }
    //        }
    //    }
    //}

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
