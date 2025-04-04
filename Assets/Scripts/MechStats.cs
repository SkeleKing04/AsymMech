using UnityEngine;

public class StatBlock
{
    public float currentValue {get; private set;}
    public Vector2 capValues {get; private set;}
    public bool reachedLowCap {get => currentValue <= capValues.x;}
    public bool reachedHighCap {get => currentValue >= capValues.y;}
    public float UpdateValue(float incomingValue)
    {
        currentValue = Mathf.Clamp(currentValue + incomingValue, capValues.x, capValues.y);
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
