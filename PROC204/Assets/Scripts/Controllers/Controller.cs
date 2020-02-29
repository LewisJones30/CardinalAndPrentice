using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    public bool IsEnabled { get; set; } = true;
    abstract protected void FindPlayer();
}
