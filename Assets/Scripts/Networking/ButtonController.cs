using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using System;

public class ButtonController : NetworkBehaviour {

    public enum ButtonModes
    { 
        None,
        Up,
        Down,
        Left,
        Right
    }

    public enum ButtonColors
    { 
        White,
        Black
    }

    public enum ButtonWalls
    { 
        None,
        Top,
        Left,
        Bottom,
        Right
    }

    public GameObject gameBoard;
    public Text txtText;
    public Image imgButton;

    [SyncVar(hook = "OnSyncMode")]
    public ButtonModes mode = ButtonModes.None;

    [SyncVar(hook = "OnSyncColor")]
    public ButtonColors color = ButtonColors.White;

    [SyncVar(hook = "OnSyncWall")]
    public ButtonWalls walls = ButtonWalls.None;

	// Use this for initialization
	void Start () {
        gameBoard = GameObject.Find("pnlGameBoard");
        //txtText = GetComponent<Text>();
        InitButton();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void InitButton()
    {
        transform.SetParent(gameBoard.transform);
        RectTransform rt = GetComponent<RectTransform>();

        //attempt to place the item on the surface of the ui
        rt.transform.localPosition = new Vector3(rt.transform.position.x, rt.transform.position.y, 0);
        UpdateUI();
    }

    /// <summary>
    /// 
    /// </summary>
    public void ButtonClickHandler()
    {
        //Toggle button mode
        mode++;
        if (mode >= (ButtonModes)Enum.GetNames(typeof(ButtonModes)).Length) mode = 0;
        UpdateUI();
    }

    ///===== callback from sync var

    public void OnSyncColor(ButtonColors color)
    {
        this.color = color;
        UpdateUI();
    }

    public void OnSyncMode(ButtonModes mode)
    {
        this.mode = mode;
        UpdateUI();
    }

    public void OnSyncWall(ButtonWalls walls)
    {
        this.walls = walls;
        UpdateUI();
    }

    //=====UI

    private void UpdateUI()
    {
        if (color == ButtonColors.White)
        {
            imgButton.color = Color.yellow;
        }
        else
        {
            imgButton.color = Color.cyan;
        }
        txtText.text = string.Format("{0}\n{1}\n{2}\n", color.ToString(), mode.ToString(), walls.ToString());
    }
}
