using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DFS : PathFinding
{
    public override List<Node> FindPath(Node startNode, Node targetNode, out HashSet<Node> closedSet, in Grid grid)
    {
        Stack<Node> openSet = new Stack<Node>();
        closedSet = new HashSet<Node>();

        openSet.Push(startNode);

        while (openSet.Count > 0)
        {

            Node node = openSet.Pop();
            closedSet.Add(node);

            if (node == targetNode)
            {
                return RetracePath(startNode, targetNode);
            }

            foreach (Node neighbour in grid.GetNeighbours(node))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                if (!openSet.Contains(neighbour))
                {
                    neighbour.parent = node;
                    openSet.Push(neighbour);
                }
            }
        }

        return new List<Node>();
    }
}
