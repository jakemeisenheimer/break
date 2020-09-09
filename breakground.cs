using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakground : MonoBehaviour
{
    public Vector3[] newVertices;
    public int[] newTriangles;
    private Mesh mesh;
    public List<Vector3> vertices;
    public MeshCollider meshCol;
    public GameObject newcude;
    public GameObject cuttable;
    public int maxX = 10;
    public int maxY = 10;
    private int MaxTriangles;
    public List<int>[,] assocation;
    public Vector2[] uvs;
    private int currentTriangle = 0;
    public GameObject expolostion;
    // Start is called before the first frame update
    void Start()
    {

        MaxTriangles = (maxX * maxY * 6) * 2;
        trianglulate(maxX, maxY);
        meshCol = GetComponent<MeshCollider>();
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.Clear();
        mesh.vertices = newVertices;
        mesh.triangles = newTriangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        meshCol.sharedMesh = mesh;
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
    }

    void breakfloor(float xt, float zt)
    {
        int xin = Mathf.RoundToInt(xt);
        int zin = Mathf.RoundToInt(zt);
        Debug.Log(xin);
        Debug.Log(zin);
        Debug.Log(assocation[xin, zin][0]);
        Debug.Log(assocation[xin, zin-1][0]);
        Debug.Log(assocation[xin, zin+1][0]);
       bottom(xin, zin, new Vector3(xin, -1, zin));

        modifySame(xin + 1, zin, new Vector3(Random.Range(-.2f, .2f), Random.Range(-.2f, .2f), Random.Range(-.2f, .2f)));
        modifySame(xin - 1, zin, new Vector3(Random.Range(-.2f, .2f), Random.Range(-.2f, .2f), Random.Range(-.2f, .2f)));
        modifySame(xin, zin + 1, new Vector3(Random.Range(-.2f, .2f), Random.Range(-.2f, .2f), Random.Range(-.2f, .2f)));
        modifySame(xin, zin - 1, new Vector3(Random.Range(-.2f, .2f), Random.Range(-.2f, .2f), Random.Range(-.2f, .2f)));
        modifySame(xin + 1, zin + 1, new Vector3(Random.Range(-.2f, .2f), Random.Range(-.2f, .2f), Random.Range(-.2f, .2f)));
        modifySame(xin - 1, zin - 1, new Vector3(Random.Range(-.2f, .2f), Random.Range(-.2f, .2f), Random.Range(-.2f, .2f)));
        modifySame(xin + 1, zin - 1, new Vector3(Random.Range(-.2f, .2f), Random.Range(-.2f, .2f), Random.Range(-.2f, .2f)));
        modifySame(xin - 1, zin + 1, new Vector3(Random.Range(-.2f, .2f), Random.Range(-.2f, .2f), Random.Range(-.2f, .2f)));

        breakrubble(xin, zin, xin + 1, zin, xin + 1, zin + 1);

        breakrubble(xin, zin, xin + 1, zin, xin + 1, zin - 1);
        breakrubble(xin, zin, xin + 1, zin + 1, xin, zin + 1);
        breakrubble(xin, zin, xin, zin + 1, xin - 1, zin + 1);
        breakrubble(xin, zin, xin - 1, zin + 1, xin - 1, zin);
        breakrubble(xin, zin, xin - 1, zin, xin - 1, zin - 1);
        breakrubble(xin, zin, xin - 1, zin - 1, xin, zin - 1);
        breakrubble(xin, zin, xin, zin - 1, xin - 1, zin - 1);
       
        mesh.vertices = newVertices;
        meshCol.sharedMesh = mesh;
        meshCol.convex = false;
        
    }

    void trianglulate(int x, int y)
    {
        assocation = new List<int>[x+1, y+1];
        makeList();
        newVertices = new Vector3[(x * y * 4) + 4];
        uvs = new Vector2[(x * y * 4) + 4];
        for (int i = 0; i < (x * y * 4) + 4;  i++)
        {
            uvs[i] = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
        }
        newTriangles = new int[MaxTriangles];
        for (int i = 0; i < x; i++)
        {
            int ver = 0;
            int tri = 0;
            for (int z = 0; z < y; z++)
            {
                newVertices[(i * 4 * y) + ver] = new Vector3(i, 0, z);
                newVertices[(i * 4 * y) + ver + 1] = new Vector3(i + 1, 0, z);
                newVertices[(i * 4 * y) + ver + 2] = new Vector3(i + 1, 0, z + 1);
                newVertices[(i * 4 * y) + ver + 3] = new Vector3(i, 0, z + 1);
                newTriangles[(i * 6 * y) + tri] = (i * 4 * y) + ver + 2;
                newTriangles[(i * 6 * y) + tri + 1] = (i * 4 * y) + ver + 1;
                newTriangles[(i * 6 * y) + tri + 2] = (i * 4 * y) + ver + 0;
                newTriangles[(i * 6 * y) + tri + 3] = (i * 4 * y) + ver + 2;
                newTriangles[(i * 6 * y) + tri + 4] = (i * 4 * y) + ver + 0;
                newTriangles[(i * 6 * y) + tri + 5] = (i * 4 * y) + ver + 3;

                assocation[i, z].Add((i * 4 * y) + ver);
                assocation[i + 1, z].Add((i * 4 * y) + ver + 1);
                assocation[i + 1, z + 1].Add((i * 4 * y) + ver + 2);
                assocation[i, z + 1].Add((i * 4 * y) + ver + 3);
                ver += 4;
                tri += 6;
            }
        }
        
        newVertices[(x * y * 4)] = new Vector3(0, -2, 0);
        newVertices[(x * y * 4) + 1] = new Vector3(0, -2, maxY);
        newVertices[(x * y * 4) + 2] = new Vector3(maxX, -2, 0);
        newVertices[(x * y * 4) + 3] = new Vector3(maxX, -2, maxY);

        newTriangles[(x * y * 6)] = (x * y * 4);
        newTriangles[(x * y * 6) + 1] = maxY * 4 - 1;
        newTriangles[(x * y * 6) + 2] = 0;

        newTriangles[(x * y * 6) + 3] = (x * y * 4);
        newTriangles[(x * y * 6) + 4] = (x * y * 4) + 1;
        newTriangles[(x * y * 6) + 5] = maxY * 4 - 1;

        newTriangles[(x * y * 6) + 6] = (x * y * 4) - 2;
        newTriangles[(x * y * 6) + 7] = maxY * 4 - 1;
        newTriangles[(x * y * 6) + 8] = (x * y * 4) + 1;

        newTriangles[(x * y * 6) + 9] = (x * y * 4) + 1;
        newTriangles[(x * y * 6) + 10] = (x * y * 4) + 3;
        newTriangles[(x * y * 6) + 11] = (x * y * 4) - 2;

        newTriangles[(x * y * 6) + 12] = (x * y * 4) - 2;
        newTriangles[(x * y * 6) + 13] = (x * y * 4) + 3;
        newTriangles[(x * y * 6) + 14] = (x * y * 4) + 2;

        newTriangles[(x * y * 6) + 15] = (x * y * 4) + 2;
        newTriangles[(x * y * 6) + 16] = (x * y * 4) - 4 - maxY * 4 + 5;
        newTriangles[(x * y * 6) + 17] = (x * y * 4) - 2;

        newTriangles[(x * y * 6) + 18] = (x * y * 4) - 4 - maxY * 4 + 5;
        newTriangles[(x * y * 6) + 19] = (x * y * 4) + 2;
        newTriangles[(x * y * 6) + 20] = (x * y * 4);

        newTriangles[(x * y * 6) + 21] = (x * y * 4) - 4 - maxY * 4 + 5;
        newTriangles[(x * y * 6) + 22] = (x * y * 4);
        newTriangles[(x * y * 6) + 23] = 0;

        newTriangles[(x * y * 6) + 24] = (x * y * 4) + 2;
        newTriangles[(x * y * 6) + 25] = (x * y * 4) + 1;
        newTriangles[(x * y * 6) + 26] = (x * y * 4);

        newTriangles[(x * y * 6) + 27] = (x * y * 4) + 1;
        newTriangles[(x * y * 6) + 28] = (x * y * 4) + 2;
        newTriangles[(x * y * 6) + 29] = (x * y * 4) + 3;
        
        
        currentTriangle = (x * y * 6) + 30;
    }

    int getVertIndex(int x, int z)
    {
        return x * maxY * 4 + (z * 4);
    }

    void breakrubble(int x, int y, int x1, int z1, int x2, int z2)
    {
        Debug.Log(getVertIndex(x, y));
        Vector3[] peice = createRubble(new Vector3(0, 0, 0), newVertices[assocation[x1, z1][0]] - newVertices[getVertIndex(x, y)], newVertices[assocation[x2, z2][0]] - newVertices[getVertIndex(x, y)]);
        breakPeice(peice, transform.TransformPoint(newVertices[getVertIndex(x, y)]));
    }

    Vector3[] createRubble(Vector3 a, Vector3 b, Vector3 c)
    {
        Vector3 d = new Vector3(a.x, 1, a.z);
        Vector3[] rub = new Vector3[] { a, b, c, d };
        return rub;
    }

    void breakPeice(Vector3[] peice, Vector3 center)
    {
        Vector3 midpointtop = (peice[1] / 2);
        Vector3 midpointbot = ((peice[1] + peice[2]) / 2);
        Vector3 midpointbot2 = ((peice[1] + peice[3]) / 2);
        Vector3 newcenter = (midpointbot + midpointbot2 + midpointtop + peice[1]) / 4;

        GameObject rubble = Instantiate(newcude, center + newcenter, Quaternion.identity);
        rubble.GetComponent<meshtest>().newVertices = new Vector3[] { peice[1] - newcenter, midpointbot - newcenter, midpointbot2 - newcenter, midpointtop - newcenter };
        rubble.GetComponent<meshtest>().newTriangles = new int[] { 2, 1, 0, 3, 2, 0, 1, 3, 0, 2, 3, 1 };
        rubble.AddComponent<Rigidbody>();
        rubble.GetComponent<Rigidbody>().AddForce(Vector3.up * 500 + new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50)));

        midpointtop = peice[2] / 2;
        midpointbot = (peice[1] + peice[2]) / 2;
        midpointbot2 = (peice[2] + peice[3]) / 2;
        newcenter = (midpointbot + midpointbot2 + midpointtop + peice[2]) / 4;

        rubble = Instantiate(newcude, center + newcenter, Quaternion.identity);
        rubble.GetComponent<meshtest>().newVertices = new Vector3[] { peice[2] - newcenter, midpointbot - newcenter, midpointbot2 - newcenter, midpointtop - newcenter };
        rubble.GetComponent<meshtest>().newTriangles = new int[] { 2, 1, 0, 3, 2, 0, 1, 3, 0, 2, 3, 1 };
        rubble.AddComponent<Rigidbody>();
        rubble.GetComponent<Rigidbody>().AddForce(Vector3.up * 500 + new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50)));

        midpointtop = peice[3] / 2;
        midpointbot = (peice[1] + peice[3]) / 2;
        midpointbot2 = (peice[2] + peice[3]) / 2;
        newcenter = (midpointbot + midpointbot2 + midpointtop + peice[3]) / 4;

        rubble = Instantiate(newcude, center + newcenter, Quaternion.identity);
        rubble.GetComponent<meshtest>().newVertices = new Vector3[] { peice[3] - newcenter, midpointbot - newcenter, midpointbot2 - newcenter, midpointtop - newcenter };
        rubble.GetComponent<meshtest>().newTriangles = new int[] { 2, 1, 0, 3, 2, 0, 1, 3, 0, 2, 3, 1 };
        rubble.AddComponent<Rigidbody>();
        rubble.GetComponent<Rigidbody>().AddForce(Vector3.up * 500 + new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50)));

        midpointtop = peice[1] / 2;
        midpointbot = peice[2] / 2;
        midpointbot2 = peice[3] / 2;
        newcenter = (midpointbot + midpointbot2 + midpointtop + peice[0]) / 4;


        rubble = Instantiate(newcude, center + newcenter, Quaternion.identity);
        rubble.GetComponent<meshtest>().newVertices = new Vector3[] { peice[0] - newcenter, midpointbot - newcenter, midpointbot2 - newcenter, midpointtop - newcenter };
        rubble.GetComponent<meshtest>().newTriangles = new int[] { 2, 1, 0, 3, 2, 0, 1, 3, 0, 2, 3, 1 };
        rubble.AddComponent<Rigidbody>();
        rubble.GetComponent<Rigidbody>().AddForce(Vector3.up * 500 + new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50)));

        midpointtop = (peice[1] + peice[2]) / 2;
        Vector3 midpointtop2 = (peice[2] + peice[3]) / 2;
        Vector3 midpointtop3 = (peice[1] + peice[3]) / 2;

        midpointbot = peice[1] / 2;
        midpointbot2 = peice[2] / 2;
        Vector3 midpointbot3 = peice[3] / 2;

        newcenter = (midpointbot + midpointbot2 + midpointbot3 + midpointtop + midpointtop2 + midpointtop3) / 6;

        rubble = Instantiate(newcude, center + newcenter, Quaternion.identity);
        rubble.GetComponent<meshtest>().newVertices = new Vector3[] { midpointtop - newcenter, midpointtop2 - newcenter, midpointtop3 - newcenter, midpointbot - newcenter, midpointbot2 - newcenter, midpointbot3 - newcenter };
        rubble.GetComponent<meshtest>().newTriangles = new int[] { 2, 1, 0, 3, 4, 5, 4, 3, 0, 5, 4, 1, 2, 3, 5, 2, 0, 3, 0, 1, 4, 1, 2, 5 };
        rubble.AddComponent<Rigidbody>();
        rubble.GetComponent<Rigidbody>().AddForce(Vector3.up * 500 + new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50)));
    }

    void makeList()
    {
        for (int x = 0; x < maxX+1; x++)
        {
            for (int y = 0; y < maxY+1; y++)
            {
                assocation[x, y] = new List<int>();
            }

        }
    }

    void printAsscoation()
    {
        for (int x = 0; x < maxX; x++)
        {
            for (int y = 0; y < maxY; y++)
            {
                for (int z = 0; z < assocation[x, y].Count; z++)
                {
                    Debug.Log( y + "     "  + newVertices[assocation[x, y][z]]);
                    
                }
            }

        }
    }

    void modifySame(int x, int z, Vector3 value)
    {
        for (int y = 0; y < assocation[x, z].Count; y++)
        {
            newVertices[assocation[x, z][y]] += value;
        }
    }

    void bottom(int x, int z, Vector3 value)
    {

        newVertices[assocation[x, z][0]] = value + new Vector3(Random.Range(-.5f, 0), 0, Random.Range(-.5f, 0));
        newVertices[assocation[x, z][1]] = value + new Vector3(Random.Range(-.5f, 0), 0, Random.Range(0, .5f));
        newVertices[assocation[x, z][2]] = value + new Vector3(Random.Range(0, .5f), 0, Random.Range(-.5f, 0));
        newVertices[assocation[x, z][3]] = value + new Vector3(Random.Range(0, .5f), 0, Random.Range(0, .5f));

        addTriangle(assocation[x, z][2]);
        addTriangle(assocation[x, z][0] - 1);
        addTriangle(assocation[x, z][0]);

        addTriangle(assocation[x, z][0]);
        addTriangle(assocation[x, z][1] - 1);
        addTriangle(assocation[x, z][1]);

        addTriangle(assocation[x, z][1]);
        addTriangle(assocation[x, z][3] + 3);
        addTriangle(assocation[x, z][3]);

        addTriangle(assocation[x, z][3]);
        addTriangle(assocation[x, z][2] - 1);
        addTriangle(assocation[x, z][2]);

        addTriangle(assocation[x, z][0]);
        addTriangle(assocation[x, z][1]);
        addTriangle(assocation[x, z][2]);

        addTriangle(assocation[x, z][3]);
        addTriangle(assocation[x, z][2]);
        addTriangle(assocation[x, z][1]);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "hammer" || other.tag == "grabbed" || other.tag == "bigrock" || other.tag == "def")
        {
            other.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            findClosest(other.transform.position);
            if (other.tag == "bigrock")
            {
                other.tag = "def";
            }
        }
        else if (other.tag == "enemy" && other.GetComponent<zombieEnemy>().ragdolled && tag == "wall")
        {

            findClosest(other.transform.position);
            other.tag = "cutable";
        }
        else if (other.tag == "grabbedr")
        {
            findClosest(other.transform.position);
            Instantiate(expolostion, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);

        }
    }

    private void findClosest(Vector3 p)
    {
        float dis = 10000;
        int index = 0;
        for (int i = 0; i < newVertices.Length; i++)
        {
            Vector3 v = transform.TransformPoint(newVertices[i]);
            if (Vector3.Distance(p, v) < dis)
            {
                index = i;
                dis = Vector3.Distance(p, v);
            }
        }
        breakfloor(newVertices[index].x, newVertices[index].z);
    }

    void addTriangle(int triangle)
    {
        newTriangles[currentTriangle] = triangle;
        currentTriangle++;
    }

    void drawbox()
    {
        Debug.DrawLine(transform.position, transform.TransformPoint(new Vector3(0, 0, 20)));
    }

    void OnDrawGizmos()
    {
        
            // Draws a blue line from this transform to the target
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.TransformPoint(new Vector3(0, 0, maxY)));
            Gizmos.DrawLine(transform.position, transform.TransformPoint(new Vector3(maxX, 0, 0)));
            Gizmos.DrawLine(transform.TransformPoint(new Vector3(maxX, 0, maxY)), transform.TransformPoint(new Vector3(0, 0, maxY)));
            Gizmos.DrawLine(transform.TransformPoint(new Vector3(maxX, 0, maxY)), transform.TransformPoint(new Vector3(maxX, 0, 0)));
            
    }
}
