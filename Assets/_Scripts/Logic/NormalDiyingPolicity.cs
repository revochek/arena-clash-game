using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalDiyingPolicity : IDiyingPolicity
{
    public bool Died(int health)
    {
        return health <= 0;
    }
}