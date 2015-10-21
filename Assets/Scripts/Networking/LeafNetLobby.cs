using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class LeafNetLobby : NetworkLobbyManager {


    /// <summary>
    /// 
    /// </summary>
    /// <param name="lobbyPlayer"></param>
    /// <param name="gamePlayer"></param>
    /// <returns></returns>
    public override bool OnLobbyServerSceneLoadedForPlayer(GameObject lobbyPlayer, GameObject gamePlayer)
    {
        Debug.Log("OnLobbyServerSceneLoadedForPlayer");
        Debug.Log("Calling lobby hook");
        LeafLobbyHook.instance.OnLobbyServerSceneLoadedForPlayer(this, lobbyPlayer, gamePlayer);

        return base.OnLobbyServerSceneLoadedForPlayer(lobbyPlayer, gamePlayer);
    }

    public override void OnLobbyClientSceneChanged(NetworkConnection conn)
    {
        base.OnLobbyClientSceneChanged(conn);
        LobbyUI.instace.setLobbyCanvas(false);
    }

}
