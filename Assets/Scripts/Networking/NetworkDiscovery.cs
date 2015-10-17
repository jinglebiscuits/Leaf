//  using System;
//  using System.Collections.Generic;
//  using UnityEngine;
//  using UnityEngine.Networking;

//  namespace Core.Networking
//  {
//      /// <summary>
//      ///   <para>The NetworkDiscovery component allows Unity games to find each other on a local network. It can broadcast presence and listen for broadcasts, and optionally join matching games using the NetworkManager.</para>
//      /// </summary>
//      [DisallowMultipleComponent]
//      public class NetworkDiscovery : MonoBehaviour
//      {
//          public static NetworkDiscovery instace = null;

//          private const int k_MaxBroadcastMsgSize = 1024;

//          [SerializeField]
//          private int m_BroadcastPort = 47777;

//          [SerializeField]
//          private int m_BroadcastKey = 2222;

//          [SerializeField]
//          private int m_BroadcastVersion = 1;

//          [SerializeField]
//          private int m_BroadcastSubVersion = 1;

//          [SerializeField]
//          private int m_BroadcastInterval = 1000;

//          [SerializeField]
//          private bool m_UseNetworkManager = true;

//          [SerializeField]
//          private string m_BroadcastData = "HELLO";

//          [SerializeField]
//          private bool m_ShowGUI = true;

//          [SerializeField]
//          private int m_OffsetX;

//          [SerializeField]
//          private int m_OffsetY;

//          private int m_HostId = -1;

//          private bool m_Running;

//          private bool m_IsServer;

//          private bool m_IsClient;

//          private byte[] m_MsgOutBuffer;

//          private byte[] m_MsgInBuffer;

//          private HostTopology m_DefaultTopology;

//          private Dictionary<string, NetworkBroadcastResult> m_BroadcastsReceived;

//          /// <summary>
//          ///   <para>The data to include in the broadcast message when running as a server.</para>
//          /// </summary>
//          public string broadcastData
//          {
//              get
//              {
//                  return this.m_BroadcastData;
//              }
//              set
//              {
//                  this.m_BroadcastData = value;
//              }
//          }

//          /// <summary>
//          ///   <para>How often in milliseconds to broadcast when running as a server.</para>
//          /// </summary>
//          public int broadcastInterval
//          {
//              get
//              {
//                  return this.m_BroadcastInterval;
//              }
//              set
//              {
//                  this.m_BroadcastInterval = value;
//              }
//          }

//          /// <summary>
//          ///   <para>A key to identify this application in broadcasts.</para>
//          /// </summary>
//          public int broadcastKey
//          {
//              get
//              {
//                  return this.m_BroadcastKey;
//              }
//              set
//              {
//                  this.m_BroadcastKey = value;
//              }
//          }

//          /// <summary>
//          ///   <para>The network port to broadcast on and listen to.</para>
//          /// </summary>
//          public int broadcastPort
//          {
//              get
//              {
//                  return this.m_BroadcastPort;
//              }
//              set
//              {
//                  this.m_BroadcastPort = value;
//              }
//          }

//          /// <summary>
//          ///   <para>A dictionary of broadcasts received from servers.</para>
//          /// </summary>
//          public Dictionary<string, NetworkBroadcastResult> broadcastsReceived
//          {
//              get
//              {
//                  return this.m_BroadcastsReceived;
//              }
//          }

//          /// <summary>
//          ///   <para>The sub-version of the application to broadcast. This is used to match versions of the same application.</para>
//          /// </summary>
//          public int broadcastSubVersion
//          {
//              get
//              {
//                  return this.m_BroadcastSubVersion;
//              }
//              set
//              {
//                  this.m_BroadcastSubVersion = value;
//              }
//          }

//          /// <summary>
//          ///   <para>The version of the application to broadcast. This is used to match versions of the same application.</para>
//          /// </summary>
//          public int broadcastVersion
//          {
//              get
//              {
//                  return this.m_BroadcastVersion;
//              }
//              set
//              {
//                  this.m_BroadcastVersion = value;
//              }
//          }

//          /// <summary>
//          ///   <para>The TransportLayer hostId being used (read-only).</para>
//          /// </summary>
//          public int hostId
//          {
//              get
//              {
//                  return this.m_HostId;
//              }
//              set
//              {
//                  this.m_HostId = value;
//              }
//          }

//          /// <summary>
//          ///   <para>True if running in client mode (read-only).</para>
//          /// </summary>
//          public bool isClient
//          {
//              get
//              {
//                  return this.m_IsClient;
//              }
//              set
//              {
//                  this.m_IsClient = value;
//              }
//          }

