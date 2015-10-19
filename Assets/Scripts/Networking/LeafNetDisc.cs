using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class LeafNetDisc : NetworkDiscovery {

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fromAddress"></param>
    /// <param name="data"></param>
    public override void OnReceivedBroadcast(string fromAddress, string data)
    {
        base.OnReceivedBroadcast(fromAddress, data);
    }
}
