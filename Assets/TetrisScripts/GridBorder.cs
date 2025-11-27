using UnityEngine;

public class GridBorder : MonoBehaviour
{
    public int width = 10;
    public int height = 20;
    public Material borderMaterial;

    void OnPostRender()
    {
        if (!borderMaterial) return;

        borderMaterial.SetPass(0);

        GL.Begin(GL.LINES);
        GL.Color(Color.black);

        // Izquierda
        GL.Vertex3(0, 0, 0);
        GL.Vertex3(0, height, 0);

        // Derecha
        GL.Vertex3(width, 0, 0);
        GL.Vertex3(width, height, 0);

        // Abajo
        GL.Vertex3(0, 0, 0);
        GL.Vertex3(width, 0, 0);

        // Arriba
        GL.Vertex3(0, height, 0);
        GL.Vertex3(width, height, 0);

        GL.End();
    }
}
