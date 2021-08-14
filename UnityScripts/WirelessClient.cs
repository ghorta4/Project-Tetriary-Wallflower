using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Guildleader;
using System;
using System.Net;

public class WirelessClient : WirelessCommunicator
{
    //keep track of what kind of data type is read/the associated ID and don't use out of order packets
    public Dictionary<packetType, int> sentDataRecords = new Dictionary<packetType, int> { };

    //this value stores what the latest processed request ID was
    public Dictionary<packetType, int> dataSequencingDictionary = new Dictionary<packetType, int> { };

    public override void Initialize()
    {
        FindVariablePort();
    }

    public void Update()
    {
        lobh.runCleanup();
        while (packets.Count > 0)
        {
            ProcessLatestPacket();
        }
    }

    public override void RecievePacket(IPAddress address, int port, byte[] data)
    {
        packets.Enqueue(DataPacket.GetDataPacket(address, port, data, dataSequencingDictionary));
    }

    public void ProcessLatestPacket()
    {
        DataPacket dp = packets.Dequeue();
        switch (dp.stowedPacketType)
        {
            default:
                ErrorHandler.AddErrorToLog(new Exception("Unhandled packet type: " + dp.stowedPacketType));
                break;
        }
    }
}