using System.Collections.Generic;
using UnityEngine;

public class procedural_geo : MonoBehaviour
{
    private void Awake()
    {
        Mesh mesh = new Mesh();
        mesh.name = "procQuad";

        //step 1) create points
        List<Vector3> points = new List<Vector3>()
        {
            new Vector3(-1, 1), //pt 0
            new Vector3(1, 1), //pt 1
            new Vector3(-1, -1), //pt 2
            new Vector3(1, -1)  //pt 3
        };

        //step2) create triangle from points
        int[] triIndices = new int[]{2,1,0,/*<--upper triangle*/ 2,3, 1 /*<--lower traignle*/};


        mesh.SetVertices(points);
        mesh.triangles = triIndices;

        //step2) set normals
        List<Vector3> normals = new List<Vector3>()
        {
            new Vector3(0,0,1),
            new Vector3(0,0,1),
            new Vector3(0,0,1),
            new Vector3(0,0,1)
        };
        mesh.SetNormals(normals);




        Debug.Log("triangle count = " + mesh.triangles.Length/3);
        Debug.Log("vert count = " + mesh.vertexCount);

        GetComponent<MeshFilter>().mesh = mesh;




        //we need to make UVs now
        //see video at 1:00:26


    }

}
