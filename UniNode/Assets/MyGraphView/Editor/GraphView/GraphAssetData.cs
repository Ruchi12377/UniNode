using UnityEditor;
using System;
using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newGraphAsset.asset", menuName = "Create GraphAsset")]
public class GraphAssetData : ScriptableObject
{
    public List<EdgeData> edges = new List<EdgeData>();
    public List<NodeData> nodes = new List<NodeData>();
    public List<PortData> ports = new List<PortData>();

    public void GraphDataSave(MyGraphView graphView)
    {
        var graphData = CreateInstance<GraphAssetData>();

        int count = 0;
        graphView.nodes.ForEach(
            node =>
            {
                var nodeData = new NodeData();
                nodeData.SetNodeRect(node.GetPosition());
                nodeData.SetNodeType(node.GetType());
                count++;
                graphData.nodes.Add(nodeData);
            }
        );
        graphView.edges.ForEach(
            edge =>
            {
                var edgeData = new EdgeData();

                edgeData.SetFromNodeID(edge.input.GetHashCode());
                edgeData.SetToNodeID(edge.output.GetHashCode());

                graphData.edges.Add(edgeData);
            }
        );
        count = 0;
        graphView.ports.ForEach(
            port =>
            {
                var portData = new PortData();
                portData.SetPortID(count);
                portData.SetHash(port.GetHashCode());
                count++;
                graphData.ports.Add(portData);
            }
        );

        nodes = graphData.nodes;
        edges = graphData.edges;
        ports = graphData.ports;
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    public void GraphDataLoad(MyGraphView graphView)
    {
        var nodes = this.nodes;
        var edges = this.edges;
        var ports = this.ports;

        Debug.Log(nodes.Count);
        Debug.Log(edges.Count);
        Debug.Log(ports.Count);

        for (int i = 0; i < nodes.Count; i++)
        {
            var node = nodes[i];
            var type = node.GetNodeType();
            var pos = node.GetNodeRect();
            if (i == 0)
            {
                graphView.root.SetPosition(pos);
            }
            else
            {
                var createNode = Activator.CreateInstance(type) as MyNode;
                createNode.SetPosition(pos);
                graphView.AddElement(createNode);
            }
        }
        
        int count = 0;
        List<int> PortNewHash = new List<int>(0);
        List<Port> PortNew = new List<Port>(0);
        graphView.ports.ForEach(
            port =>
            {
                if(count < ports.Count)
                {
                    var Hash = ports[count].GetHash();
                    PortNewHash.Add(Hash);
                    PortNew.Add(port);
                    count++;
                }
            }
        );
        /*------------------------------------------------------------------*/
        for (int e = 0; e < edges.Count; e++)
        {
            for(int o = 0; o < ports.Count; o++)
            {
                if (ports[o].GetHash() == edges[e].GetFromNodeID())
                {
                    edges[e].SetFromNodeID(PortNewHash[o]);
                    edges[e].SetFromPort(PortNew[o]);
                }
                if (ports[o].GetHash() == edges[e].GetToNodeID())
                {
                    edges[e].SetToNodeID(PortNewHash[o]);
                    edges[e].SetToPort(PortNew[o]);
                }
            }
        }

        foreach(var edge in edges)
        {
            var f = edge.GetFromPort();
            var t = edge.GetToPort();
            Edge CreateEdge = f.ConnectTo(t);
            graphView.AddElement(CreateEdge);
        }
    }
}

[Serializable]
public class NodeData
{
    private Rect rect;
    private Type type;
    private MyNode myNode;
    public int NodeID;
    public int NodeHash;
    /*-------------------------------------------*/
    public void SetNodeRect(Rect rect)
    {
        this.rect = rect;
    }

    public void SetNodeType(Type type)
    {
        this.type = type;
    }

    public void SetNodeID(int NodeID)
    {
        this.NodeID = NodeID;
    }

    public void SetNodeHash(int NodeHash)
    {
        this.NodeHash = NodeHash;
    }
    /*-------------------------------------------*/
    public Rect GetNodeRect()
    {
        return rect;
    }

    public Type GetNodeType()
    {
        return type;
    }

    public int GetNodeID()
    {
        return NodeID;
    }

    public int GetNodeHash()
    {
        return NodeHash;
    }

    /*-------------------------------------------*/
}


[Serializable]
public class PortData
{
    public int PortHash;
    public int PortID;

    /*-------------------------------------------*/
    public void SetHash(int hash)
    {
        PortHash = hash;
    }

    public void SetPortID(int ID)
    {
        PortID = ID;
    }
    /*-------------------------------------------*/
    public int GetHash()
    {
        return PortHash;
    }

    public int GetPortID()
    {
        return PortID;
    }
    /*-------------------------------------------*/
}

[Serializable]
public class EdgeData
{
    private Port FromPort;
    private Port ToPort;
    private int FromNodeID;
    private int ToNodeID;

    /*-------------------------------------------*/
    /// <summary>
    /// output
    /// </summary>
    /// <param name="FromPort"></param>
    public void SetFromPort(Port FromPort)
    {
        this.FromPort = FromPort;
    }

    /// <summary>
    /// input
    /// </summary>
    /// <param name="ToPort"></param>
    public void SetToPort(Port ToPort)
    {
        this.ToPort = ToPort;
    }

    public void SetFromNodeID(int ID)
    {
        FromNodeID = ID;
    }

    public void SetToNodeID(int ID)
    {
        ToNodeID = ID;
    }
    /*-------------------------------------------*/

    public Port GetFromPort()
    {
        return FromPort;
    }

    public Port GetToPort()
    {
        return ToPort;
    }

    public int GetFromNodeID()
    {
        return FromNodeID;
    }

    public int GetToNodeID()
    {
        return ToNodeID;
    }
    /*-------------------------------------------*/
}