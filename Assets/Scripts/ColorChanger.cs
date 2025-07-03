using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorChanger
{
    public static void RandomColor(Material material)
    {
        material.color = new Color(Random.value, Random.value, Random.value);
    }
}
