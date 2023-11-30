using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    private static TeamManager instance = null;

    [SerializeField]
    private TeamMemberController developer;
    [SerializeField]
    private TeamMemberController designer;
    [SerializeField]
    private TeamMemberController composer;
    [SerializeField]
    private TeamMemberController writer;

    public static TeamManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
    }

    public void AddFeatureToTimes(Feature feature)
    {
        developer.AllocateTime(ConvertTime(feature.developerEffortPercent));
        designer.AllocateTime(ConvertTime(feature.designerEffortPercent));
        composer.AllocateTime(ConvertTime(feature.composerEffortPercent));
        writer.AllocateTime(ConvertTime(feature.writerEffortPercent));
    }

    public bool CanAddFeature(Feature feature)
    {
        bool can = true;
        if (feature.developerEffortPercent > 0)
        {
            can = can && developer.HasRemainingTime(ConvertTime(feature.developerEffortPercent));
        }
        if (feature.designerEffortPercent > 0)
        {
            can = can && designer.HasRemainingTime(ConvertTime(feature.designerEffortPercent));
        }
        if (feature.composerEffortPercent > 0)
        {
            can = can && composer.HasRemainingTime(ConvertTime(feature.composerEffortPercent));
        }
        if (feature.writerEffortPercent > 0)
        {
            can = can && writer.HasRemainingTime(ConvertTime(feature.writerEffortPercent));
        }
        if (!can)
        {
            MusicManager.Instance.PlayError();
        }
        return can;
    }

    public int ConvertTime(int time)
    {
        return (int) Mathf.Floor(time * GameManager.Instance.featureCostMultiplier);
    }

    public void RemoveTime(TeamRole role, int time)
    {
        if (role == TeamRole.DEVELOPER)
        {
            developer.RemoveTime((time));
        }
        if (role == TeamRole.DESIGNER)
        {
            designer.RemoveTime((time));
        }
        if (role == TeamRole.COMPOSER)
        {
            composer.RemoveTime((time));
        }
        if (role == TeamRole.WRITER)
        {
            writer.RemoveTime((time));
        }
    }

    public void RemoveFeatureFromTimes(Feature feature)
    {
        developer.DeallocateTime(ConvertTime(feature.developerEffortPercent));
        designer.DeallocateTime(ConvertTime(feature.designerEffortPercent));
        composer.DeallocateTime(ConvertTime(feature.composerEffortPercent));
        writer.DeallocateTime(ConvertTime(feature.writerEffortPercent));
    }
}
