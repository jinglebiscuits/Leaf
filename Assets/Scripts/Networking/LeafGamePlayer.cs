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
        //if (isLocalPlayer && isServer) InitBoard();
        Debug.Log("player started");
        InitBoard();
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
            sqr.transform.SetParent(gameBoard.transform);
            sqr.transform.name = "square" + i.ToString();
            RectTransform rt = sqr.GetComponent<RectTransform>();

            //attempt to place the item on the surface of the ui
            rt.transform.localPosition = new Vector3(rt.transform.position.x, rt.transform.position.y, 0);

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
