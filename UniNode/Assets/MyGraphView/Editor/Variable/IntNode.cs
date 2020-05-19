using UnityEngine;
using UnityEditor.UIElements;
using UnityEditor.Experimental.GraphView;
using Graph.Field;

[SearchAttributeName("Variable/Int")]
public class IntNode : MyNode
{
    private IntegerField intField;
    public int value { get { return intField.value; } }

    public IntNode() : base()
    {
        title = "Int";

        var outputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(int));
        outputContainer.Add(outputPort);

        intField = new IntegerField();
        mainContainer.Add(intField);
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