using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    [SerializeField]
    public UnityEngine.Events.UnityEvent OnCanStartGame;

    void OnEnable()
    {
        ManomotionManager.OnManoMotionFrameProcessed += HandleManoMotionFrameUpdated;
    }

    // Set it in a state that it can still function
    private void OnDisable()
    {
        ManomotionManager.OnManoMotionFrameProcessed -= HandleManoMotionFrameUpdated;
    }

    void HandleManoMotionFrameUpdated()
    {
        GestureInfo gesture = ManomotionManager.Instance.Hand_infos[0].hand_info.gesture_info;
        Warning warning = ManomotionManager.Instance.Hand_infos[0].hand_info.warning;

        CanStartGame(gesture, warning);
    }

    void CanStartGame(GestureInfo gesture, Warning warning) {
        // The hand should be visible and have the open hand gesture before starting
        if (warning == Warning.NO_WARNING && gesture.mano_gesture_continuous == ManoGestureContinuous.OPEN_HAND_GESTURE) {
            if (OnCanStartGame != null) {
                OnCanStartGame.Invoke();
            }
        }
    }
}
