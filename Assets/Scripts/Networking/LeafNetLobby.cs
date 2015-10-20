using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class LeafNetLobby : NetworkLobbyManager {

    public override void OnLobbyClientAddPlayerFailed()
    {
        base.OnLobbyClientAddPlayerFailed();
        Debug.Log("OnLobbyClientAddPlayerFailed");
    }
    public override void OnLobbyClientConnect(NetworkConnection conn)
    {
        base.OnLobbyClientConnect(conn);
        Debug.Log("OnLobbyClientConnect");
    }

    public override void OnLobbyClientDisconnect(NetworkConnection conn)
    {
        base.OnLobbyClientDisconnect(conn);
        Debug.Log("OnLobbyClientDisconnect");
    }

    public override void OnLobbyClientEnter()
    {
        base.OnLobbyClientEnter();
        Debug.Log("OnLobbyClientEnter");
    }

    public override void OnLobbyClientExit()
    {
        base.OnLobbyClientExit();
        Debug.Log("OnLobbyClientExit");
    }

    public override void OnLobbyClientSceneChanged(NetworkConnection conn)
    {
        base.OnLobbyClientSceneChanged(conn);
        Debug.Log("OnLobbyClientSceneChanged");
    }

    public override void OnLobbyServerConnect(NetworkConnection conn)
    {
        base.OnLobbyServerConnect(conn);
        Debug.Log("OnLobbyServerConnect");
    }

    public override GameObject OnLobbyServerCreateGamePlayer(NetworkConnection conn, short playerControllerId)
    {
        Debug.Log("OnLobbyServerCreateGamePlayer");
        return base.OnLobbyServerCreateGamePlayer(conn, playerControllerId);
    }

    public override GameObject OnLobbyServerCreateLobbyPlayer(NetworkConnection conn, short playerControllerId)
    {
        Debug.Log("OnLobbyServerCreateLobbyPlayer");
        return base.OnLobbyServerCreateLobbyPlayer(conn, playerControllerId);
    }
    public override void OnLobbyServerDisconnect(NetworkConnection conn)
    {
        base.OnLobbyServerDisconnect(conn);
        Debug.Log("OnLobbyServerDisconnect");
    }

    public override void OnLobbyServerPlayerRemoved(NetworkConnection conn, short playerControllerId)
    {
        base.OnLobbyServerPlayerRemoved(conn, playerControllerId);
        Debug.Log("OnLobbyServerPlayerRemoved");
    }
    public override void OnLobbyServerPlayersReady()
    {
        base.OnLobbyServerPlayersReady();
        Debug.Log("OnLobbyServerPlayersReady");
    }

    public override void OnLobbyServerSceneChanged(string sceneName)
    {
        base.OnLobbyServerSceneChanged(sceneName);
        Debug.Log("OnLobbyServerSceneChanged");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="lobbyPlayer"></param>
    /// <param name="gamePlayer"></param>
    /// <returns></returns>
    public override bool OnLobbyServerSceneLoadedForPlayer(GameObject lobbyPlayer, GameObject gamePlayer)
    {
        Debug.Log("OnLobbyServerSceneLoadedForPlayer");
        LeafLobbyHook.instance.OnLobbyServerSceneLoadedForPlayer(this, lobbyPlayer, gamePlayer);

        return base.OnLobbyServerSceneLoadedForPlayer(lobbyPlayer, gamePlayer);
    }

    public override void OnLobbyStartClient(NetworkClient lobbyClient)
    {
        base.OnLobbyStartClient(lobbyClient);
        Debug.Log("OnLobbyStartClient");
    }

    public override void OnLobbyStartHost()
    {
        base.OnLobbyStartHost();
        Debug.Log("OnLobbyStartHost");
    }

    public override void OnLobbyStartServer()
    {
        base.OnLobbyStartServer();
        Debug.Log("OnLobbyStartServer");
    }

    public override void OnLobbyStopClient()
    {
        base.OnLobbyStopClient();
        Debug.Log("OnLobbyStopClient");
    }

    public override void OnLobbyStopHost()
    {
        base.OnLobbyStopHost();
        Debug.Log("OnLobbyStopHost");
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        Debug.Log("OnClientConnect");
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);
        Debug.Log("OnClientDisconnect");
    }

    public override void OnClientError(NetworkConnection conn, int errorCode)
    {
        base.OnClientError(conn, errorCode);
        Debug.Log("OnClientError");
    }

    public override void OnClientNotReady(NetworkConnection conn)
    {
        base.OnClientNotReady(conn);
        Debug.Log("OnClientNotReady");
    }

    public override void OnClientSceneChanged(NetworkConnection conn)
    {
        base.OnClientSceneChanged(conn);
        Debug.Log("OnClientSceneChanged");
    }

    
}
