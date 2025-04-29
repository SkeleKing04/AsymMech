using UnityEngine;

public class VariableTimerTracker : TimerTracker
{
    [System.Serializable]
    private class InfluenceStat
    {
        public string m_key;
        [Tooltip("How much this stat should add to the rate when full.")]
        public float m_weight;
    }
    [SerializeField] private InfluenceStat[] m_influences;

    protected override void Update()
    {
        //get the influence
        float totalInfluence = 0;
        foreach(var stat in m_influences)
        {
            totalInfluence += m_targetMech.Stats.GetStat(stat.m_key).capPercent * stat.m_weight;
        }

        //apply value
        m_targetBlock.UpdateValue(Time.deltaTime * (m_rate + totalInfluence));
        foreach(var tracker in GetComponents<SimpleStatTracker>())
        {
            tracker.CheckStat();
        }
    }
}
