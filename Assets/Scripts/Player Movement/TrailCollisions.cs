using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class TrailCollisions : MonoBehaviour
{
    TrailRenderer trail;
    public PolygonCollider2D trailCollider;

    static List<PolygonCollider2D> unusedColliders = new();

    [HideInInspector]
    public bool isActiveTrail = true;


    void Awake()
    {
        trail = this.GetComponent<TrailRenderer>();
        trailCollider = GetValidCollider();
    }

    private void FixedUpdate()
    {
        if (isActiveTrail)
        {
            GenerateMeshCollider();
        }
    }

    PolygonCollider2D GetValidCollider()
    {
        PolygonCollider2D validCollider;
        var newCollider = new GameObject("TrailCollider", typeof(PolygonCollider2D))
        {
            tag = "Root",
            layer = LayerMask.NameToLayer("ActiveRoot")
        };
        validCollider = newCollider.GetComponent<PolygonCollider2D>();
        //validCollider.AddComponent<TrailClick>();
        return validCollider;
    }

    void OnDestroy()
    {
        if (trailCollider != null)
        {
            trailCollider.enabled = false;
            unusedColliders.Add(trailCollider);
        }
    }

    public void GenerateMeshCollider()
    {
        if (trail.positionCount > 1)
        {
            Mesh mesh = new Mesh();
            trail.BakeMesh(mesh, true);

            // if you need collisions on both sides of the line, simply duplicate & flip facing the other direction!
            // This can be optimized to improve performance ;)
            int[] meshIndices = mesh.GetIndices(0);
            int[] newIndices = new int[meshIndices.Length * 2];

            int j = meshIndices.Length - 1;
            for (int i = 0; i < meshIndices.Length; i++)
            {
                newIndices[i] = meshIndices[i];
                newIndices[meshIndices.Length + i] = meshIndices[j];
            }
            mesh.SetIndices(newIndices, MeshTopology.Triangles, 0);

            int[] triangles = mesh.triangles;
            Vector3[] vertices = mesh.vertices;

            // Get just the outer edges from the mesh's triangles (ignore or remove any shared edges)
            Dictionary<string, KeyValuePair<int, int>> edges = new();
            for (int i = 0; i < triangles.Length; i += 3)
            {
                for (int e = 0; e < 3; e++)
                {
                    int vert1 = triangles[i + e];
                    int vert2 = triangles[i + e + 1 > i + 2 ? i : i + e + 1];
                    string edge = Mathf.Min(vert1, vert2) + ":" + Mathf.Max(vert1, vert2);
                    if (edges.ContainsKey(edge))
                    {
                        edges.Remove(edge);
                    }
                    else
                    {
                        edges.Add(edge, new KeyValuePair<int, int>(vert1, vert2));
                    }
                }
            }

            // Create edge lookup (Key is first vertex, Value is second vertex, of each edge)
            Dictionary<int, int> lookup = new Dictionary<int, int>();
            foreach (KeyValuePair<int, int> edge in edges.Values)
            {
                if (lookup.ContainsKey(edge.Key) == false)
                {
                    lookup.Add(edge.Key, edge.Value);
                }
            }

            // Create empty polygon collider
            //PolygonCollider2D polygonCollider = gameObject.AddComponent<PolygonCollider2D>();
            trailCollider.pathCount = 0;

            // Loop through edge vertices in order
            int startVert = 0;
            int nextVert = startVert;
            int highestVert = startVert;
            List<Vector2> colliderPath = new List<Vector2>();
            while (true)
            {

                // Add vertex to collider path
                colliderPath.Add(vertices[nextVert]);

                // Get next vertex
                nextVert = lookup[nextVert];

                // Store highest vertex (to know what shape to move to next)
                if (nextVert > highestVert)
                {
                    highestVert = nextVert;
                }

                // Shape complete
                if (nextVert == startVert)
                {

                    // Add path to polygon collider
                    trailCollider.pathCount++;
                    trailCollider.SetPath(trailCollider.pathCount - 1, colliderPath.ToArray());
                    colliderPath.Clear();

                    // Go to next shape if one exists
                    if (lookup.ContainsKey(highestVert + 1))
                    {

                        // Set starting and next vertices
                        startVert = highestVert + 1;
                        nextVert = startVert;

                        // Continue to next loop
                        continue;
                    }

                    // No more verts
                    break;
                }
            }

            //myCollider.sharedMesh = mesh;
        }

    }
}