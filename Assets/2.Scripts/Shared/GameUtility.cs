using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameUtility
{
    public static int SortByPos(float posY)
    {
        return Mathf.RoundToInt(posY * -100);
    }
}
