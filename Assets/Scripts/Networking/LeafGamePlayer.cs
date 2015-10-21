using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class LeafGamePlayer : NetworkBehaviour {

    public GameObject squarePrefab;
    public GameObject gameBoard;
    public Text txtRedScore;
    public Text txtBlueScore;
    public int score { get; set; }
    public Constants.PlayerColors playerColor { get; set; }

    /// <summary>
    /// 
    /// </summary>
    void Start () {
        Debug.Log("player started");
        gameBoard = GameObject.Find("pnlGameBoard");
        txtRedScore = GameObject.Find("txtRedPlayerScore").GetComponent<Text>();
        txtBlueScore = GameObject.Find("txtBluePlayerScore").GetComponent<Text>();
        if (isLocalPlayer && isServer) InitBoard();
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
            sqr.transform.name = "square" + i.ToString();

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
}
