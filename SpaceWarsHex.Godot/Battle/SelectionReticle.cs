using Godot;
using GDC = Godot.Collections;
using SpaceWarsHex.Bridges;
using System;
using System.Linq;
using SpaceWarsHex.Model;

namespace SpaceWarsHex.Godot
{

    public partial class SelectionReticle : MeshInstance2D
    {
        private const float LineLength = 0.3f;
        private const float RadiusMultiplier = 1.1f;

        private HexVector2 _hexPosition;
        public HexVector2 HexPosition
        {
            get => _hexPosition;
            set
            {
                if (_hexPosition != value)
                {
                    _hexPosition = value;
                    Position = _hexPosition.ToVector2();
                }
            }
        }

        public override void _Ready()
        {
            GenerateMesh();
        }

        private void GenerateMesh()
        {
            var verts = new Vector2[18];

            // calculate corners first
            for (int i = 0; i < 6; i++)
            {
                verts[i * 3] = HexMath.Default.HexagonVertex(i) * RadiusMultiplier;
            }

            float oneMinusL = 1f - LineLength;

            // calculate other points based on corners
            for (int i = 0; i < 6; i++)
            {
                var corner_i = i * 3;
                var next_i = (corner_i + 3) % 18;
                var corner = verts[corner_i];
                var next = verts[next_i];

                var d = next - corner;

                verts[corner_i + 1] = corner + (d * LineLength);
                verts[corner_i + 2] = corner + (d * oneMinusL);
            }

            var indices = new int[24];
            for (int i = 0; i < 6; i++)
            {
                var j = i * 3;
                var k = i * 4;
                indices[k] = j;
                indices[k + 1] = (j + 1) % 18;
                indices[k + 2] = j;
                indices[k + 3] = (j + 17) % 18;
            }

            GDC.Array surfaceArray = [];
            surfaceArray.Resize((int)Mesh.ArrayType.Max);
            surfaceArray[(int)Mesh.ArrayType.Vertex] = verts.ToArray();
            //surfaceArray[(int)Mesh.ArrayType.TexUV] = null;
            //surfaceArray[(int)Mesh.ArrayType.Normal] = null;
            surfaceArray[(int)Mesh.ArrayType.Index] = indices.ToArray();

            var arrMesh = Mesh as ArrayMesh;
            if (arrMesh != null)
            {
                arrMesh.ClearSurfaces();
                // Create mesh surface from mesh array
                // No blendshapes, lods, or compression used.
                arrMesh.AddSurfaceFromArrays(Mesh.PrimitiveType.Lines, surfaceArray);
            }
        }
    }
}
