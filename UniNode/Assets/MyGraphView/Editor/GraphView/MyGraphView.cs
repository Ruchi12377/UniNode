using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;
using System.Linq;

public class MyGraphView : GraphView
{
    public RootNode root;
    public RootObject rootObject;
    public GraphAssetData graphAssetData;

    public MyGraphView() : base()
    {
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

        var grid = new GridBackground();
        grid.styleSheets.Add(Resources.Load<StyleSheet>("MyBackGround"));
        grid.StretchToParentSize();
        Insert(0, grid);

        root = new RootNode();
        AddElement(root);

        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        var searchWindowProvider = ScriptableObject.CreateInstance<MySearchWindowProvider>();
        searchWindowProvider.Initialize(this);

        nodeCreationRequest += context =>
        {
            SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), searchWindowProvider);
        };

        //EditorApplication.update += EditorUpdate;
    }

    public void DataSave()
    {
        graphAssetData.GraphDataSave(this);
    }

    public void DataLoad()
    {
        graphAssetData.GraphDataLoad(this);
    }

    public override List<Port> GetCompatiblePorts(Port startAnchor, NodeAdapter nodeAdapter)
    {
        var compatiblePorts = new List<Port>();
        foreach (var port in ports.ToList())
        {
            if (startAnchor.node == port.node ||
                startAnchor.direction == port.direction ||
                startAnchor.portType != port.portType)
            {
                continue;
            }

            compatiblePorts.Add(port);
        }
        return ports.ToList();
    }

    public void Execute()
    {
        if (root.RootObject == null)
        {
            Debug.LogError("RootNodeにGameObjectが設定されていません。");
            return;
        }
        var rootEdge = root.OutputPort.connections.FirstOrDefault();
        if (rootEdge == null) return;

        var currentNode = rootEdge.input.node as ProcessNode;

        while (true)
        {
            currentNode.Execute();

            var edge = currentNode.OutputPort.connections.FirstOrDefault();
            if (edge == null) break;

            currentNode = edge.input.node as ProcessNode;
        }
    }

    /*void EditorUpdate()
    {
        Debug.Log("c");
        if (root.RootObject == null)
        {
            return;
        }
        rootObject = root.RootObject.GetComponent<RootObject>();
        if (rootObject == null)
        {
            root.RootObject.AddComponent<RootObject>();
        }
        if (didCall == false && rootObject != null)
        {
            Debug.Log("a");
            if (rootObject.isStart)
            {
                Debug.Log("b");
                Execute();
                didCall = true;
            }
        }
    }*/
}