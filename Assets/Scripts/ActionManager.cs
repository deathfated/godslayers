using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ActionManager : MonoBehaviour
{
    [SerializeField] Image actionPanel;
    private TileManager _tileMan;
    private GameObject _statsPanel;
    private GameObject _charaSprite;

    public void ShowPanel(bool isShow)
    {
        actionPanel.gameObject.SetActive(isShow);

        _charaSprite.GetComponent<RawImage>().texture = TokenManager.instance.GetTokenSprite(0).texture;
        _charaSprite.SetActive(isShow);

        _statsPanel.SetActive(false);
    }

    void Start()
    {
        _tileMan = GameObject.FindGameObjectWithTag("TileManager").GetComponent<TileManager>();
        _statsPanel = actionPanel.transform.GetChild(3).GetChild(1).gameObject;
        _charaSprite = actionPanel.transform.parent.GetChild(1).gameObject;
    }

    public void ActionIsPressed(string action)
    {
        _tileMan.CheckActionable(action);

    }

    public void StatIsPressed()
    {
        _statsPanel.SetActive(true);

        //set the stat values from singleton
        _statsPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Name : " + TokenManager.instance.GetTokenName(0);
        _statsPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "MaxHP : " + TokenManager.instance.GetTokenStat(0,"MaxHp");
        _statsPanel.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "CurHP : " + TokenManager.instance.GetTokenStat(0,"CurHp");

    }

    public void ShowEnemyPanel(bool IsShowEnemy)
    {
        actionPanel.gameObject.SetActive(false);
        /*_statsPanel.SetActive(IsShowEnemy);

        //set the stat values from singleton
        _statsPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Name : " + TokenManager.instance.GetTokenName(1);
        _statsPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "MaxHP : " + TokenManager.instance.GetTokenStat(1,"MaxHp");
        _statsPanel.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "CurHP : " + TokenManager.instance.GetTokenStat(1,"CurHp");*/

        _charaSprite.GetComponent<RawImage>().texture = TokenManager.instance.GetTokenSprite(1).texture;
        _charaSprite.SetActive(IsShowEnemy);

    }


}
