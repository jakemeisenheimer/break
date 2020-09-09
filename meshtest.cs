using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meshtest : MonoBehaviour
{
    public float min = -1f;
    public float max = 1f;
    public Vector3[] newVertices;
    public int[] newTriangles;
    public GameObject cuttable;
    private Mesh mesh;
    public GameObject newcude;
    public MeshCollider meshCol;
    public List<Vector3> vertices;
    // Start is called before the first frame update
    void Start()
    {
        meshCol = GetComponent<MeshCollider>();
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.Clear();
        mesh.vertices = newVertices;
        mesh.triangles = newTriangles;
        mesh.RecalculateNormals();
        meshCol.sharedMesh = mesh;
    }


   public void cutCube(float X)
    {
        newcude =  Instantiate(cuttable, transform.position, Quaternion.identity);
        newVertices = new Vector3[] { new Vector3(-1, -1, X), new Vector3(1, -1, X), new Vector3(1, 1, X), new Vector3(-1, 1, X), new Vector3(-1, -1, -1), new Vector3(1, -1, -1), new Vector3(1, 1, -1), new Vector3(-1, 1, -1) };
        newcude.GetComponent<meshtest>().newVertices = new Vector3[] { new Vector3(-1, -1, max), new Vector3(1, -1, max), new Vector3(1, 1, max), new Vector3(-1, 1, max), new Vector3(-1, -1, X), new Vector3(1, -1, X), new Vector3(1, 1, X), new Vector3(-1, 1, X) };
        newcude.GetComponent<meshtest>().newTriangles = new int[] { 0, 1, 2, 0, 2, 3, 1, 5, 6, 1, 6, 2, 5, 4, 6, 7, 6, 4, 3, 7, 4, 0, 3, 4, 2, 6, 3, 3, 6, 7, 4, 1, 0, 4, 5, 1 };
        max = X;
        newcude.AddComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        mesh.vertices = newVertices;
        mesh.RecalculateNormals();
    }
}
