using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class LeafLobbyPlayer : NetworkLobbyPlayer {

    public Button btnReady; 

 

    /// <summary>
    /// 
    /// </summary>
    public override void OnClientEnterLobby()
    {
        base.OnClientEnterLobby();
        Debug.Log(string.Format("OnClientEnterLobby() - local {0}  - slot {1}", isLocalPlayer.ToString(),  slot.ToString()));
        LobbyUI.instace.AddPlayerToLobby(this);
        if (isLocalPlayer)
        {
            SetupLocalPlayer();
        }
        else
        {
            SetupRemotePlayer();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public override void OnStartClient()
    {
        base.OnStartClient();
        Debug.Log(string.Format("OnStartClient() - local {0}  - slot {1}", isLocalPlayer.ToString(), slot.ToString()));
    }

    /// <summary>
    /// 
    /// </summary>
    void SetupLocalPlayer()
    {
        Debug.Log("SetupLocalPlayer()");
        btnReady.interactable = true;
        OnClientReady(false);
    }

    /// <summary>
    /// 
    /// </summary>
    void SetupRemotePlayer()
    {
        Debug.Log("SetupRemotePlayer");
        btnReady.interactable = false;
    }

    /// <summary>
    /// Fires when the lobby user clicks the Ready button
    /// </summary>
    public void ReadyButtonClickHandler()
    {
        Debug.Log(string.Format("ReadyButtonClickHandler() - local {0}  - slot {1}", isLocalPlayer.ToString(), slot.ToString()));

        //send the ready message back to the server
        SendReadyToBeginMessage();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="readyState"></param>
    public override void OnClientReady(bool readyState)
    {
        Debug.Log(string.Format("OnClientReady() readyState = {0} - local {1}  - slot {2}  ", readyState.ToString(), isLocalPlayer.ToString(), slot.ToString()));
        if (readyState)
        {
            //ChangeReadyButtonColor(TransparentColor);

            Text textComponent = btnReady.transform.GetChild(0).GetComponent<Text>();
            textComponent.text = "READY";
            textComponent.color = Color.green;
            btnReady.interactable = isLocalPlayer;
        }
        else
        {
            //ChangeReadyButtonColor(isLocalPlayer ? JoinColor : NotReadyColor);

            Text textComponent = btnReady.transform.GetChild(0).GetComponent<Text>();
            textComponent.text = isLocalPlayer ? "JOIN" : "...";
            textComponent.color = Color.white;
            btnReady.interactable = isLocalPlayer;
        }
    }

   

}
