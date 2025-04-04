using System.Collections.Generic;
using UnityEngine;

public class DictionaryAsset<T> : ScriptableObject
{
    public Dictionary<string, T> _dictionary {get; private set; } = new Dictionary<string, T>();
}
