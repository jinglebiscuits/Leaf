//  using System;
//  using System.Collections.Generic;
//  using System.Linq;
//  using System.Text;
//  using UnityEngine.Networking;
//  using UnityEngine;

//  /// <summary>
//  /// 
//  /// </summary>
//  public class NetworkUtils
//  {
//      /// <summary>
//      /// 
//      /// </summary>
//      /// <param name="bytes"></param>
//      /// <returns></returns>
//      public static string BytesToString(byte[] bytes)
//      {
//          char[] chrArray = new char[(int)bytes.Length / 2];
//          Buffer.BlockCopy(bytes, 0, chrArray, 0, (int)bytes.Length);
//          return new string(chrArray);
//      }

//      /// <summary>
//      /// 
//      /// </summary>
//      /// <param name="str"></param>
//      /// <returns></returns>
//      public static byte[] StringToBytes(string str)
//      {
//          byte[] numArray = new byte[str.Length * 2];
//          Buffer.BlockCopy(str.ToCharArray(), 0, numArray, 0, (int)numArray.Length);
//          return numArray;
//      }

//      /// <summary>
//      /// 
//      /// </summary>
//      /// <param name="strIp"></param>
//      /// <returns></returns>
//      public static string ParseIPFromNetworkBroadcastResult(string strIp)
//      {
//          //the forlat looks like this
//          //::ffff:192.168.56.1
//          //we should split by : and return the last element
//          string[] strParts = strIp.Split(':');
//          string strRetVal = strParts[strParts.Length - 1];
//          return strRetVal;
//      }

//      public static bool GetSocketInfoFromNetworkBroadcastResult(ref NetworkBroadcastResult item, ref string ipAddress, ref int port)
//      {
//          bool blnRetVal = true;
//          string str = NetworkUtils.BytesToString(item.broadcastData);
//          string[] strArrays = str.Split(new char[] { ':' });
//          if ((int)strArrays.Length == 3)
//          {
//              ipAddress = NetworkUtils.ParseIPFromNetworkBroadcastResult(item.serverAddress);
//              port = Convert.ToInt32(strArrays[2]);
//          }
//          else
//          {
//              Debug.Log("GetSocketInfoFromNetworkBroadcastResult - bad data format");
//          }

//          return blnRetVal;
//      }
//  }

