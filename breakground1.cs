using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakground1 : MonoBehaviour
{
    public Vector3[] newVertices;
    public int[] newTriangles;
    private Mesh mesh;
    public List<Vector3> vertices;
    public MeshCollider meshCol;
    public GameObject newcude;
    public GameObject cuttable;
    public Vector3[] n;
    
    public int maxX = 10;
    public int maxY = 10;
    // Start is called before the first frame update
    void Start()
    {
        trianglulate(maxX,maxY);
        meshCol = GetComponent<MeshCollider>();
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.Clear();
        mesh.vertices = newVertices;
        mesh.triangles = newTriangles;
        mesh.RecalculateNormals();
        meshCol.sharedMesh = mesh;
        n = mesh.normals;
    }


    public void digGround(float X, float Y)
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        mesh.vertices = newVertices;
        mesh.triangles = newTriangles;
        mesh.RecalculateNormals();
       //mesh.normals = n;
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && hit.collider.tag == "ground")
            {
                int xin = Mathf.RoundToInt(transform.InverseTransformPoint(hit.point).x);
                int zin = Mathf.RoundToInt(transform.InverseTransformPoint(hit.point).z);
                //cude.GetComponent<meshtest>().cutCube(cude.transform.InverseTransformPoint(hit.point).z);
                Debug.Log("as" + transform.InverseTransformPoint(hit.point));
                newVertices[getVertIndex(xin, zin)].y = -1;
                newVertices[getVertIndex(xin+1, zin)].y = -1;
                newVertices[getVertIndex(xin, zin+1)].y = -1;
                newVertices[getVertIndex(xin+1, zin+1)].y = -1;


                breakrubble(xin, zin, 1, 2,0,0);
                breakrubble(xin, zin, 2, 3,0,0);
                breakrubble(xin, zin, 4, 5,0,0);
                breakrubble(xin, zin, 5, 1,0,0);
                breakrubble(xin, zin, 4, 4,4,0);
                breakrubble(xin, zin, 2, 3, 4, 4);
                breakrubble(xin, zin, 3, 7, 4, 4);
                breakrubble(xin, zin, 7, 4, 4, 4);
                meshCol.sharedMesh = mesh;
               
                //Debug.Log((100 + Mathf.RoundToInt( transform.InverseTransformPoint(hit.point).x))*400 +(Mathf.RoundToInt(transform.InverseTransformPoint(hit.point).z) + 100)*4);
            }
        }
    }

    void trianglulate(int x , int y)
    {
        newVertices = new Vector3[(x * y )+4];
        newTriangles = new int[(x * y * 6)+30];
        for (int i = 0; i < x; i++)
        {
            int ver = 0;
            int tri = 0;

            for (int z = 0; z < x; z++)
            {
                for (int b = 0; b < y; b++)
                {
                    newVertices[(z*maxX+b)] = new Vector3(z, 0, b);
                }
            }

            for (int z = 0; z < x-1; z++)
            {
                for (int b = 0; b <  y-1; b++)
                {
                    newTriangles[tri] = getVertIndex(z, b);
                    newTriangles[tri + 1] = getVertIndex(z, b+1);
                    newTriangles[tri + 2] = getVertIndex(z+1, b+1);
                    newTriangles[tri + 3] = getVertIndex(z + 1, b + 1);
                    newTriangles[tri + 4] = getVertIndex(z+1, b);
                    newTriangles[tri + 5] = getVertIndex(z, b);
                    tri += 6;
                }
            }
        }


    }

    int getVertIndex(int x, int z)
    {
        return (x * maxX) + z;
    }

    void breakrubble(int x, int y, int a, int b , int row1, int row2)
    {
        GameObject rubble = Instantiate(newcude, newVertices[getVertIndex(x, y)] + transform.position, Quaternion.identity);
        rubble.GetComponent<meshtest>().newVertices = createRubble(newVertices[getVertIndex(x, y)] - newVertices[getVertIndex(x, y)], newVertices[getVertIndex(x, y) + a + maxX * row1] - newVertices[getVertIndex(x, y)], newVertices[getVertIndex(x, y) + b + maxX * row2] - newVertices[getVertIndex(x, y)]);
        rubble.GetComponent<meshtest>().newTriangles = new int[] { 0, 1, 2, 3, 2, 1, 3, 1, 0, 0, 2, 3 };
        rubble.AddComponent<Rigidbody>();
    }

    Vector3[] createRubble(Vector3 a, Vector3 b, Vector3 c)
    {
        Vector3 d = new Vector3(a.x, 1, a.z);
        Vector3[] rub = new Vector3[] { a, b, c,d };
        return rub;
    }

}