//          /// <summary>
//          ///   <para>True if running in server mode (read-only).</para>
//          /// </summary>
//          public bool isServer
//          {
//              get
//              {
//                  return this.m_IsServer;
//              }
//              set
//              {
//                  this.m_IsServer = value;
//              }
//          }

//          /// <summary>
//          ///   <para>The horizontal offset of the GUI if active.</para>
//          /// </summary>
//          public int offsetX
//          {
//              get
//              {
//                  return this.m_OffsetX;
//              }
//              set
//              {
//                  this.m_OffsetX = value;
//              }
//          }

//          /// <summary>
//          ///   <para>The vertical offset of the GUI if active.</para>
//          /// </summary>
//          public int offsetY
//          {
//              get
//              {
//                  return this.m_OffsetY;
//              }
//              set
//              {
//                  this.m_OffsetY = value;
//              }
//          }

//          /// <summary>
//          ///   <para>True is broadcasting or listening (read-only).</para>
//          /// </summary>
//          public bool running
//          {
//              get
//              {
//                  return this.m_Running;
//              }
//              set
//              {
//                  this.m_Running = value;
//              }
//          }

//          /// <summary>
//          ///   <para>True to draw the default Broacast control UI.</para>
//          /// </summary>
//          public bool showGUI
//          {
//              get
//              {
//                  return this.m_ShowGUI;
//              }
//              set
//              {
//                  this.m_ShowGUI = value;
//              }
//          }

//          /// <summary>
//          ///   <para>True to integrate with the NetworkManager.</para>
//          /// </summary>
//          public bool useNetworkManager
//          {
//              get
//              {
//                  return this.m_UseNetworkManager;
//              }
//              set
//              {
//                  this.m_UseNetworkManager = value;
//              }
//          }

//          public NetworkDiscovery()
//          {
//          }

//          private static string BytesToString(byte[] bytes)
//          {
//              char[] chrArray = new char[(int)bytes.Length / 2];
//              Buffer.BlockCopy(bytes, 0, chrArray, 0, (int)bytes.Length);
//              return new string(chrArray);
//          }

//          /// <summary>
//          /// 
//          /// </summary>
//          void Awake()
//          {
//              NetworkDiscovery.instace = this;
//          }

//          /// <summary>
//          ///   <para>Initializes the NetworkDiscovery component.</para>
//          /// </summary>
//          /// <returns>
//          ///   <para>Return true if the network port was available.</para>
//          /// </returns>
//          public bool Initialize()
//          {
//              if (this.m_BroadcastData.Length >= 1024)
//              {
//                  if (LogFilter.logError)
//                  {
//                      Debug.LogError(string.Concat("NetworkDiscovery Initialize - data too large. max is ", 1024));
//                  }
//                  return false;
//              }
//              if (!NetworkTransport.IsStarted)
//              {
//                  NetworkTransport.Init();
//              }
//              if (this.m_UseNetworkManager && NetManager.singleton != null)
//              {
//                  this.m_BroadcastData = string.Concat(new object[] { "CoreNetMgr:", NetManager.singleton.networkPort, ":", NetManager.singleton.networkPort});
//                  if (LogFilter.logInfo)
//                  {
//                      Debug.Log(string.Concat("NetworkDiscovery set broadbast data to:", this.m_BroadcastData));
//                  }
//              }
//              this.m_MsgOutBuffer = NetworkDiscovery.StringToBytes(this.m_BroadcastData);
//              this.m_MsgInBuffer = new byte[1024];
//              this.m_BroadcastsReceived = new Dictionary<string, NetworkBroadcastResult>();
//              ConnectionConfig connectionConfig = new ConnectionConfig();
//              connectionConfig.AddChannel(QosType.Unreliable);
//              this.m_DefaultTopology = new HostTopology(connectionConfig, 1);
//              if (this.m_IsServer)
//              {
//                  this.StartAsServer();
//              }
//              if (this.m_IsClient)
//              {
//                  this.StartAsClient();
//              }
//              return true;
//          }

