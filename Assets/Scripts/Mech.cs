using System.Collections.Generic;
using UnityEngine;

public class Mech : MonoBehaviour
{
    public static List<Mech> Instances  {get; private set;}
    public int InstIndex {get => GetInstanceIndex(); }
    public MechStats Stats = new();

    void Start()
    {
        Instances.Add(this);
    }
    public int GetInstanceIndex(Mech targetMech = null)
    {
        return Instances.IndexOf(targetMech != null ? targetMech : this);
    }
    void OnDestroy()
    {
        Instances.Remove(this);
    }
}
