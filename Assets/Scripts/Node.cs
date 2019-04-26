using UnityEngine;
using System.Collections;

public class Node {
	
	public bool walkable;
	public Vector3 worldPosition;
	public int gridX;
	public int gridY;

    public float pathCost;
	public float gCost;
	public int hCost;
	public Node parent;
	
	public Node(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY) {
		walkable = _walkable;
		worldPosition = _worldPos;
		gridX = _gridX;
		gridY = _gridY;
        gCost = int.MaxValue;
        pathCost = Random.value * 0.5f + 0.5f;
    }

	public float fCost {
		get {
			return gCost + hCost;
		}
	}
}
