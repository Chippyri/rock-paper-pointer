using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

// Contains images for numbers, in blue and yellow color!
public class NumberImageFactory : MonoBehaviour
{
    private static Sprite[] yellowNumbers = new Sprite[10];
    private static Sprite[] blueNumbers = new Sprite[10];
    
    private string[] spriteNames = { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
    private const string blueSpriteSuffix = "Blue";
    private const string numberSpritePath = "Sprites/Numbers/";

    // Initialize the Sprite arrays
    void Awake() {
        // There are 10 numbers, 0-9, as images
        // The yellow images have no color suffix, but the blue ones are suffixed with "Blue"
        for (int i = 0; i < 10; i++) {
            yellowNumbers[i] = Resources.Load<Sprite>(numberSpritePath + spriteNames[i]);
            blueNumbers[i] = Resources.Load<Sprite>(numberSpritePath + spriteNames[i] + blueSpriteSuffix);
        }
    }

    // Note: Do not call before awake!
    public static Sprite getNumberSprite(int number, bool yellow) {
        if (yellow)
        {
            return yellowNumbers[number];
        }
        else {
            return blueNumbers[number];
        }
    }
}
