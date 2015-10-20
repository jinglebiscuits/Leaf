using UnityEngine;
using System.Collections;

public class LeafLobbyHook : MonoBehaviour {

    public static LeafLobbyHook instance = null;

    /// <summary>
    /// 
    /// </summary>
    void Awake()
    {
        LeafLobbyHook.instance = this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="manager"></param>
    /// <param name="lobbyPlayer"></param>
    /// <param name="gamePlayer"></param>
    public void OnLobbyServerSceneLoadedForPlayer(UnityEngine.Networking.NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        LeafLobbyPlayer leafLobbyPlayer = lobbyPlayer.GetComponent<LeafLobbyPlayer>();

        LeafGamePlayer leafGamePlayer = gamePlayer.GetComponent<LeafGamePlayer>();
        //paddle.number = l.slot;
        //paddle.color = l.playerColor;
        //paddle.playerName = l.playerName;

        //PongManager.AddPlayer(paddle);
    }
}
