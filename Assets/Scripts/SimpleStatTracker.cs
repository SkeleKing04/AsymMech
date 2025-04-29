using UnityEngine;

public class SimpleStatTracker : MonoBehaviour
{
    public Mech m_targetMech {get; private set;}
    public string m_statKey {get; private set;}
    public StatBlock m_targetBlock {get; private set;}

    protected virtual void Start()
    {
        m_targetMech = GetComponentInParent<Mech>();
    }
    public void GiveMech(Mech mech)
    {
        m_targetMech = mech;
    }
    public bool GetStat(Mech mech, string key)
    {
        m_targetMech = mech;
        m_statKey = key;
        m_targetBlock = m_targetMech.Stats.GetStat(key);
        if(m_targetBlock != null)
        {
            CheckStat();
            return true;
        }
        else return false;
    }
    public bool GetStat(string key)
    {
        if(m_targetMech != null)
            return GetStat(m_targetMech, key);
        return false;
    }

    public virtual void UpdateStat(float value)
    {
        CheckStat();
    }
    protected virtual void CheckStat()
    {

    }
}