//          private void OnGUI()
//          {
//              if (!this.m_ShowGUI)
//              {
//                  return;
//              }
//              int mOffsetX = 10 + this.m_OffsetX;
//              int mOffsetY = 40 + this.m_OffsetY;
//              if (this.m_MsgInBuffer == null)
//              {
//                  if (GUI.Button(new Rect((float)mOffsetX, (float)mOffsetY, 200f, 20f), "Initialize Broadcast"))
//                  {
//                      this.Initialize();
//                  }
//                  return;
//              }
//              string empty = string.Empty;
//              if (this.m_IsServer)
//              {
//                  empty = " (server)";
//              }
//              if (this.m_IsClient)
//              {
//                  empty = " (client)";
//              }
//              GUI.Label(new Rect((float)mOffsetX, (float)mOffsetY, 200f, 20f), string.Concat("initialized", empty));
//              mOffsetY = mOffsetY + 24;
//              if (!this.m_Running)
//              {
//                  if (GUI.Button(new Rect((float)mOffsetX, (float)mOffsetY, 200f, 20f), "Start Broadcasting"))
//                  {
//                      this.StartAsServer();
//                  }
//                  mOffsetY = mOffsetY + 24;
//                  if (GUI.Button(new Rect((float)mOffsetX, (float)mOffsetY, 200f, 20f), "Listen for Broadcast"))
//                  {
//                      this.StartAsClient();
//                  }
//                  mOffsetY = mOffsetY + 24;
//              }
//              else
//              {
//                  if (GUI.Button(new Rect((float)mOffsetX, (float)mOffsetY, 200f, 20f), "Stop"))
//                  {
//                      this.StopBroadcast();
//                  }
//                  mOffsetY = mOffsetY + 24;
//                  if (this.m_BroadcastsReceived != null)
//                  {
//                      foreach (string key in this.m_BroadcastsReceived.Keys)
//                      {
//                          NetworkBroadcastResult item = this.m_BroadcastsReceived[key];
//                          if (GUI.Button(new Rect((float)mOffsetX, (float)(mOffsetY + 20), 200f, 20f), string.Concat("Game at ", key)) && this.m_UseNetworkManager)
//                          {
//                              string str = NetworkDiscovery.BytesToString(item.broadcastData);
//                              string[] strArrays = str.Split(new char[] { ':' });
//                              Debug.Log(string.Format("connecting to server address {0}  broadcast data {1} array length {2}", item.serverAddress, str, strArrays.Length));

//                              //We will be using a custom network manager
//                              //if ((int)strArrays.Length == 3 && strArrays[0] == "NetworkManager" && NetworkManager.singleton != null && NetworkManager.singleton.client == null)
//                              //{
//                              //    NetworkManager.singleton.networkAddress = strArrays[1];
//                              //    NetworkManager.singleton.networkPort = Convert.ToInt32(strArrays[2]);
//                              //    NetworkManager.singleton.StartClient();
//                              //}
//                              if ((int)strArrays.Length == 3)
//                              {
//                                  string strIpAddress = NetworkUtils.ParseIPFromNetworkBroadcastResult( item.serverAddress);
//                                  int intPort = Convert.ToInt32(strArrays[2]);

//                                  NetworkManager.singleton.networkAddress = strIpAddress;
//                                  NetworkManager.singleton.networkPort = intPort;
//                                  NetworkManager.singleton.StartClient();
//                                  //If connect is successful, then we want to turn running off
//                                  this.running = false;
//                              }
//                          }
//                          mOffsetY = mOffsetY + 24;
//                      }
//                  }
//              }
//          }

//          /// <summary>
//          ///   <para>This is a virtual function that can be implemented to handle broadcast messages when running as a client.</para>
//          /// </summary>
//          /// <param name="fromAddress">The IP address of the server.</param>
//          /// <param name="data">The data broadcast by the server.</param>
//          public virtual void OnReceivedBroadcast(string fromAddress, string data)
//          {
//          }

//          /// <summary>
//          ///   <para>Starts listening for broadcasts messages.</para>
//          /// </summary>
//          /// <returns>
//          ///   <para>True is able to listen.</para>
//          /// </returns>
//          public bool StartAsClient()
//          {
//              byte num;
//              if (this.m_HostId != -1 || this.m_Running)
//              {
//                  if (LogFilter.logWarn)
//                  {
//                      Debug.LogWarning("NetworkDiscovery StartAsClient already started");
//                  }
//                  return false;
//              }
//              this.m_HostId = NetworkTransport.AddHost(this.m_DefaultTopology, this.m_BroadcastPort);
//              if (this.m_HostId == -1)
//              {
//                  if (LogFilter.logError)
//                  {
//                      Debug.LogError("NetworkDiscovery StartAsClient - addHost failed");
//                  }
//                  return false;
//              }

//              Debug.Log("Client Start Listening");
//              NetworkTransport.SetBroadcastCredentials(this.m_HostId, this.m_BroadcastKey, this.m_BroadcastVersion, this.m_BroadcastSubVersion, out num);
//              this.m_Running = true;
//              this.m_IsClient = true;
//              if (LogFilter.logDebug)
//              {
//                  Debug.Log("StartAsClient Discovery listening");
//              }
//              return true;
//          }

