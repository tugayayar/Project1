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
    private int size = 5;
    public int width;
    public int height;
    public float cellSize;
    public int[,] mapArray;

    [Header("Map Part")]
    [SerializeField] private Transform mapPart;
 
    private void Start()
    {
        width = size;
        height = size;
        cellSize = size;
        CreateMap(width, height, cellSize);
    }

    public void CreateMap(int width, int height, float cellSize)
    {
        mapArray = new int[width, height];

        DrawMap();
    }

    void DrawMap()
    {
        for (int x = 0; x < mapArray.GetLength(0); x++)
        {
            for (int z = 0; z < mapArray.GetLength(1); z++)
            {
                //Transform txt = Instantiate(textt, GetWorldPosition(x, z), Quaternion.identity);
                //txt.GetChild(0).GetComponent<TextMeshPro>().text = x + ", " + z;

                Transform createdMapPart = Instantiate(mapPart, GetWorldPosition(x, z), Quaternion.identity);
                createdMapPart.localScale = CalculateBoxSize();

                Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x + 1, z), Color.white, 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
    }

    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0, z) * cellSize;
    }

    public void GetCellPosition(Vector3 worldPos, out int x, out int z)
    {
        x = Mathf.FloorToInt(worldPos.x / cellSize);
        z = Mathf.FloorToInt(worldPos.z / cellSize);
        Debug.Log(x + ", " + z + " - " + mapArray[x, z].ToString());
    }

    private Vector3 CalculateBoxSize()
    {
        Vector3 desiredSize = new Vector3(size, .1f, size);
        return desiredSize;
    }
}
