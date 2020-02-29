using System;
using System.Collections.Generic;

/// <summary>
/// A directed graph datatype
/// </summary>
/// <typeparam name="T">The type of node</typeparam>
public class Graph<T> {

	private List<Node<T>> nodes = new List<Node<T>>();

	/// <summary>
	/// Add item into the graph as a node
	/// </summary>
	/// <param name="item">The object to insert to</param>
	public void AddItem(T item) {
		nodes.Add(new Node<T>(item));
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
		List<Node<T>> adjacents = nodes[index].adjacencyList;
		List<AdjacentNodes<T>> adjacentNodes = new List<AdjacentNodes<T>>();
        foreach (Node<T> adjacent in adjacents) {
			adjacentNodes.Add(new AdjacentNodes<T>(nodes[index], adjacent));
        }
		return adjacentNodes.ToArray();
    }

	/// <summary>
    /// Returns the adjacent nodes of a node
    /// </summary>
    /// <param name="node">The node to check</param>
    /// <returns></returns>
	public AdjacentNodes<T>[] Adjacent(T node) {
		return Adjacent(nodes.IndexOf(node));
    }

	/// <summary>
	/// Make a connection between graph
	/// </summary>
	/// <param name="index1">The index of the node to connect from</param>
	/// <param name="index2">The index of the node to connect to</param>
	public void ConnectNodes(int index1, int index2) {
		nodes[index1].adjacencyList.Add(nodes[index2]);
    }

	/// <summary>
	/// Make a connection between graph
	/// </summary>
	/// <param name="node1">The node to connect from</param>
	/// <param name="node2">The node to connect to</param>
	public void ConnectNodes(T node1, T node2) {
		ConnectNodes(nodes.IndexOf(node1), nodes.IndexOf(node2));
	}

	/// <summary>
	/// Get the item at index i
	/// </summary>
	/// <param name="i">Item index</param>
	/// <returns></returns>
	public T this[int i] {
		get { return nodes[i]; }
	}

	private class Node<T> {
		public T item;
		public List<Node<T>> adjacencyList = new List<Node<T>>();

		public Node(T item) {
			this.item = item;
		}

		public void ConnectTo (Node<T> node) {
			if (!adjacencyList.Contains(node)) {
				adjacencyList.Add(node);
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

		public AdjacentNodes(T first, T second) {
			this.first = first;
			this.second = second;
		}
    }
	
}
