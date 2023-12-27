using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpawnLocation : MonoBehaviour
{
    public abstract Vector3 GetSpawnPoint();
    public abstract Transform GetParentTransform();
}
