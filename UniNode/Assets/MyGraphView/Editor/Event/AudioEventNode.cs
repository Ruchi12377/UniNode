using System;
using System.Linq;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEditor.Experimental.GraphView;
using Graph.Field;

[SearchAttributeName("Audio/AudioEventNode")]
public class AudioEventNode : ProcessNode
{
    private Port inputClip;
    private Port inputSource;

    private EnumField enumField;
    public int value { get { return Fields.returnValue(Convert.ToInt32(enumField.value)); } }

    enum EventType
    {
        Play,
        Stop,
        Pause,
        UnPause,
        Loop,
    };

    public AudioEventNode() : base()
    {
        title = "AudioEvent";

        inputClip = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(AudioClip));
        inputContainer.Add(inputClip);

        inputSource = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(AudioSource));
        inputContainer.Add(inputSource);

        enumField = new EnumField();
        EventType eventType = EventType.Play;
        enumField.Init(eventType);
        mainContainer.Add(enumField);
    }

    public override void Execute()
    {
        AudioClip audioClip = null;
        AudioSource audioSource = null;
        if (inputClip.connections.FirstOrDefault() != null)
        {
            Edge Clip_edge = inputClip.connections.FirstOrDefault();
            var Clip_node = Clip_edge.output.node as AudioClipNode;
            audioClip = Clip_node.value;
        }

        if (inputSource.connections.FirstOrDefault() != null)
        {
            Edge Source_edge = inputSource.connections.FirstOrDefault();
            var Source_node = Source_edge.output.node as AudioSourceNode;
            audioSource = Source_node.value;
        }

        if (audioClip != null && audioSource != null)
        {
            audioSource.clip = audioClip;
            Play(audioSource, audioClip, value);
        }
    }

    private static void Play(AudioSource source, AudioClip clip, int state)
    {
        source.clip = clip;

        source.playOnAwake = false;
        source.mute = false;
        source.loop = false;

        if (state == 0)
        {
            source.Play();
        }
        if (state == 1)
        {
            source.Stop();
        }
        if (state == 2)
        {
            source.Pause();
        }
        if (state == 3)
        {
            source.UnPause();
        }
        if (state == 4)
        {
            source.loop = true;
            source.Play();
        }
    }
}