using UnityEngine;

public class TimerTracker : SimpleStatTracker
{
    [Tooltip("The rate (in seconds) at which the target value will increase.")]
    [SerializeField] protected float m_rate = 1;

    protected virtual void Update()
    {
        m_targetBlock.UpdateValue(Time.deltaTime * m_rate);
        foreach(var tracker in GetComponents<SimpleStatTracker>())
        {
            tracker.CheckStat();
        }
    }
}
