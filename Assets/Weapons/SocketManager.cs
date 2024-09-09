using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;


public class SocketManager : MonoBehaviour
{

    List<AttachSocket> _attachSockets;

    public bool FindAndAttachToSocket(ISocketInterface socketInterface)
    {
        initSockets();
        foreach (AttachSocket socket in _attachSockets)        
        {
            if (socket.IsForSocket(socketInterface))
            {
                socket.Attach(socketInterface);
                return true;
            }
        }
        return false;
    }

    void initSockets()
    {
        if (_attachSockets != null)
        {
            return;
        }

        _attachSockets = new List<AttachSocket>();
        AttachSocket[] attachSockets = GetComponentsInChildren<AttachSocket>();
        _attachSockets.AddRange(attachSockets);

        
    }
}
