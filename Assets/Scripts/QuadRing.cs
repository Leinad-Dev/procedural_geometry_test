using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadRing : MonoBehaviour
{
    [Range(0.01f, 1f)]
    [SerializeField] float radiusInner = .5f;

    [Range(0.01f, 2f)]
    [SerializeField] float thickness = 1f;

    float RadiusOuter => radiusInner + thickness;

    [Range(3, 100)]
    [SerializeField] int subdivision = 3;

    private void OnDrawGizmosSelected()
    {
        DrawWireCircle(transform.position, transform.rotation, 1);
       /* Gizmos.DrawWireSphere(transform.position,radiusInner);
        Gizmos.DrawWireSphere(transform.position, RadiusOuter);*/
    }

    const float TAU = 6.28318530718f; //circle constant in radians circumfrance = TAU*radius
    private void DrawWireCircle(Vector3 pos, Quaternion rot, float radius, int detail = 32)
    {
        Vector3[] points3D = new Vector3[detail]; //initialize vector 3d size
        for (int i =0; i<detail; i++)
        {
            float t = i / (float)detail; //get percentage complete of circle
            float angleRadian = t * TAU; //get the radian equivilent of the %

            Vector2 points2D = new Vector2(
                Mathf.Cos(angleRadian),//cos(y) to get x coordinate at the current radian point
                Mathf.Sin(angleRadian) //sin(x) to get y coordinate at the current radian point
                );

            //now that we have our x and y 2d values for the given radian, we now need to translate the info into 3D vector space
            points2D *= radius; //convert 2D points to our desired radius
            points3D[i] = pos + (rot * points2D); //rot is defined as Quaternion, multiply 2D point to it to convert the 2D to 3D space
            //timestamp 1:38:18

        }

        //Now go through all of our 3D points and draw a sphere is their position
        for (int i = 0; i < detail; i++)
        {
            Gizmos.DrawSphere(points3D[i],0.01f);
            //timestamp 1:38:18

        }


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //timestamp 1:38:18
}
