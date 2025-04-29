using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Rendering;

public class StatBlock
{
    public float currentValue {get; private set;}
    public float lowCap {get; private set;}
    public float highCap {get; private set;}
    public bool reachedLowCap {get => currentValue <= lowCap;}
    public bool reachedHighCap {get => currentValue >= highCap;}
    public StatBlock(float current = 0, float lCap = 0, float hCap = 0)
    {
        currentValue = current;
        lowCap = lCap;
        highCap = hCap;
        UpdateValue(0);
    }
    public float UpdateValue(float incomingValue, bool clamp = true)
    {
        currentValue = clamp ? Mathf.Clamp(currentValue + incomingValue, lowCap, highCap) : currentValue + incomingValue;
        return currentValue;
    }
}
public class MechStats : MonoBehaviour
{
    protected Dictionary<string, StatBlock> m_stats = new Dictionary<string, StatBlock>();
    public StatBlock GetStat(string key)
    {
        return m_stats[key];
    }
    [SerializeField] private string m_statFilePath;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        LoadStats(m_statFilePath);
    }
    public bool LoadStats(string path)
    {
        //Check if valid file path
        if(path != "" && path.EndsWith(".csv"))
        {
            //Check if there even is a file
            if(File.Exists(path))
            {
                //read the file
                var linesOut = File.ReadLines(path);
                //clear the dictionary and save to it
                m_stats = new Dictionary<string, StatBlock>();
                foreach(var line in linesOut)
                {
                    string[] values = line.Split(',');
                    m_stats.Add(values[0], new StatBlock(0, float.Parse(values[1]), float.Parse(values[2])));
                }
                return true;
            }
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
