using UnityEngine;

public class MechStatTester : MechStats
{
    [SerializeField] GameObject BasicStatAffectObject;
    [SerializeField] Transform Parent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();

        foreach(var entry in m_stats)
        {
            GameObject newO = Instantiate(BasicStatAffectObject);
            newO.GetComponent<SimpleStatTracker>().GiveStat(entry.Value);
            newO.transform.SetParent(Parent, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
