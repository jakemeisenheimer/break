using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spear : MonoBehaviour
{

    public Vector3[] newVertices;
    public int[] newTriangles;
    private Mesh mesh;
    public MeshCollider meshCol;
    public List<Vector3> vertices;
    public GameObject player;
    public bool addforce = true;
    public Transform top;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3f);
        player = GameObject.FindGameObjectWithTag("Player");
        meshCol = GetComponent<MeshCollider>();
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.Clear();
        mesh.vertices = newVertices;
        mesh.triangles = newTriangles;
        mesh.RecalculateNormals();
        meshCol.sharedMesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (newVertices[4].z < 2)
        {
            newVertices[4].z += .05f;
            newVertices[5].z -= .05f;
        }
        else if(addforce)
        {
            
            transform.LookAt(player.transform.position);
            transform.eulerAngles = new Vector3(0, transform.rotation.eulerAngles.y, 0);
            addforce = false;
            GetComponent<Rigidbody>().useGravity = true;
            Vector3 a = player.transform.position + Vector3.up - transform.position;
            //a.y = 0;
            GetComponent<Rigidbody>().AddForce(a*200);
        }
        mesh.vertices = newVertices;
        mesh.RecalculateNormals();
    }
}