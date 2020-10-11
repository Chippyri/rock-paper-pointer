using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Which gesture is shown for a hand.
public enum HandState
{
    PAPER, POINTER, ROCK, UNKNOWN
}

public abstract class HandEntity : MonoBehaviour {

    protected Image handImage;
    protected HandState handState;

    public abstract HandState GetState();
    protected abstract void UpdateHandWithState(HandState hs);

    public abstract void InitHand();
}
