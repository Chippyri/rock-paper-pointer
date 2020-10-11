using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Class that contains logic for the enemy hand.
// Could be expanded on with some AI tracking player moves, but the uncertainty of the move chosen could be exciting!
public class EnemyHand : HandEntity
{ 
    public UnityEngine.Events.UnityEvent OnHandShown;

    void OnEnable()
    {
        InitHand();
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator CountdownAndSetRandomState() {
        while (true) {

            // Count down from 3 with sprites
            for (int i = 3; i > 0; i--)
            {
                handImage.sprite = NumberImageFactory.getNumberSprite(i, false);
                yield return new WaitForSeconds(1);
            }

            // Set a random state to the hand
            setRandomHandState();
            yield return new WaitForSeconds(3);
        }
    }

    void setRandomHandState() {
        HandState randomState = (HandState)Random.Range(0, 3);
        UpdateHandWithState(randomState);
        HandShown();
    }

    public override HandState GetState()
    {
        return handState;
    }

    protected override void UpdateHandWithState(HandState hs)
    {
        handState = hs;
        handImage.sprite = HandImageFactory.getHandSprite(hs, false);
    }

    public override void InitHand()
    {
        handState = HandState.UNKNOWN;
        handImage = GetComponent<Image>();
        handImage.preserveAspect = true;
        StartCoroutine(nameof(CountdownAndSetRandomState));
    }

    // Notify that the hand is shown to other objects
    private void HandShown()
    {
        if (OnHandShown != null)
        {
            OnHandShown.Invoke();
        }
    }
}
