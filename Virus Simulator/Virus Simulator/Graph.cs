using System;
using System.Collections.Generic;

/// <summary>
/// A weighted directed graph datatype
/// </summary>
/// <typeparam name="T">The type of node</typeparam>
public class Graph<T> {

	private List<Node<T>> nodes = new List<Node<T>>();

	/// <summary>
	/// Add item into the graph as a node
	/// </summary>
	/// <param name="item">The object to insert to</param>
	public void AddNode(T node) {
		nodes.Add(new Node<T>(node));
	}

	/// <summary>
    /// The number of nodes in the graph
    /// </summary>
	public int Size {
		get { return nodes.Count; }
	}

	/// <summary>
    /// Returns the adjacent nodes of a node
    /// </summary>
    /// <param name="index">Index of the node to check</param>
    /// <returns></returns>
	public AdjacentNodes<T>[] Adjacent(int index) {
		Dictionary<Node<T>, float> adjacents = nodes[index].adjacencyList;
		List<AdjacentNodes<T>> adjacentNodes = new List<AdjacentNodes<T>>();
        foreach (KeyValuePair<Node<T>, float> adjacent in adjacents) {
			adjacentNodes.Add(new AdjacentNodes<T>(nodes[index].item, adjacent.Key.item, adjacent.Value));
        }
		return adjacentNodes.ToArray();
    }

	/// <summary>
	/// Make a connection between graph
	/// </summary>
	/// <param name="index1">The index of the node to connect from</param>
	/// <param name="index2">The index of the node to connect to</param>
	public void ConnectNodes(int index1, int index2, float weight = 1) {
		nodes[index1].ConnectTo(nodes[index2], weight);
    }

	/// <summary>
	/// Get the item at index i
	/// </summary>
	/// <param name="i">Item index</param>
	/// <returns></returns>
	public T this[int i] {
		get { return nodes[i].item; }
	}

	private class Node<T> {
		public T item;
		public Dictionary<Node<T>, float> adjacencyList = new Dictionary<Node<T>, float>();

		public Node(T item) {
			this.item = item;
		}

		public void ConnectTo (Node<T> node, float weight = 1) {
			if (!adjacencyList.ContainsKey(node)) {
				adjacencyList.Add(node, weight);
            }
		}
	}

	/// <summary>
    /// Pair of adjacent nodes
    /// </summary>
    /// <typeparam name="T">Node type</typeparam>
	public class AdjacentNodes<T> {
		public T first;
		public T second;
		public float weight;

		public AdjacentNodes(T first, T second, float weight) {
			this.first = first;
			this.second = second;
			this.weight = weight;
		}
    }
	
}
