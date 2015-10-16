﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;


public class NetManager : NetworkManager
{
    public static NetManager instance = null;

    void Awake()
    {
        NetManager.instance = this;
    }
}

