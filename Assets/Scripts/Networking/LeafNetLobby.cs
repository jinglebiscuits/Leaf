using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class LeafNetLobby : NetworkLobbyManager {


    /// <summary>
    /// This event gets fired when the game is leaving the lobby for the game scene.  It is used to 
    /// copy the user's lobby player selections over to the game player object
    /// </summary>
    /// <param name="lobbyPlayer"></param>
    /// <param name="gamePlayer"></param>
    /// <returns></returns>
    public override bool OnLobbyServerSceneLoadedForPlayer(GameObject lobbyPlayer, GameObject gamePlayer)
    {
        Debug.Log("OnLobbyServerSceneLoadedForPlayer");

        LeafLobbyPlayer leafLobbyPlayer = lobbyPlayer.GetComponent<LeafLobbyPlayer>();
        LeafGamePlayer leafGamePlayer = gamePlayer.GetComponent<LeafGamePlayer>();
        leafGamePlayer.playerColor = leafLobbyPlayer.playerColor;

        return base.OnLobbyServerSceneLoadedForPlayer(lobbyPlayer, gamePlayer);
    }

    /// <summary>
    /// this event fires when scene changes on the client
    /// </summary>
    /// <param name="conn"></param>
    public override void OnLobbyClientSceneChanged(NetworkConnection conn)
    {
        base.OnLobbyClientSceneChanged(conn);

        //The game scene has been loaded.  we need to hide the lobby canvas.
        //Note that we cannot just allow the lobby canvas to be destroyed because
        //the UNet system is still using the lobbyPlayer object.
        LobbyUI.instace.setLobbyCanvas(false);
    }

}
