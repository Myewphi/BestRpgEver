using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractBase : MonoBehaviour
{
    public GameObject player;
    public string debugText;
    public virtual void Interact()
    {
        Debug.Log(debugText);
    }
}
