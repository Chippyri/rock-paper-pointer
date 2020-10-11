using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLoseMessage : MonoBehaviour
{
    [SerializeField]
    Image winImage;

    [SerializeField]
    Image loseImage;

    [SerializeField]
    Image beatsImage;

    [SerializeField]
    Image tieImage;

    void OnEnable()
    {
        winImage.enabled = false;
        loseImage.enabled = false;
        tieImage.enabled = false;
        beatsImage.enabled = true;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator ShowWin() {
        beatsImage.enabled = false;
        winImage.enabled = true;
        yield return new WaitForSeconds(2);
        winImage.enabled = false;
        beatsImage.enabled = true;
        yield break;
    }

    IEnumerator ShowLose()
    {
        beatsImage.enabled = false;
        loseImage.enabled = true;
        yield return new WaitForSeconds(2);
        loseImage.enabled = false;
        beatsImage.enabled = true;
        yield break;
    }

    IEnumerator ShowTie()
    {
        beatsImage.enabled = false;
        tieImage.enabled = true;
        yield return new WaitForSeconds(2);
        tieImage.enabled = false;
        beatsImage.enabled = true;
        yield break;
    }

    public void showWin() {
        StartCoroutine(nameof(ShowWin));
    }

    public void showLose()
    {
        StartCoroutine(nameof(ShowLose));
    }

    public void showTie()
    {
        StartCoroutine(nameof(ShowTie));
    }

}
