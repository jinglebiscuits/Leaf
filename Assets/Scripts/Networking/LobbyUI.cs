using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Collections;

public class LobbyUI : MonoBehaviour {

    public static LobbyUI instace = null;

    LeafNetDisc discovery = null;
    LeafNetLobby lobbyManager = null;

    //select host or client panel
    public GameObject selectHostOrClientPanel;

    //host list panel
    public GameObject hostListPanel;
    public GameObject hostListContentPanel;
    public GameObject hostItem;
    public float hostItemHeight = 600f;

    public GameObject playerListPanel;
    public GameObject playerListContentPanel;
    public GameObject playerItem;

    public GameObject gameLoadingPanel;

    public float transmitSleepTime = 1f;

    public int maxLobbyPlayers = 3;

    private Dictionary<string, GameObject> dctHostItems = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> dctPlayerItems = new Dictionary<string, GameObject>();

    private GameObject localPlayer;

    /// <summary>
    /// 
    /// </summary>
    public enum LobbyStatusList
    {
        SelectHostOrClient,
        HostList,
        PlayerList,
        LoadGame
    }

    public LobbyStatusList lobbyStatus = LobbyStatusList.SelectHostOrClient;
   
    /// <summary>
    /// 
    /// </summary>
    void Awake()
    {
        LobbyUI.instace = this;
    }

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        discovery = GetComponent<LeafNetDisc>();
        lobbyManager = GetComponent<LeafNetLobby>();

        discovery.Initialize();
        lobbyStatus = LobbyStatusList.SelectHostOrClient;
    }

    #region "Host"


    /// <summary>
    /// 
    /// </summary>
    public void HostButtonClickHandler()
    {
        Debug.Log("HostButtonClickHandler");
        //start the hosting service so other players can join
        lobbyManager.StartHost();
        //start advertising the hosting service so other 
        //players can find us
        discovery.StartAsServer();
        //Display the player list so we can see the players
        //who have joined to far
        lobbyStatus = LobbyStatusList.PlayerList;
        UpdateUIMode();
        
    }
    #endregion

    #region "Client"

    /// <summary>
    /// start discovering hosts on the network.
    /// display the host list.
    /// </summary>
    public void ClientButtonClickHandler()
    {
        //listen for available games on the network
        discovery.StartAsClient();
        //display the list of games discovered so far
        lobbyStatus = LobbyStatusList.HostList;
        UpdateUIMode();
    }

    /// <summary>
    /// Get a list of all of the hosts discovered on the network
    /// </summary>
    void ListHosts()
    {
        string strIpAddress = string.Empty;
        int intPort = 0;
        int intItemRow = 0;
        if (discovery.broadcastsReceived != null)
        {
            foreach (string key in discovery.broadcastsReceived.Keys)
            {
                NetworkBroadcastResult item = discovery.broadcastsReceived[key];
                if (NetworkUtils.GetSocketInfoFromNetworkBroadcastResult(ref item, ref strIpAddress, ref intPort))
                {
                    //check to see if this item is in the host list
                    if (!this.dctHostItems.ContainsKey(strIpAddress))
                    {
                        Debug.Log("LobbyUI adding host item " + strIpAddress);
                        GameObject hostItemInstance = Instantiate(hostItem, hostListContentPanel.transform.position, Quaternion.identity) as GameObject;
                        hostItemInstance.transform.SetParent(hostListContentPanel.transform);
                        RectTransform rt = hostItemInstance.GetComponent<RectTransform>();

                        //attempt to place the item on the surface of the ui
                        rt.transform.localPosition = new Vector3(rt.transform.position.x, rt.transform.position.y, 0);

                        LobbyHostItem lobbyHostItem = hostItemInstance.GetComponentInChildren<LobbyHostItem>();
                        lobbyHostItem.Init(strIpAddress, intPort);

                        //add this to the list to make sure we don't select it again.
                        this.dctHostItems.Add(strIpAddress, hostItemInstance);
                    }
                }
                intItemRow++;

            }

        }
    }

    /// <summary>
    /// The user selected a host on the network
    /// </summary>
    public void HostSelected(string strIPAddress, int intPort)
    {
        System.Net.IPAddress ip = System.Net.IPAddress.Parse(strIPAddress);
        Debug.Log(string.Format("ip {0}  port {1}", strIPAddress, intPort));

        lobbyManager.networkAddress = strIPAddress;
        lobbyManager.StartClient();

        lobbyStatus = LobbyStatusList.PlayerList;
        UpdateUIMode();

    }
    #endregion

    /// <summary>
    /// This method gets called by LeafLobbyPlayer.OnClientEnterLobby()
    /// </summary>
    public void AddPlayerToLobby(LeafLobbyPlayer lobbyPlayer)
    {
        GameObject newPlayer = AddPlayerPrefabToLobby();
        //LobbyPlayerItem playerItem = newPlayer.GetComponent<LobbyPlayerItem>();
        //playerItem.SetLocal(true);
        localPlayer = newPlayer;
        //dctPlayerItems.Add(playerItem.playerId, newPlayer);
    }

    /// <summary>
    /// Create a player item, add it to the player list in the lobby, and return a ref
    /// </summary>
    /// <returns></returns>
    private GameObject AddPlayerPrefabToLobby()
    {
        GameObject playerItemInstance = Instantiate(playerItem, playerListContentPanel.transform.position, Quaternion.identity) as GameObject;
        playerItemInstance.transform.SetParent(playerListContentPanel.transform);
        RectTransform rt = playerItemInstance.GetComponent<RectTransform>();

        //attempt to place the item on the surface of the ui
        rt.transform.localPosition = new Vector3(rt.transform.position.x, rt.transform.position.y, 0);

        return playerItemInstance;
    }

    public void LoadGame()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        
        if (lobbyStatus == LobbyStatusList.HostList) ListHosts();
        if (lobbyStatus == LobbyStatusList.LoadGame) LoadGame();
    }

    /// <summary>
    /// Display the UI panel for this mode
    /// </summary>
    void UpdateUIMode()
    {
        if (lobbyStatus == LobbyStatusList.SelectHostOrClient)
        {
            disableAllPanels();
            selectHostOrClientPanel.SetActive(true);
        }
        else if (lobbyStatus == LobbyStatusList.HostList)
        {
            disableAllPanels();
            hostListPanel.SetActive(true);
        }
        else if (lobbyStatus == LobbyStatusList.LoadGame)
        {
            disableAllPanels();
            gameLoadingPanel.SetActive(true);
        }
        else if (lobbyStatus == LobbyStatusList.PlayerList)
        {
            disableAllPanels();
            playerListPanel.SetActive(true);
        }

    }

    void disableAllPanels()
    {
        selectHostOrClientPanel.SetActive(false);
        hostListPanel.SetActive(false);
        gameLoadingPanel.SetActive(false);
        playerListPanel.SetActive(false);
    }



}
