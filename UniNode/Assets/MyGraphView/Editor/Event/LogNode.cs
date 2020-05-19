using System.Linq;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

[SearchAttributeName("Event/Log")]
public class LogNode : ProcessNode
{
    private Port inputLetter;
    //private Port inputColor;

    public LogNode() : base()
    {
        title = "Log";

        inputLetter = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(string));
        inputContainer.Add(inputLetter);
        //inputColor = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(Color));
        //inputContainer.Add(inputColor);
    }

    public override void Execute()
    {
        string text;
        //Edge color_edge;
        //ColorNode color_node = new ColorNode();
        //Color color;

        text = "The StringNode is not plugged in.";
        //color = Color.black;
        if (inputLetter.connections.FirstOrDefault() != null)
        {
            Edge any_edge = inputLetter.connections.FirstOrDefault();
            var any_node = any_edge.output.node;
            var t_00 = any_node as StringNode;
            if (t_00 != null)
            {
                var decideNode = any_node as StringNode;
                text = decideNode.value.ToString();
            }

            var t_01 = any_node as FloatNode;
            if (t_01 != null)
            {
                var decideNode = any_node as FloatNode;
                text = decideNode.value.ToString();
            }

            var t_02 = any_node as AddNode;
            if (t_02 != null)
            {
                var decideNode = any_node as AddNode;
                text = decideNode.value.ToString();
            }
        }
        //if (inputColor.connections.FirstOrDefault() != null)
        //
        //    color_edge = inputColor.connections.FirstOrDefault();
        //    color_node = color_edge.output.node as ColorNode;
        //    color = color_node.Color;
        //}

        //Debug.Log(string_node.Text);
        Debug.Log(text);
    }
    //}
}