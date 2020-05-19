using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEditor.UIElements;
public class RootNode : MyNode
{
    private ObjectField RootObjectField;
    public GameObject RootObject { get { return RootObjectField.value as GameObject; } }

    public Port OutputPort;
    public RootNode() : base()
    {
        title = "Root";

        capabilities -= Capabilities.Deletable;

        OutputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(Port));
        OutputPort.portName = "Out";
        outputContainer.Add(OutputPort);

        RootObjectField = new ObjectField();
        RootObjectField.objectType = typeof(GameObject);
        mainContainer.Add(RootObjectField);
    }
}
