using UnityEngine;
using UnityEditor.UIElements;
using UnityEditor.Experimental.GraphView;
using Graph.Field;

[SearchAttributeName("Variable/AudioClip")]
public class AudioClipNode : MyNode
{
    private ObjectField audioClipField;
    public AudioClip value { get { return Fields.returnValue<object>(audioClipField.value) as AudioClip; } }

    public AudioClipNode() : base()
    {
        title = "AudioClip";

        var outputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(AudioClip));
        outputContainer.Add(outputPort);

        audioClipField = new ObjectField();
        audioClipField.objectType = typeof(AudioClip);
        mainContainer.Add(audioClipField);
    }

}