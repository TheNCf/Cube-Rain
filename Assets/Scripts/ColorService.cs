using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorService
{
    public Color PickRandom()
    {
        return new Color(Random.value, Random.value, Random.value);
    }
}
