using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour, ICollisionReceiver
{
    private Animation anim;
    private bool isOpen = false;
    private bool isOpenTowardsFront = false;
    private bool isPlayerInFrontTrigger = false; // Nová proměnná pro sledování hráče v přední oblasti
    private bool isPlayerInBackTrigger = false; // Nová proměnná pro sledování hráče v zadní oblasti

    void Start()
    {
        anim = GetComponent<Animation>();
        // Konfigurace animací zůstává stejná
    }

    void Update()
    {
        // Metoda Update zůstává prázdná
    }

    public void OnTriggerEnterNotif(Collider other, string sender)
    {
        if (sender == "front")
        {
            if (!isOpen)
            {
                anim.Play("GateOpen2");
                isOpenTowardsFront = true;
            }
            isPlayerInFrontTrigger = true;
        }
        else if (sender == "back")
        {
            if (!isOpen)
            {
                anim.Play("GateOpen1");
                isOpenTowardsFront = false;
            }
            isPlayerInBackTrigger = true;
        }

        isOpen = true;
    }

    public void OnTriggerExitNotif(Collider other, string sender)
    {
        if (sender == "front")
        {
            isPlayerInFrontTrigger = false;
        }
        else if (sender == "back")
        {
            isPlayerInBackTrigger = false;
        }

        if (!isPlayerInFrontTrigger && !isPlayerInBackTrigger)
        {
            if (isOpenTowardsFront)
            {
                anim.Play("GateClose2");
            }
            else if (!isOpenTowardsFront)
            {
                anim.Play("GateClose1");
            }
            isOpen = false;
        }
    }

    public void OnTriggerStayNotif(Collider other, string sender)
    {
        // Stays unimplemented
    }
}