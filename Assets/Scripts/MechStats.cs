using UnityEngine;
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
    }
    public float UpdateValue(float incomingValue)
    {
        currentValue = Mathf.Clamp(currentValue + incomingValue, lowCap, highCap);
        return currentValue;
    }
}
[CreateAssetMenu(fileName = "Mech Stats", menuName = "Mech Stuff/Mech Stats")]
public class MechStats : DictionaryAsset<StatBlock>
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
