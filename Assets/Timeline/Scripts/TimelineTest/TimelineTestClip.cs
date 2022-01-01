using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class TimelineTestClip : PlayableAsset, ITimelineClipAsset
{
    public TimelineTestBehaviour template = new TimelineTestBehaviour ();

    public float test;

    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<TimelineTestBehaviour>.Create (graph, template);
        TimelineTestBehaviour clone = playable.GetBehaviour ();
        return playable;
    }
}
