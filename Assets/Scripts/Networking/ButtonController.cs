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

    /// <summary>
    /// when a SyncVar is updated on the server, it get synced on the clients.
    /// if a hook method is specified, then the hook method will be called on the client
    /// when the variable is updated.
    /// </summary>
    [SyncVar(hook = "OnSyncMode")]
    public ButtonModes mode = ButtonModes.None;

    [SyncVar(hook = "OnSyncColor")]
    public ButtonColors color = ButtonColors.White;

    [SyncVar(hook = "OnSyncWall")]
    public ButtonWalls walls = ButtonWalls.None;

	/// <summary>
    /// 
    /// </summary>
	void Start () {
        gameBoard = GameObject.Find("pnlGameBoard");
        InitButton();
    }
	
    /// <summary>
    /// 
    /// </summary>
    void InitButton()
    {
        //set the name of the button
        transform.name = "square" + gameBoard.transform.childCount.ToString();
        //place it on the board
        transform.SetParent(gameBoard.transform);
        //get the recttransform
        RectTransform rt = GetComponent<RectTransform>();
        //attempt to place the item on the surface of the ui
        rt.transform.localPosition = new Vector3(rt.transform.position.x, rt.transform.position.y, 0);
        //init the ui of the button
        UpdateUI();
    }

    /// <summary>
    /// 
    /// </summary>
    public void ButtonClickHandler()
    {
        //for security reasons, the new UNet system allows only scripts running on locally spawned objects to 
        //talk to the server.  since the buttons were spawned on the server, they can talk to the server 
        //only if we are running on the host. since the only thing the client spawned is its own player
        //object, all of our communication to the server has to flow through that.

        //so, if we are running on the server, then we can call UpdateButtonMode() directly.  if we are
        //running on the client, then we have to route our calls through the player object.  for consistency,
        //we will go ahead and route through the player object in both conditions.

        //tell the local player to tell the server to update this button
        if (LeafGamePlayer.localPlayer != null) LeafGamePlayer.localPlayer.UpdateBoardButton(transform.name);


    }

    /// <summary>
    /// This method is only called on the server.  When the user clicks the button, the event is sent to the 
    /// player object.  The player object routes the event up to the server.  And, the player object on the
    /// server calls this method.  Since mode is a SyncVar, the server will sync its values on all of the clients.
    /// </summary>
    public void UpdateButtonMode()
    {

        //Toggle button mode
        mode++;
        if (mode >= (ButtonModes)Enum.GetNames(typeof(ButtonModes)).Length) mode = 0;
        UpdateUI();
    }

    ///===== callback from sync var

    /// <summary>
    /// this method gets called when the color syncvar gets changed on the server
    /// </summary>
    /// <param name="mode"></param>

    public void OnSyncColor(ButtonColors color)
    {
        //if we don't use a hook function, then this.color gets set automatically.  but, if you 
        //do implement a hook function, then you have to populate 
        this.color = color;
        UpdateUI();
    }

    /// <summary>
    /// this method gets called when the mode syncvar gets changed on the server
    /// </summary>
    /// <param name="mode"></param>
    public void OnSyncMode(ButtonModes mode)
    {
        this.mode = mode;
        UpdateUI();
    }

    /// <summary>
    /// this method gets called when the walls syncvar gets changed on the server
    /// </summary>
    /// <param name="walls"></param>
    public void OnSyncWall(ButtonWalls walls)
    {
        this.walls = walls;
        UpdateUI();
    }

    //=====UI

    /// <summary>
    /// 
    /// </summary>
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
