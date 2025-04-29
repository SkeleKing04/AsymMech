using System.Collections.Generic;
using UnityEngine;

public class Mech : MonoBehaviour
{
    public static List<Mech> Instances  {get; private set;}
    public static void AddInstance(Mech mech)
    {
        if(Instances == null)
            Instances = new();

        Instances.Add(mech);
    }
    public int InstIndex {get => GetInstanceIndex(); }
    public MechStats Stats = new();
    [SerializeField] string m_statFile;

    void Awake()
    {
        AddInstance(this);
        Stats.LoadStats(m_statFile);
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
