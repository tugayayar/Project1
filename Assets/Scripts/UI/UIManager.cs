using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Menu Panel Variables")]
    [SerializeField] TMP_InputField sizeIF;
    [SerializeField] Button rebuildButton;
    [SerializeField] TextMeshProUGUI matchCountText;

    private int GetMapSize()
    {
        return int.Parse(sizeIF.text);
    }
    
    public void RebuildButtonClick()
    {
        MapCreator.Instance.CreateMap(GetMapSize());
    }
}
