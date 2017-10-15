/*
* Copyright 2017 sheng chongshan. All rights reserved.
* email: shany.sheng@qq.com
* License: https://github.com/shanysheng/RenderPipeline/blob/master/LICENSE
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ToJsonMesh
{
    public enum MESH_COMPONENT_TYPE
    {
        MESH_COMPONENT_POSITION = 1<<1,
        MESH_COMPONENT_COLOR    = 1<<2,
        MESH_COMPONENT_NORMAL   = 1<<3,
        MESH_COMPONENT_TANGENT  = 1<<4,
        MESH_COMPONENT_UV0      = 1<<5,
        MESH_COMPONENT_UV1      = 1<<6,
        MESH_COMPONENT_UV2      = 1<<7,
        MESH_COMPONENT_UV3      = 1<<8,
    }

    public enum MESH_VERTEX_TYPE
    {
        MESH_VERTEX_STATIC_PCNTUV2,
        MESH_VERTEX_SKINNED_PCNTUV2
    }

    public struct StaticVertex_PCNTUV2
    {
        public Vector3 pos;
        public Vector4 color;
        public Vector3 normal;
        public Vector4 tangent;
        public Vector2 uv0;
        public Vector2 uv1;
    }

    public struct SkinnedVertex_PCNTUV2
    {
        public Vector3 pos;
        public Vector4 color;
        public Vector3 normal;
        public Vector4 tangent;
        public Vector2 uv0;
        public Vector2 uv1;
        public Vector4 boneIndices;
        public Vector4 boneWeights;
    }

    public class MeshPiece
    {
        public string pieceName;

        public Int32 vertexType;
        public Int32 vertexStride;
        public Int32 primitiveType;
        public Int32 compressType;
        public Int32 vertexCount;
        public Int32 IndexCount;
        public byte[] reserved; // 128 bytes reserved
        public float[] vertexArray;
        public Int16[] indexArray;  
    }

    public class MeshFile
    {
        public Int32 magicID;
        public Int32 version;
        public Int32 fileSize;
        public MeshPiece piece;
    }

    public static void Export(ToJsonContext context, Mesh mesh, string fullpath)
    {
        Bounds bounds = mesh.bounds;

        Vector3[] vertices = mesh.vertices;
        int[] indices = mesh.triangles;
        Color[] colors = mesh.colors;
        Color32[] colors32 = mesh.colors32;
        Vector3[] normals = mesh.normals;
        Vector4[] tangents = mesh.tangents;
        Vector2[] uv = mesh.uv;
        Vector2[] uv2 = mesh.uv2;
        Vector2[] uv3 = mesh.uv3;
        Vector2[] uv4 = mesh.uv4;

        int vertexcount = (vertices == null) ? 0 : vertices.Length;
        int indexcount = (indices == null) ? 0 : indices.Length;
        int colorcount = (colors == null) ? 0 : colors.Length;
        int color32count = (colors32 == null) ? 0 : colors32.Length;
        int normalcount = (normals == null) ? 0 : normals.Length;
        int tangentcount = (tangents == null) ? 0 : tangents.Length;
        int uvcount = (uv == null) ? 0 : uv.Length;
        int uv2count = (uv2 == null) ? 0 : uv2.Length;
        int uv3count = (uv3 == null) ? 0 : uv3.Length;
        int uv4count = (uv4 == null) ? 0 : uv4.Length;

        Debug.LogFormat("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}\n", vertexcount, indexcount, colorcount, color32count, normalcount, tangentcount, uvcount, uv2count, uv3count, uv4count);

        bool bhascolor = (colors32 == null) ? false : true;
        bool bhasnormal = (normals == null) ? false : true;
        bool bhastangent = (tangents == null) ? false : true;
        bool hasuv = (uv == null) ? false : true;
        bool hasuv2 = (uv2 == null) ? false : true;
        bool hasuv3 = (uv3 == null) ? false : true;
        bool hasuv4 = (uv4 == null) ? false : true;

        MeshFile meshfile = new MeshFile();
        //meshfile.magicID = ExporterUtility.StringToMagicID("st3d");
        meshfile.version = 0x1000;

        MeshPiece piece = new MeshPiece();
        meshfile.piece = piece;

        int stride = 18 * sizeof(float);

        piece.vertexType = (Int32)MESH_VERTEX_TYPE.MESH_VERTEX_STATIC_PCNTUV2;
        piece.vertexStride = stride;
        piece.primitiveType = 0;
        piece.compressType = 0;
        piece.vertexCount = mesh.vertices.Length;
        piece.IndexCount = mesh.triangles.Length;
        piece.reserved = new byte[512];
        piece.vertexArray = new float[mesh.vertices.Length * stride / 4];
        piece.indexArray = new Int16[mesh.triangles.Length];

        int floatstride = piece.vertexStride / 4;
        for (int i = 0; i < mesh.vertices.Length; ++i)
        {
            piece.vertexArray[i * floatstride] = mesh.vertices[i].x;
            piece.vertexArray[i * floatstride + 1] = mesh.vertices[i].x;
            piece.vertexArray[i * floatstride + 2] = mesh.vertices[i].x;

            if (hasuv)
            {
                piece.vertexArray[i * floatstride + 3] = mesh.uv[i].x;
                piece.vertexArray[i * floatstride + 4] = mesh.uv[i].x;
            }

            if (bhasnormal)
            {
                piece.vertexArray[i * floatstride + 5] = mesh.normals[i].x;
                piece.vertexArray[i * floatstride + 6] = mesh.normals[i].y;
                piece.vertexArray[i * floatstride + 7] = mesh.normals[i].z;
            }
        }

        for (int i = 0; i < piece.indexArray.Length; ++i)
        {
            piece.indexArray[i] = (Int16)mesh.triangles[i];
        }


        // write mesh buffer to file
        using (FileStream file = new FileStream(fullpath, FileMode.Create))
        {
            using (BinaryWriter writer = new BinaryWriter(file))
            {
                writer.Write(meshfile.magicID);
                writer.Write(meshfile.version);
                writer.Write(meshfile.fileSize);

                writer.Write(meshfile.piece.pieceName.Length);
                byte[] byteArray = System.Text.Encoding.Default.GetBytes(meshfile.piece.pieceName);
                writer.Write(byteArray);

                MeshPiece meshpiece = meshfile.piece;
                writer.Write(meshpiece.vertexType);
                writer.Write(meshpiece.vertexStride);
                writer.Write(meshpiece.primitiveType);
                writer.Write(meshpiece.compressType);
                writer.Write(meshpiece.vertexCount);
                writer.Write(meshpiece.IndexCount);
                writer.Write(meshpiece.reserved);

                byteArray = new byte[meshpiece.vertexArray.Length * sizeof(float)];
                Buffer.BlockCopy(meshpiece.vertexArray, 0, byteArray, 0, byteArray.Length);
                writer.Write(byteArray);

                byteArray = new byte[meshpiece.indexArray.Length * sizeof(Int16)];
                Buffer.BlockCopy(meshpiece.indexArray, 0, byteArray, 0, byteArray.Length);
                writer.Write(byteArray);
            }
        }
    }
}