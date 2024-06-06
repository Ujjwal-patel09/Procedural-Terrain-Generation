using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class procedural_terrain : MonoBehaviour
{
   Mesh mesh;

   Vector3[] vertices;
   int[] triangles;

   public int xSize = 10;
   public int zSize = 10;

   private void Start()
   {
     mesh = new Mesh();
     GetComponent<MeshFilter>().mesh = mesh;

     create_shape();
     Update_shape();    
   }
   
   // creating mesh //
   void create_shape()
   {  
    // creating vertices//
      vertices = new Vector3[(xSize+1)*(zSize+1)];

      for (int i = 0, z = 0; z <= zSize; z++)
      {
        for (int x = 0; x <= xSize; x++)
        {
            float y = Mathf.PerlinNoise(x*.3f,z*.3f)*2f;
            vertices[i] = new Vector3(x,y,z);
            i++;
        }
      }

    // creating triangles //
      triangles = new int[xSize*zSize*6];
      int verts = 0;
      int tris  = 0;

      for (int z = 0; z < zSize; z++)
      {
        for (int x = 0; x < xSize; x++)
        {
        
          triangles[tris+0] = verts + 0;
          triangles[tris+1] = verts + zSize + 1;
          triangles[tris+2] = verts + 1;
          triangles[tris+3] = verts + 1;
          triangles[tris+4] = verts + zSize + 1;
          triangles[tris+5] = verts + zSize + 2;
        
          verts++;
          tris += 6;
        }
        verts++;
      }
   }

   // updating mesh into meshfilter //
   void Update_shape()
   {
     mesh.Clear();

     mesh.vertices = vertices;
     mesh.triangles = triangles;

     mesh.RecalculateNormals();
   }
   
}
