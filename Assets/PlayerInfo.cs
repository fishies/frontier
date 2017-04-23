using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
	public int playerID;
	public int[] stockpile;

	void Start ()
	{
		stockpile = new int[System.Enum.GetNames (typeof(Production.Resource)).Length];
		stockpile [(int)Production.Resource.Food] = 6;
		stockpile [(int)Production.Resource.Lumber] = 6;
		stockpile [(int)Production.Resource.Cement] = 6;
		stockpile [(int)Production.Resource.Steel] = 6;

		foreach (GraphNode node in ConnectedTerritories()) {
			node.gameObject.GetComponent<SpriteRenderer> ().color = Color.red;
		}
	}

	List<GraphNode> ConnectedTerritories () //returns what you think it returns based on the name of this function :^)
	{
		// BFS starting with own capital as root.
		GraphNode root = gameObject.GetComponent<GraphNode> ();

		List<GraphNode> visited = new List<GraphNode> ();
		visited.Add (root);

		Queue<GraphNode> toVisit = new Queue<GraphNode> ();
		foreach (GraphNode node in root.neighbors) {
			toVisit.Enqueue (node);
		}

		while (toVisit.Count > 0) {
			GraphNode node = toVisit.Dequeue ();
			if (visited.Contains (node))
				continue;
			visited.Add (node);
			foreach (GraphNode next in node.neighbors) {
				toVisit.Enqueue (next);
			}
		}

		return visited;

		// The following actually won't work. Counter-example: village has 1 edge but it's cut off from the capital after a raider destroys a road.
		/*Dictionary<GameObject,int> edgeCount = new Dictionary<GameObject, int> (); // keeps track of how many neighbors a node has; islands have no owners
		foreach (RoadHandler rh in Object.FindObjectsOfType<RoadHandler>()) {
			foreach (GameObject obj in rh.getEndpoints()) {
				++edgeCount [obj];
			}
		}
		foreach (GameObject obj in edgeCount.Keys) {
			if (edgeCount [obj] < 1) {
				obj.GetComponent<Production> ().ownerID = 0;
			}
		}*/
	}

	void CalculateIncome ()
	{ //This should be called at the start of the player's turn
		foreach (Production p in Object.FindObjectsOfType<Production>()) {
			if (p.ownerID == playerID) {
				foreach (int resource in System.Enum.GetValues(typeof(Production.Resource))) {
					stockpile [resource] += p.income [resource];
				}
			}
		}
	}
}
