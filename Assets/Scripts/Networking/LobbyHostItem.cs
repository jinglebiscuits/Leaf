//  using UnityEngine;
//  using UnityEngine.UI;
//  using System.Collections;

//  public class LobbyHostItem : MonoBehaviour {

//      public string IPAddress;
//      public int Port;

//      /// <summary>
//      /// 
//      /// </summary>
//      /// <param name="strIpAddress"></param>
//      /// <param name="intPort"></param>
//      public void Init(string strIpAddress, int intPort)
//      {
//          this.IPAddress = strIpAddress;
//          this.Port = intPort;
//          //get the text box, there is only one
//          Text txt = GetComponentInChildren<Text>();
//          //set it to the ip address
//          txt.text = strIpAddress;
//      }

//      /// <summary>
//      /// 
//      /// </summary>
//      public void SelectButtonClickHandler()
//      {
//          LobbyUI.instace.HostSelected(IPAddress, Port);
//      }
//  }