//          /// <summary>
//          ///   <para>Starts sending broadcast messages.</para>
//          /// </summary>
//          /// <returns>
//          ///   <para>True is able to broadcast.</para>
//          /// </returns>
//          public bool StartAsServer()
//          {
//              byte num;
//              if (this.m_HostId != -1 || this.m_Running)
//              {
//                  if (LogFilter.logWarn)
//                  {
//                      Debug.LogWarning("NetworkDiscovery StartAsServer already started");
//                  }
//                  return false;
//              }
//              this.m_HostId = NetworkTransport.AddHost(this.m_DefaultTopology, 0);
//              if (this.m_HostId == -1)
//              {
//                  if (LogFilter.logError)
//                  {
//                      Debug.LogError("NetworkDiscovery StartAsServer - addHost failed");
//                  }
//                  return false;
//              }

//              Debug.Log("Server Start Broadcasting");
//              //Start broadcasting the IP and port of our game to the clients
//              if (!NetworkTransport.StartBroadcastDiscovery(this.m_HostId, this.m_BroadcastPort, this.m_BroadcastKey, this.m_BroadcastVersion, this.m_BroadcastSubVersion, this.m_MsgOutBuffer, (int)this.m_MsgOutBuffer.Length, this.m_BroadcastInterval, out num))
//              {
//                  if (LogFilter.logError)
//                  {
//                      Debug.LogError(string.Concat("NetworkDiscovery StartBroadcast failed err: ", num));
//                  }
//                  return false;
//              }

//              //Now that our server info is being broadcasted out over the network, we should start listening for connections
//              NetManager.singleton.StartServer();

//              this.m_Running = true;
//              this.m_IsServer = true;
//              if (LogFilter.logDebug)
//              {
//                  Debug.Log("StartAsServer Discovery broadcasting");
//              }
//              UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
//              return true;
//          }

//          /// <summary>
//          ///   <para>Stops listening and broadcasting.</para>
//          /// </summary>
//          public void StopBroadcast()
//          {
//              if (this.m_HostId == -1)
//              {
//                  if (LogFilter.logError)
//                  {
//                      Debug.LogError("NetworkDiscovery StopBroadcast not initialized");
//                  }
//                  return;
//              }
//              if (!this.m_Running)
//              {
//                  Debug.LogWarning("NetworkDiscovery StopBroadcast not started");
//                  return;
//              }
//              if (this.m_IsServer)
//              {
//                  NetworkTransport.StopBroadcastDiscovery();
//              }
//              NetworkTransport.RemoveHost(this.m_HostId);
//              this.m_HostId = -1;
//              this.m_Running = false;
//              this.m_IsServer = false;
//              this.m_IsClient = false;
//              this.m_MsgInBuffer = null;
//              this.m_BroadcastsReceived = null;
//              if (LogFilter.logDebug)
//              {
//                  Debug.Log("Stopped Discovery broadcasting");
//              }
//          }

//          private static byte[] StringToBytes(string str)
//          {
//              byte[] numArray = new byte[str.Length * 2];
//              Buffer.BlockCopy(str.ToCharArray(), 0, numArray, 0, (int)numArray.Length);
//              return numArray;
//          }

//          private void Update()
//          {
//              NetworkEventType networkEventType;
//              int intConnectionId;
//              int intChannelId;
//              int intRecievedSize;
//              byte bytError;
//              string strIpAddress;
//              int intPort;
//              if (!this.running)
//              {
//                  return;
//              }
//              if (this.m_HostId == -1)
//              {
//                  return;
//              }
//              if (this.m_IsServer)
//              {
//                  return;
//              }
//              do
//              {
//                  networkEventType = NetworkTransport.ReceiveFromHost(this.m_HostId, out intConnectionId, out intChannelId, this.m_MsgInBuffer, 1024, out intRecievedSize, out bytError);
//                  if (networkEventType != NetworkEventType.BroadcastEvent)
//                  {
//                      continue;
//                  }
//                  NetworkTransport.GetBroadcastConnectionMessage(this.m_HostId, this.m_MsgInBuffer, 1024, out intRecievedSize, out bytError);
//                  NetworkTransport.GetBroadcastConnectionInfo(this.m_HostId, out strIpAddress, out intPort, out bytError);
//                  NetworkBroadcastResult networkBroadcastResult = new NetworkBroadcastResult()
//                  {
//                      serverAddress = strIpAddress,
//                      broadcastData = new byte[intRecievedSize]
//                  };
//                  Buffer.BlockCopy(this.m_MsgInBuffer, 0, networkBroadcastResult.broadcastData, 0, intRecievedSize);
//                  this.m_BroadcastsReceived[strIpAddress] = networkBroadcastResult;
//                  this.OnReceivedBroadcast(strIpAddress, NetworkDiscovery.BytesToString(this.m_MsgInBuffer));
//              }
//              while (networkEventType != NetworkEventType.Nothing);
//          }
//      }
//  }