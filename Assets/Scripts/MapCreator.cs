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
        width = size;
        height = size;
        cellSize = CalculateCellSize();
        CreateMap(width, height);
        
    }
    
    private Vector2 CalculateCellSize()
    {
        float w = Screen.width / size;
        float h = (Screen.height - screenHeightPixelValue) / size;
        return new Vector2(w, h);
    }

    public void CreateMap(int width, int height)
    {
        mapArray = new int[width, height];

        DrawMap();
    }
    
    void DrawMap()
    {
        for (int x = 0; x < mapArray.GetLength(0); x++)
        {
            for (int y = 0; y < mapArray.GetLength(1); y++)
            {
                CreateMapPart(GetScreenPosition(x, y), CalculateButtonSize());

                Debug.DrawLine(GetScreenPosition(x, y), GetScreenPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetScreenPosition(x, y), GetScreenPosition(x + 1, y), Color.white, 100f);
            }
        }
        Debug.DrawLine(GetScreenPosition(0, height), GetScreenPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetScreenPosition(width, 0), GetScreenPosition(width, height), Color.white, 100f);
    }

    public Vector2 GetScreenPosition(int x, int y)
    {
        return (new Vector2(x, y) * cellSize) + new Vector2(0f, screenHeightPixelValue);
    }

    public void GetCellPosition(Vector2 screenPos, out int x, out int y)
    {
        x = Mathf.FloorToInt(screenPos.x / cellSize.x);
        y = Mathf.FloorToInt(screenPos.y / cellSize.y);
        Debug.Log(x + ", " + y + " - " + mapArray[x, y].ToString());
    }

    private Vector2 CalculateButtonSize()
    {
        return new Vector2(cellSize.x / buttonSizeMult, cellSize.y / buttonSizeMult);
    }

    private void CreateMapPart(Vector3 worldPos, Vector2 size)
    {
        Vector2 anchoredPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(gameInPanel, worldPos, cam, out anchoredPos);

        RectTransform mapButtonPart = Instantiate(mapPart, Vector3.zero, Quaternion.identity);
        mapButtonPart.parent = gameInPanel.transform;
        mapButtonPart.localPosition = new Vector3(anchoredPos.x, anchoredPos.y, 0f);
        mapButtonPart.localScale = Vector3.one;

        RectTransform mapButton = mapButtonPart.transform.GetChild(0).GetComponent<RectTransform>();
        mapButton.sizeDelta = size;
        mapButton.anchoredPosition = new Vector3(size.x * .5f, size.y * .5f, 0f);

    }
}