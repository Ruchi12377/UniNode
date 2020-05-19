using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using Graph.Math;

public class AddNode : ProcessNode
{
    private Port inputRight;
    private Port inputLeft;

    private FloatField floatField;
    public float value { get { return floatField.value; } }

    public AddNode() : base()
    {
        title = "Addition";
        inputRight = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
        inputContainer.Add(inputRight);
        inputLeft = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(float));
        inputContainer.Add(inputLeft);

        var outputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(float));
        outputContainer.Add(outputPort);
    }

    public override void Execute()
    {
        Edge left_edge;
        FloatNode add_node_l = new FloatNode();
        float left = 0;

        Edge right_edge;
        FloatNode add_node_r = new FloatNode();
        float right = 0;

        if (inputRight.connections.FirstOrDefault() != null)
        {
            left_edge = inputRight.connections.FirstOrDefault();
            add_node_l = left_edge.output.node as FloatNode;
            left = add_node_l.value;
        }
        if (inputLeft.connections.FirstOrDefault() != null)
        {
            right_edge = inputLeft.connections.FirstOrDefault();
            add_node_r = right_edge.output.node as FloatNode;
            right = add_node_r.value;
        }
        floatField = new FloatField();
        floatField.value = Float.Add(left, right);
    }
}

public class BoolNode : MyNode
{
    private PropertyField textField;
    public  object value { get { return textField.userData; } }

    public BoolNode() : base()
    {
        title = "String";

        var outputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(string));
        outputContainer.Add(outputPort);

        textField = new PropertyField();
        textField.label = "test";
        mainContainer.Add(textField);
    }
}