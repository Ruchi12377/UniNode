using UnityEditor;
using UnityEngine;
using UnityEditor.Callbacks;
using UnityEngine.UIElements;


public class GraphEditorWindow : EditorWindow
{
    GraphAssetData m_GraphAsset;
    MyGraphView m_GraphEditorElement;

    //[MenuItem("Window/Open GraphEditor")]
    public static void Open()
	{
        GraphEditorWindow graphEditorWindow = CreateInstance<GraphEditorWindow>();
        graphEditorWindow.Show();
        graphEditorWindow.titleContent = new GUIContent("GraphEditor");
        if (Selection.activeObject is GraphAssetData graphAsset)
        {
            graphEditorWindow.Initialize(graphAsset);
        }
    }

    [OnOpenAsset(0)]//アセットを開いたとき
    static bool OnOpenAsset(int instanceId, int line)
    {
        if (EditorUtility.InstanceIDToObject(instanceId) is GraphAssetData)//GraphAssetかどうか
        {
            Open();
            return true;
        }
        return false;
    }

    //CreateInstance<GraphEditor>()の時に呼ばれる
    private void OnEnable()
    {
        if (m_GraphAsset != null)
        {
            Initialize(m_GraphAsset);
        }
    }

    public void Initialize(GraphAssetData graphAsset)
    {
        // 初期化でもgraphAssetを使うことになるのでここに移す
        m_GraphAsset = graphAsset;
        m_GraphEditorElement = new MyGraphView()
        {
            style = { flexGrow = 1 }
        };

        rootVisualElement.Add(m_GraphEditorElement);
        rootVisualElement.Add(new Button(m_GraphEditorElement.DataSave) { text = "Save" });
        rootVisualElement.Add(new Button(m_GraphEditorElement.Execute) { text = "Execute" });
        m_GraphEditorElement.graphAssetData = graphAsset;
        m_GraphEditorElement.DataLoad();
    }
}