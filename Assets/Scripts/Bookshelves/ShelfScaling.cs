using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShelfScaling : MonoBehaviour
{
    public int columns = 2;
    public int rows = 3;
    public Vector2 spacingScale = new Vector2(0.026f, 0.005f);
    public float heightScale = 0.1f;
    public float widthScale = 0.03f;

    private GridLayoutGroup grid;
    private RectTransform rect;

    void Awake()
    {
        grid = GetComponent<GridLayoutGroup>();
        rect = GetComponent<RectTransform>();

        UpdateGrid();
    }

    void Update()
    {
        UpdateGrid();
    }

    void UpdateGrid()
    {
        
        float spacingX = rect.rect.width * spacingScale.x;
        float spacingY = rect.rect.height * spacingScale.y;
        grid.spacing = new Vector2(spacingX, spacingY);

        int paddingWidth = Mathf.RoundToInt(rect.rect.width * widthScale);

        int paddingHeight = Mathf.RoundToInt(rect.rect.height * heightScale);

        grid.padding = new RectOffset
        (
            paddingWidth, //left
            paddingWidth, // right
            paddingHeight, //top
            paddingHeight //bottom
        );


        float totalSpacingX = spacingX * (columns - 1) + grid.padding.left + grid.padding.right;

        float availableWidth = rect.rect.width - totalSpacingX;

        float cellWidth = availableWidth / columns;

        float totalSpacingY = spacingY * (rows - 1) + grid.padding.top + grid.padding.bottom;

        float availableHeight = rect.rect.height - totalSpacingY;

        float cellHeight = availableHeight / rows;

        grid.cellSize = new Vector2(cellWidth, cellHeight);
    }
     
}
