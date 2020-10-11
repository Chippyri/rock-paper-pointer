using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Class that contains logic for the player hand.
// Inspired by the Spooky-example!

public class PlayerHand : HandEntity
{ 
    // Keeps record of last seen "valid" state
    // However, if no valid state was seen since last win-evaluation, it is UNKNOWN
    private HandState lastSeenState; 

    // Start is called before the first frame update
    void OnEnable()
    {
        InitHand();
    }

    void OnDisable()
    {
        ManomotionManager.OnManoMotionFrameProcessed -= HandleManoMotionFrameUpdated;
        StopAllCoroutines();
    }

    public override void InitHand()
    {
        handState = HandState.UNKNOWN;
        lastSeenState = HandState.UNKNOWN;
        handImage = GetComponent<Image>();
        handImage.preserveAspect = true;
        ManomotionManager.OnManoMotionFrameProcessed += HandleManoMotionFrameUpdated;
    }

    public override HandState GetState()
    {
        return lastSeenState;
    }

    protected override void UpdateHandWithState(HandState hs)
    {
        handState = hs;
        lastSeenState = hs;
        UpdateHandSprite(hs);
    }

    public void UpdateHandSprite(HandState hs)
    {
        handImage.sprite = HandImageFactory.getHandSprite(hs, true);
    }

    HandState GetStateForGesture(ManoGestureContinuous gesture)
    {
        switch (gesture)
        {
            case ManoGestureContinuous.OPEN_HAND_GESTURE:
                return HandState.PAPER;
            case ManoGestureContinuous.POINTER_GESTURE:
                return HandState.POINTER;
            case ManoGestureContinuous.CLOSED_HAND_GESTURE:
                return HandState.ROCK;
            default:
                return HandState.UNKNOWN;
        }
    }

    void HandleManoMotionFrameUpdated()
    {
        GestureInfo gesture = ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info;
        Warning warning = ManomotionManager.Instance.Hand_infos[0].hand_info.warning;
        UpdateStateForGesture(gesture, warning);
    }

	void UpdateStateForGesture(GestureInfo gesture, Warning warning)
    {
        if (warning != Warning.WARNING_HAND_NOT_FOUND)
        {
            UpdateHandWithState(GetStateForGesture(gesture.mano_gesture_continuous));
        }
        else {
            // I do not want to set the lastSeenState to UNKNOWN,
            // as it is supposed to track the last "valid" gesture for the player.
            handState = HandState.UNKNOWN;
            UpdateHandSprite(HandState.UNKNOWN);
        }
    }

    public void PauseUpdating() {
        StartCoroutine(nameof(StopUpdatingForAWhile));
    }

    // Stops checking for the player's gesture and instead displays the last "valid"
    // gesture for a while when showing the evaluation for who won the current round.
    // This is because it can be confusing for the user if the hand keeps updating
    // and/or displaying unknown when the evaluation is being made.
    IEnumerator StopUpdatingForAWhile()
    {
        ManomotionManager.OnManoMotionFrameProcessed -= HandleManoMotionFrameUpdated;
        UpdateHandSprite(lastSeenState);

        yield return new WaitForSeconds(2);

        ManomotionManager.OnManoMotionFrameProcessed += HandleManoMotionFrameUpdated;
        lastSeenState = HandState.UNKNOWN;
    }
}
