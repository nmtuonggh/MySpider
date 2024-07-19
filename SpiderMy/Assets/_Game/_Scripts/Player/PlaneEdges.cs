using System;
using System.Collections.Generic;
using UnityEngine;

namespace SFRemastered
{
    public class PlaneEdges : MonoBehaviour
    {
        public List<GameObject> verticesObjects = new List<GameObject>();

        public List<(Vector3, Vector3)> GetEdges()
        {
            List<(Vector3, Vector3)> edges = new List<(Vector3, Vector3)>();
            for (int i = 0; i < verticesObjects.Count; i++)
            {
                // Extract positions from GameObjects
                Vector3 start = verticesObjects[i].transform.position;
                Vector3 end = verticesObjects[(i + 1) % verticesObjects.Count].transform.position;
                edges.Add((start, end));
            }

            return edges;
        }
    }
}