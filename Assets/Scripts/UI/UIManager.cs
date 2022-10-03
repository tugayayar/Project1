using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    [Header("Menu Panel Variables")]
    [SerializeField] TMP_InputField sizeIF;
    [SerializeField] Button rebuildButton;
    [SerializeField] TextMeshProUGUI matchCountText;

    [Header("Game In Panel Variables")]
    public Transform screenPointBottomLeftWorldRef;
    public Transform screenPointTopRightWorldRef;

    private int GetMapSize()
    {
        return int.Parse(sizeIF.text);
    }
    
    public void RebuildButtonClick()
    {
        MapCreator.Instance.CreateMap(GetMapSize());
        GameManager.Instance.matchedCheckIndexList.Clear();
        GameManager.Instance.matchedSeries.Clear();
        GameManager.Instance.matchedCount = 0;
        UpdateMatchCountText(GameManager.Instance.matchedCount);
    }

    public void UpdateMatchCountText(int count)
    {
        matchCountText.text = "Match Count: " + count.ToString();
    }
}
