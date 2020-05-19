using UnityEngine;
using UnityEditor.UIElements;
using UnityEditor.Experimental.GraphView;
using Graph.Field;

[SearchAttributeName("Variable/AudioSource")]
public class AudioSourceNode : MyNode
{
    private ObjectField audioSourceField;
    public AudioSource value { get { return audioSourceField.value as AudioSource; } }

    public AudioSourceNode() : base()
    {
        title = "AudioSource";
        var outputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(AudioSource));
        outputContainer.Add(outputPort);

        audioSourceField = new ObjectField();
        audioSourceField.objectType = typeof(AudioSource);
        mainContainer.Add(audioSourceField);
    }

    public T returnValue<T>(T value)
    {
        if (value.GetType() == typeof(GameObject))
        {
            var gameObject = value as GameObject;
            return gameObject.GetComponent<T>();
        }
        return value;
    }
}