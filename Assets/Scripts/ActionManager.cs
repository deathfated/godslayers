using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ActionManager : MonoBehaviour
{
    [SerializeField] Image actionPanel;
    private TileManager _tileMan;

    public void ShowPanel(bool isShow)
    {
        actionPanel.gameObject.SetActive(isShow);
    }

    void Start()
    {
        _tileMan = GameObject.FindGameObjectWithTag("TileManager").GetComponent<TileManager>();
    }

    public void ActionIsPressed(string action)
    {
        _tileMan.CheckActionable(action);

    }
}
