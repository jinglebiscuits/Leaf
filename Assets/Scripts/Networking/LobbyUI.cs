//  using UnityEngine;
//  using System.Collections.Generic;
//  using UnityEngine.UI;
//  using UnityEngine.Networking;
//  using System;
//  using System.Collections;

//  public class LobbyUI : MonoBehaviour {

//      public static LobbyUI instace = null;

//      Core.Networking.NetworkDiscovery discovery = null;

//      //select host or client panel
//      public GameObject selectHostOrClientPanel;

//      //host list panel
//      public GameObject hostListPanel;
//      public GameObject hostListContentPanel;
//      public GameObject hostItem;
//      public float hostItemHeight = 600f;

//      public GameObject waitingForPlayersPanel;

//      public GameObject gameLoadingPanel;

//      public float transmitSleepTime = 1f;

//      public int maxLobbyPlayers = 3;

//      private Dictionary<string, GameObject> dctHostItems = new Dictionary<string, GameObject>();
//      private Dictionary<string, GameObject> dctPlayerItems = new Dictionary<string, GameObject>();

//      private GameObject localPlayer;

//      /// <summary>
//      /// 
//      /// </summary>
//      public enum LobbyStatusList
//      {
//          SelectHostOrClient,
//          WaitingForPlayers,
//          HostList,
//          LoadGame
//      }

//      public LobbyStatusList lobbyStatus = LobbyStatusList.SelectHostOrClient;
   
//      /// <summary>
//      /// 
//      /// </summary>
//      void Awake()
//      {
//          LobbyUI.instace = this;
//      }

//      /// <summary>
//      /// 
//      /// </summary>
//      void Start()
//      {
//          discovery = Core.Networking.NetworkDiscovery.instace;
//          discovery.Initialize();
//          lobbyStatus = LobbyStatusList.SelectHostOrClient;
//      }

//      /// <summary>
//      /// 
//      /// </summary>
//      public void HostButtonClickHandler()
//      {
//          Debug.Log("HostButtonClickHandler");
//          discovery.StartAsServer();
//          //Request the player list from the server

//          //Display the player list
//          lobbyStatus = LobbyStatusList.WaitingForPlayers;
//          UpdateUIMode();
        
//      }

//      /// <summary>
//      /// 
//      /// </summary>
//      public void ClientButtonClickHandler()
//      {
//          //listen for available games on the network
//          discovery.StartAsClient();
//          //display the list of games discovered so far
//          lobbyStatus = LobbyStatusList.HostList;
//          UpdateUIMode();
//      }

//      /// <summary>
//      /// 
//      /// </summary>
//      public void HostSelected(string strIPAddress, int intPort)
//      {
//          System.Net.IPAddress ip = System.Net.IPAddress.Parse(strIPAddress);
//          //if (NetworkController.instance.StartClient(ip, intPort))
//          //{
//          //    //If connect is successful, then we want to turn discovery off
//          //    discovery.running = false;


//          //}

//          Debug.Log(string.Format("ip {0}  port {1}", strIPAddress, intPort));

//          if (NetManager.singleton == null)
//          {
//              Debug.Log("NetManager.singleton is null");
//          }
//          else
//          {
//              NetManager.singleton.networkAddress = strIPAddress;
//              NetManager.singleton.networkPort = intPort;
//              NetManager.singleton.StartClient();

//              lobbyStatus = LobbyStatusList.LoadGame;
//              UpdateUIMode();
//          }
//      }


//      public void LoadGame()
//      {

//      }

//      /// <summary>
//      /// 
//      /// </summary>
//      void Update()
//      {
        
//          if (lobbyStatus == LobbyStatusList.HostList) ListHosts();
//          if (lobbyStatus == LobbyStatusList.LoadGame) LoadGame();
//      }

//      /// <summary>
//      /// 
//      /// </summary>
//      void UpdateUIMode()
//      {
//          if (lobbyStatus == LobbyStatusList.SelectHostOrClient)
//          {
//              disableAllPanels();
//              selectHostOrClientPanel.SetActive(true);
//          }
//          else if (lobbyStatus == LobbyStatusList.HostList)
//          {
//              disableAllPanels();
//              hostListPanel.SetActive(true);
//          }
//          else if (lobbyStatus == LobbyStatusList.LoadGame)
//          {
//              disableAllPanels();
//              gameLoadingPanel.SetActive(true);
//          }
//          else if (lobbyStatus == LobbyStatusList.WaitingForPlayers)
//          {
//              disableAllPanels();
//              waitingForPlayersPanel.SetActive(true);
//          }

//      }

//      void disableAllPanels()
//      {
//          selectHostOrClientPanel.SetActive(false);
//          hostListPanel.SetActive(false);
//          gameLoadingPanel.SetActive(false);
//          waitingForPlayersPanel.SetActive(false);
//      }

//      /// <summary>
//      /// 
//      /// </summary>
//      void ListHosts()
//      {
//          string strIpAddress = string.Empty;
//          int intPort = 0;
//          int intItemRow = 0;
//          if (discovery.broadcastsReceived != null)
//          {
//              foreach (string key in discovery.broadcastsReceived.Keys)
//              {
//                  NetworkBroadcastResult item = discovery.broadcastsReceived[key];
//                  if (NetworkUtils.GetSocketInfoFromNetworkBroadcastResult(ref item, ref strIpAddress, ref intPort))
//                  {
//                      //check to see if this item is in the host list
//                      if (!this.dctHostItems.ContainsKey(strIpAddress))
//                      {
//                          Debug.Log("LobbyUI adding host item " + strIpAddress);
//                          GameObject hostItemInstance = Instantiate(hostItem, hostListContentPanel.transform.position, Quaternion.identity) as GameObject;
//                          hostItemInstance.transform.SetParent(hostListContentPanel.transform);
//                          RectTransform rt = hostItemInstance.GetComponent<RectTransform>();

//                          //attempt to place the item on the surface of the ui
//                          rt.transform.localPosition = new Vector3(rt.transform.position.x, rt.transform.position.y, 0);

//                          LobbyHostItem lobbyHostItem = hostItemInstance.GetComponentInChildren<LobbyHostItem>();
//                          lobbyHostItem.Init(strIpAddress, intPort);

//                          //add this to the list to make sure we don't select it again.
//                          this.dctHostItems.Add(strIpAddress, hostItemInstance);
//                      }
//                  }
//                  intItemRow++;

//              }

//          }
//      }

//  }
