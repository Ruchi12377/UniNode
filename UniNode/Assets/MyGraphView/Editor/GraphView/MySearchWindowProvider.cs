using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MySearchWindowProvider : ScriptableObject, ISearchWindowProvider
{
    private GraphView graphView;
    private List<SearchTreeEntry> SearchTreeEntries { get; set; }
    private Texture2D DummyIcon { get; set; }
    private IDictionary<string, SearchTreeGroupEntry> SearchTreeGroupEntries { get; } = new Dictionary<string, SearchTreeGroupEntry>();

    private static IEnumerable<string> DirectoryOrder { get; } = new[]
    {
        "Audio",
        "Event",
        "Variable",
    };

    public void Initialize(GraphView graphView)
    {
        this.graphView = graphView;

        DummyIcon = new Texture2D(1, 1);
        DummyIcon.SetPixel(0, 0, new Color(0, 0, 0, 0));
        DummyIcon.Apply();

        CalculateSearchTree();
    }

    public void CalculateSearchTree()
    {
        SearchTreeGroupEntries.Clear();
        SearchTreeEntries = new List<SearchTreeEntry>
        {
            new SearchTreeGroupEntry(new GUIContent("Create Node"))
        };
        AppDomain
        .CurrentDomain
        .GetAssemblies()
        .SelectMany(x => x.GetTypes())
        .Where(x => x.IsClass && x.IsSubclassOf(typeof(MyNode)) && !x.IsAbstract && x.GetCustomAttributes(false).Any(y => y is SearchAttributeName))
        .Where(x => x.GetCustomAttributes(typeof(SearchAttributeName), false).Any())
        .Select(x => (menu: x.GetCustomAttributes(typeof(SearchAttributeName), false).OfType<SearchAttributeName>().First(), type: x))
        .Select(x => (x.menu, x.type, entries: x.menu.name.Split('/').Skip(0).ToArray()))
        .Select(x => (x.menu, x.type, x.entries, directory: x.entries.Take(x.entries.Length - 1).Aggregate((a, b) => $"{a}/{b}")))
        .Select(x => (x.menu, x.type, x.entries, x.directory, directoryOrder: DirectoryOrder.Contains(x.entries.FirstOrDefault()) ? DirectoryOrder.Select((directory, index) => (directory, index)).First(y => y.directory == x.entries.FirstOrDefault()).index : int.MaxValue))
        .OrderBy(x => x.directoryOrder)
        .GroupBy(x => x.directory)
        .SelectMany(x => x.OrderBy(y => y.entries.LastOrDefault()))
        .ToList()
        .ForEach(
            item =>
            {
                var (_, type, componentMenuEntries, _, _) = item;
                var fullPath = new StringBuilder();
                foreach (var (componentMenuEntry, index) in componentMenuEntries.Select((x, i) => (x, i)))
                {
                    fullPath.Append($"/{componentMenuEntry}");
                    if (index == componentMenuEntries.Length - 1)
                    {
                        break;
                    }

                    if (SearchTreeGroupEntries.ContainsKey(fullPath.ToString()))
                    {
                        continue;
                    }

                    SearchTreeGroupEntries[fullPath.ToString()] = new SearchTreeGroupEntry(new GUIContent(componentMenuEntry)) { level = index + 1 };
                    SearchTreeEntries.Add(SearchTreeGroupEntries[fullPath.ToString()]);
                }

                SearchTreeEntries.Add(new SearchTreeEntry(new GUIContent(componentMenuEntries.Last(), DummyIcon)) { level = componentMenuEntries.Length, userData = type });
            }
        );
    }

    List<SearchTreeEntry> ISearchWindowProvider.CreateSearchTree(SearchWindowContext context)
    {
        return SearchTreeEntries;
    }

    bool ISearchWindowProvider.OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
    {
        var type = SearchTreeEntry.userData as Type;
        var node = Activator.CreateInstance(type) as MyNode;
        graphView.AddElement(node);
        return true;
    }
}


[Serializable]
public class SearchAttributeName : Attribute
{
    public string name;

    public SearchAttributeName(string name)
    {
        this.name = name;
    }
}