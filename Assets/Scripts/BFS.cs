using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFS : PathFinding
{
    public override List<Node> FindPath(Node startNode, Node targetNode, out HashSet<Node> closedSet, in Grid grid)
    {
        Queue<Node> openSet = new Queue<Node>();
        closedSet = new HashSet<Node>();

        openSet.Enqueue(startNode);

        while (openSet.Count > 0)
        {
            Node node = openSet.Dequeue();
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
                    openSet.Enqueue(neighbour);
                }
            }
        }

        return new List<Node>();
    }
}
