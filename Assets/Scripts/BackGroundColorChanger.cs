using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundColorChanger : MonoBehaviour
{
	static List<Color32> colors = new List<Color32>(){
        new Color32(248,210,8,255),
        new Color32(255,209,88,255),
        new Color32(255,190,163,255),
        new Color32(210,255,162,255),
        new Color32(162,255,165,255),
        new Color32(162,255,226,255),
        new Color32(162,237,255,255),
        new Color32(171,162,255,255),
        new Color32(255,162,174,255)
    };

    void Awake()
    {
        gameObject.GetComponent<SpriteRenderer>().color = GenerateColor();
    }

    private static Color32 GenerateColor(){
        
        var random = new System.Random();
        int index = random.Next(colors.Count);
        return colors[index];
    }

}
