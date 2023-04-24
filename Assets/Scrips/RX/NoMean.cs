using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct NoMean
{
    private NoMean(bool b = true){}
    public static NoMean Default => new NoMean();
}
