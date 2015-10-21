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
        Debug.Log("Lobby Hook - OnLobbyServerSceneLoadedForPlayer()");
        LeafLobbyPlayer leafLobbyPlayer = lobbyPlayer.GetComponent<LeafLobbyPlayer>();

        LeafGamePlayer leafGamePlayer = gamePlayer.GetComponent<LeafGamePlayer>();
        leafGamePlayer.playerColor = leafLobbyPlayer.playerColor;

    }
}
