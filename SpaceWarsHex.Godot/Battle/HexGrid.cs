using Godot;
using SpaceWarsHex.Bridges;
using System;
using System.Linq;

public partial class HexGrid : MeshInstance2D
{
    private const float GridCutoffSize = 30f;
    private const float GridOpacityScale = 35f;

#pragma warning disable CS8618 // These will be assigned to in the editor or in _Ready(), if not then something went wrong and any resulting exceptions should be thrown.
    private Camera2D _camera;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    private Rect2 _lastRect;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _camera = GetNode<Camera2D>("../MainCamera");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        RecalcGrid();
    }

    private void RecalcGrid()
    {
        var viewport = _camera.GetViewport();
        var transform = viewport.CanvasTransform.AffineInverse();
        var viewRect = transform * viewport.GetVisibleRect();

        if (viewRect != _lastRect)
        {
            if (viewRect.Size.X < GridCutoffSize * HexMath.Default.R * 2)
            {
                var p = viewRect.Position; // transform * Vector2.Zero;
                var v = viewRect.End; // transform * viewRect.End;

                float deltaX = Mathf.Abs(v.X - p.X);
                float deltaY = Mathf.Abs(v.Y - p.Y);

                int cols = Convert.ToInt32(Mathf.Ceil(deltaX / HexMath.Default.R3)) + 2; // padding
                int rows = Convert.ToInt32(Mathf.Ceil(deltaY / HexMath.Default.H2)) + 2;

                // snap p to closest hex
                p = HexMath.Default.VectorToHexVector2(p).ToVector2();
                float x = p.X - HexMath.Default.R3;
                float y = p.Y - HexMath.Default.H1;
                Position = new Vector2(x, y);

                GenerateMesh(cols, rows, -HexMath.Default.R, -HexMath.Default.H1);
                _lastRect = viewRect;
            }
        }
    }

    private void GenerateMesh(int cols, int rows, float baseX, float baseY)
    {
        // allocate space for varriables before loops
        int height = rows * 2 + 1;
        int width = cols + 1;
        int vertCount = height * width;
        int curVertex = 0;
        int curIndex = 0;

        bool even;

        float x1;
        float x2;
        float y;

        Godot.Collections.Array surfaceArray = [];
        surfaceArray.Resize((int)Mesh.ArrayType.Max);
        var verts = new Vector2[vertCount];
        var lineCount = width * (height - 1) + (int)Mathf.Ceil(cols * (rows + 0.5f));
        var indices = new int[lineCount * 2];

        // do first strip
        x1 = baseX;
        x2 = x1 + HexMath.Default.R2;
        y = baseY;
        verts[curVertex++] = new Vector2(x2, y);
        y += HexMath.Default.H1;
        for (int i = 0; i < rows; i++)
        {
            indices[curIndex++] = curVertex - 1;
            indices[curIndex++] = curVertex;
            verts[curVertex++] = new Vector2(x1, y);
            y += HexMath.Default.H1;

            indices[curIndex++] = curVertex - 1;
            indices[curIndex++] = curVertex;
            verts[curVertex++] = new Vector2(x2, y);
            y += HexMath.Default.H1;
        }

        for (int j = 1; j < width; j++)
        {
            even = (j % 2 == 0);

            if (even)
            {
                x1 = baseX + (j * HexMath.Default.R3);
                x2 = x1 + HexMath.Default.R2;
            }
            else
            {
                x2 = baseX + (j * HexMath.Default.R3);
                x1 = x2 + HexMath.Default.R2;
                indices[curIndex++] = curVertex - height;
                indices[curIndex++] = curVertex;
            }
            y = baseY;

            verts[curVertex++] = new Vector2(x2, y);
            y += HexMath.Default.H1;

            for (int i = 0; i < rows; i++)
            {
                if (even)
                {
                    indices[curIndex++] = curVertex - height;
                    indices[curIndex++] = curVertex;
                }
                indices[curIndex++] = curVertex - 1;
                indices[curIndex++] = curVertex;
                verts[curVertex++] = new Vector2(x1, y);
                y += HexMath.Default.H1;

                if (!even)
                {
                    indices[curIndex++] = curVertex - height;
                    indices[curIndex++] = curVertex;
                }
                indices[curIndex++] = curVertex - 1;
                indices[curIndex++] = curVertex;
                verts[curVertex++] = new Vector2(x2, y);
                y += HexMath.Default.H1;
            }
        }

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
