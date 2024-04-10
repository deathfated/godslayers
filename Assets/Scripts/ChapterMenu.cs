using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChapterMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TitleText;
    [SerializeField] private TextMeshProUGUI descText;
    
    void Start()
    {
        Debug.Log("Last unlocked save :");

    }
    
    public void ChapterPressed(ChapterData data)
    {
        Debug.Log(data.gameObject.name + " selected");
        TitleText.text = data.ChapterTitle;
        descText.text = data.ChapterDesciption;
    }
}
