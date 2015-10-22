using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class LeafGamePlayer : NetworkBehaviour {

    public static LeafGamePlayer localPlayer = null;

    public GameObject squarePrefab;
    public GameObject gameBoard;
    public Text txtRedScore;
    public Text txtBlueScore;
    public int score { get; set; }
    public Constants.PlayerColors playerColor { get; set; }

    /// <summary>
    /// 
    /// </summary>
    void Awake()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    void Start () {
        Debug.Log("player started");
        gameBoard = GameObject.Find("pnlGameBoard");
        txtRedScore = GameObject.Find("txtRedPlayerScore").GetComponent<Text>();
        txtBlueScore = GameObject.Find("txtBluePlayerScore").GetComponent<Text>();
        if (isLocalPlayer && isServer) InitBoard();

        //This method will run for each player that enters the game.  When the 
        //local player enters, we want to get a reference to it.
        if (isLocalPlayer) LeafGamePlayer.localPlayer = this;
    }

    /// <summary>
    /// 
    /// </summary>
    void Update () {

    }

    /// <summary>
    /// 
    /// </summary>
    void InitBoard()
    {
        for (int i = 0; i < 100; i++)
        {
            GameObject sqr = (GameObject)Instantiate(squarePrefab, Vector3.zero, Quaternion.identity);
            //sqr.transform.name = "square" + i.ToString();

            ButtonController btnCt = sqr.GetComponent<ButtonController>();
            if (i % 2 != 0)
            {
                btnCt.color = ButtonController.ButtonColors.White;
            }
            else
            {
                btnCt.color = ButtonController.ButtonColors.Black;
            }
            NetworkServer.Spawn(sqr);
        }

    }

    /// <summary>
    /// 
    /// </summary>
    void UpdateScoreUI()
    {
        if (playerColor == Constants.PlayerColors.Red)
        {
            txtRedScore.text = score.ToString();
        }
        else
        {
            txtBlueScore.text = score.ToString();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buttonName"></param>
    public void UpdateBoardButton(string buttonName)
    {
        //Debug.Log("Client : LeafGamePlayer.UpdateBoardButton() - " + buttonName);
        //For security reasons, only scripts running on the player objects or objects
        //spawned by the network manager can send messages to the server.  So, we expose
        //methods for the game board here
        CmdUpdatedBoardButton(buttonName);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buttonName"></param>
    [Command]
    public void CmdUpdatedBoardButton(string buttonName)
    {
        //Debug.Log("Server : LeafGamePlayer.CmdUpdatedBoardButton() - " + buttonName);
        //This code should be running on the server

        //Get a reference to the button that was clicked on the client
        GameObject clickedButton = GameObject.Find(buttonName);
        //get a ref to the button controller script
        ButtonController bt = clickedButton.GetComponent<ButtonController>();
        //this is the method that got called in the client's button click handler event.
        //when it runs on the server
        bt.UpdateButtonMode();
    }
}
