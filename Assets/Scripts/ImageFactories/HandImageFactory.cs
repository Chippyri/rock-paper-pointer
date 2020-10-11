using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Contains images for the hands, in blue and yellow color!
public class HandImageFactory : MonoBehaviour
{
    private static Sprite[] yellowHands = new Sprite[4];
    private static Sprite[] blueHands = new Sprite[4];

    private string[] spriteNames = { "Paper", "Pointer", "Rock", "Question" };
    private const string blueSpriteSuffix = "Blue";
    private const string numberSpritePath = "Sprites/Hands/";

    // Initialize the Sprite arrays
    void Awake()
    {
        // There are 4 different states for the hand graphic
        // The yellow images have no color suffix, but the blue ones are suffixed with "Blue"
        for (int i = 0; i < 4; i++)
        {
            yellowHands[i] = Resources.Load<Sprite>(numberSpritePath + spriteNames[i]);
            blueHands[i] = Resources.Load<Sprite>(numberSpritePath + spriteNames[i] + blueSpriteSuffix);
        }
    }

    // Note: Do not call before awake!
    public static Sprite getHandSprite(HandState handState, bool yellow)
    {
        if (yellow)
        {
            return yellowHands[(int)handState];
        }
        else
        {
            return blueHands[(int)handState];
        }
    }
}
