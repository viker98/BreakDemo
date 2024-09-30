using UnityEngine;

public enum TeamAttitude
{
    Friendly,
    Enemy,
    Neutral
}

public interface ITeamInterface
{
    static int GetNeutralTeamID()
    {
        return -1;
    }
    public int GetTeamID()
    {
        return GetNeutralTeamID();
    }

    public TeamAttitude GetTeamAttitudeTowards(GameObject other)
    {
        ITeamInterface otherInterface = other.GetComponent<ITeamInterface>();
        if(otherInterface == null || otherInterface.GetTeamID() == GetNeutralTeamID() || GetTeamID() == GetNeutralTeamID())
        {
            return TeamAttitude.Neutral;
        }
        if(otherInterface.GetTeamID() == GetTeamID())
        {
            return TeamAttitude.Friendly;
        }
        return TeamAttitude.Enemy;

    }
}
