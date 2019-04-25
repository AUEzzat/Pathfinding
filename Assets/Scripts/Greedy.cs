using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Greedy : PathFinding
{
    public override List<Node> FindPath(Node startNode, Node targetNode, out HashSet<Node> closedSet, in Grid grid)
    {
        List<Node> openSet = new List<Node>();
        closedSet = new HashSet<Node>();

        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node node = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].hCost < node.hCost)
                {
                    node = openSet[i];
                }
            }

            openSet.Remove(node);
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

                int newCostToNeighbour = GetDistance(neighbour, targetNode);
                if (newCostToNeighbour < neighbour.hCost || !openSet.Contains(neighbour))
                {
                    neighbour.hCost = newCostToNeighbour;
                    neighbour.parent = node;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }

        return new List<Node>();
    }
}