using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    #region Singleton
    public static MapCreator Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    [Header("Map Variables")]
    private int size = 3;
    public int width;
    public int height;
    public Vector2 cellSize;
    public int[,] mapArray;
    public MapPartButtonInfo[,] mapArrayList;
    float screenHeightPixelValue = 828f;//toplamda 828 pixel'lik bir boþluk býrakacak ekranýn altýndan
    float buttonSizeMult = 3.73f;
    //cellSize.x / 3.73f = button width
    //cellSize.y / 3.73f = button height

    [Header("Map Part")]
    [SerializeField] private RectTransform mapPart;

    [Header("Map Canvas")]
    [SerializeField] private RectTransform mapCanvas;
    [SerializeField] private RectTransform gameInPanel;
    [SerializeField] private Camera cam;
 
    private void Start()
    {
        mapArray = new int[0, 0];
        mapArrayList = new MapPartButtonInfo[0, 0];
    }
    
    private Vector2 CalculateCellSize()
    {
        float w = Screen.width / size;
        float h = (Screen.height - screenHeightPixelValue) / size;
        return new Vector2(w, h);
    }

    public void CreateMap(int size)
    {
        ClearMap();

        this.size = size;
        width = size;
        height = size;
        cellSize = CalculateCellSize();
        mapArray = new int[width, height];
        mapArrayList = new MapPartButtonInfo[width, height];

        DrawMap();
    }
    
    void DrawMap()
    {
        for (int x = 0; x < mapArray.GetLength(0); x++)
        {
            for (int y = 0; y < mapArray.GetLength(1); y++)
            {
                CreateMapPart(GetScreenPosition(x, y), CalculateButtonSize(), x, y);

                //Debug.DrawLine(GetScreenPosition(x, y), GetScreenPosition(x, y + 1), Color.white, 100f);
                //Debug.DrawLine(GetScreenPosition(x, y), GetScreenPosition(x + 1, y), Color.white, 100f);
            }
        }
        //Debug.DrawLine(GetScreenPosition(0, height), GetScreenPosition(width, height), Color.white, 100f);
        //Debug.DrawLine(GetScreenPosition(width, 0), GetScreenPosition(width, height), Color.white, 100f);
    }

    public Vector2 GetScreenPosition(int x, int y)
    {
        return (new Vector2(x, y) * cellSize) + new Vector2(0f, screenHeightPixelValue);
    }

    private Vector2 CalculateButtonSize()
    {
        return new Vector2(cellSize.x / buttonSizeMult, cellSize.y / buttonSizeMult);
    }

    private void CreateMapPart(Vector3 worldPos, Vector2 size, int x, int y)
    {
        Vector2 anchoredPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(gameInPanel, worldPos, cam, out anchoredPos);

        //Button Pivot Position
        RectTransform mapButtonPart = Instantiate(mapPart, Vector3.zero, Quaternion.identity);
        mapButtonPart.parent = gameInPanel.transform;
        mapButtonPart.localPosition = new Vector3(anchoredPos.x, anchoredPos.y, 0f);
        mapButtonPart.localScale = Vector3.one;
        
        //Button LocalScale and Local Position
        RectTransform mapButton = mapButtonPart.transform.GetChild(0).GetComponent<RectTransform>();
        mapButton.sizeDelta = size;
        mapButton.anchoredPosition = new Vector3(size.x * .5f, size.y * .5f, 0f);

        //Button Information and mapArrayList Assignment
        MapPartButtonInfo mapPartInfoScript = mapButton.GetComponent<MapPartButtonInfo>();
        mapArrayList[x, y] = mapPartInfoScript;
        mapPartInfoScript.xRowInMapIndex = x;
        mapPartInfoScript.yColumnInMapIndex = y;
        //mapPartInfoScript.text.text = x.ToString() + ", " + y.ToString();
    }

    private void ClearMap()
    {
        for (int x = 0; x < mapArrayList.GetLength(0); x++)
            for (int y = 0; y < mapArrayList.GetLength(1); y++)
                Destroy(mapArrayList[x, y].transform.parent.gameObject);
    }
}