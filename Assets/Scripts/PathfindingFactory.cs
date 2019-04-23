using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class PathfindingFactory : MonoBehaviour
{
    public Transform seeker, target;
    Grid grid;
    Dictionary<string, PathFinding> pathFindingDict = new Dictionary<string, PathFinding>();
    string currentAlgorithm;

    void Awake()
    {
        grid = GetComponent<Grid>();
        PopulatePathFindings();
    }

    void Update()
    {
        Node startNode = grid.NodeFromWorldPoint(seeker.position);
        Node targetNode = grid.NodeFromWorldPoint(target.position);

        if (currentAlgorithm != null)
            grid.path = pathFindingDict[currentAlgorithm].FindPath(startNode, targetNode, out grid.@checked, grid);
    }

    public void PopulatePathFindings()
    {
        Assembly.GetAssembly(typeof(PathFinding)).GetTypes()
            .Where(type => type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(PathFinding)))
             .ToList().ForEach(x => pathFindingDict.Add(x.Name, (PathFinding)System.Activator.CreateInstance(x)));
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUIStyle style = new GUIStyle();

        foreach (string pathFindAlgorithm in pathFindingDict.Keys)
        {
            if (pathFindAlgorithm == currentAlgorithm)
            {
                GUI.backgroundColor = Color.white;
                GUI.contentColor = Color.black;
            }
            else
            {
                GUI.backgroundColor = Color.black;
                GUI.contentColor = Color.white;
            }

            if (GUILayout.Button(pathFindAlgorithm))
            {
                currentAlgorithm = pathFindAlgorithm;
            }
        }
        GUILayout.EndHorizontal();
    }
}
