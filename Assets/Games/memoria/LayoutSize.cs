using UnityEngine;
using UnityEngine.UI;

public class LayoutSize : MonoBehaviour
{
    public GridLayoutGroup gridLayoutGroup;
    public void ModificarGridLayoutProperties2(){
         // Establecer el tamaño de celda
        Vector2 cellSize = new Vector2(150f, 150f);
        
        // Establecer el espaciado
        Vector2 spacing = new Vector2(0f, 0f);
        
        // Establecer el número de columnas
        int numColumnas = 2;

        // Establecer las propiedades del GridLayoutGroup
        gridLayoutGroup.cellSize = cellSize;
        gridLayoutGroup.spacing = spacing;
        gridLayoutGroup.constraintCount = numColumnas;
    }
    public void ModificarGridLayoutProperties3()
    {
        // Establecer el tamaño de celda
        Vector2 cellSize = new Vector2(150f, 150f);
        
        // Establecer el espaciado
        Vector2 spacing = new Vector2(-20f, 10f);
        
        // Establecer el número de columnas
        int numColumnas = 3;

        // Establecer las propiedades del GridLayoutGroup
        gridLayoutGroup.cellSize = cellSize;
        gridLayoutGroup.spacing = spacing;
        gridLayoutGroup.constraintCount = numColumnas;
    }
    public void ModificarGridLayoutProperties4()
    {
        // Establecer el tamaño de celda
        Vector2 cellSize = new Vector2(130f, 130f);
        
        // Establecer el espaciado
        Vector2 spacing = new Vector2(-25f, 10f);
        
        // Establecer el número de columnas
        int numColumnas = 4;

        // Establecer las propiedades del GridLayoutGroup
        gridLayoutGroup.cellSize = cellSize;
        gridLayoutGroup.spacing = spacing;
        gridLayoutGroup.constraintCount = numColumnas;
    }
}

