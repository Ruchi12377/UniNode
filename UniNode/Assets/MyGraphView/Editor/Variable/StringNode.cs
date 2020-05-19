using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using Graph.Field;

[SearchAttributeName("Variable/String")]
public class StringNode : MyNode
{
    private TextField textField;//returnValue<string>(textField.value)
    public string value { get { return Fields.returnValue(textField.value); } }

    public StringNode() : base()
    {
        title = "String";

        var outputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(string));
        outputContainer.Add(outputPort);

        textField = new TextField();
        mainContainer.Add(textField);
    }
}