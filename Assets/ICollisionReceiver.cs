using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollisionReceiver
{
    void OnTriggerEnterNotif(Collider other, string sender);
    void OnTriggerStayNotif(Collider other, string sender);
    void OnTriggerExitNotif(Collider other, string sender);
}
