using UnityEngine;
using UnityEditor.UIElements;
using UnityEditor.Experimental.GraphView;
using Graph.Field;

[SearchAttributeName("Variable/Float")]
public class FloatNode : MyNode
{
    private FloatField floatField;
    public float value { get { return floatField.value; } }

    public FloatNode() : base()
    {
        title = "Float";

        var outputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
        outputPort.portName = "float";
        outputContainer.Add(outputPort);

        floatField = new FloatField();
        mainContainer.Add(floatField);
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