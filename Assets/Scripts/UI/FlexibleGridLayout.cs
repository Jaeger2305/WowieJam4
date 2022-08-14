using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlexibleGridLayout : LayoutGroup
{
    //Credit: GameDevGuide
    //https://www.youtube.com/watch?v=CGsEJToeXmA

    public enum FitType
    {
        Uniform,
        Width,
        Height,
        FixedRows,
        FixedColumns
    }

    public FitType LayoutFitType;
    public int Rows;
    public int Columns;
    public Vector2 CellSize;
    public Vector2 Spacing;

    bool fitX;
    bool fitY;

    public override void CalculateLayoutInputVertical()
    {
        //Determine Grid & Cell Dimensions
        int cellCount = transform.childCount;
        
        float sqrt = Mathf.Sqrt(cellCount);

        //Determine Row/Col Count by FitType
        switch (LayoutFitType) {
            case FitType.Uniform:
                Rows = Mathf.CeilToInt(sqrt);
                Columns = Mathf.CeilToInt(sqrt);
                fitX = true;
                fitY = true;
                break;
            case FitType.Width:
                Columns = Mathf.CeilToInt(sqrt);
                Rows = Mathf.CeilToInt(cellCount / (float)Columns);
                fitX = true;
                fitY = true;
                break;
            case FitType.Height:
                Rows = Mathf.CeilToInt(sqrt);
                Columns = Mathf.CeilToInt(cellCount / (float)Rows);
                fitX = true;
                fitY = true;
                break;
            case FitType.FixedRows:
                Columns = Mathf.CeilToInt(cellCount / (float)Rows);
                break;
            case FitType.FixedColumns:
                Rows = Mathf.CeilToInt(cellCount / (float)Columns);
                break;
            default:
                break;
        }

        float gridWidth = rectTransform.rect.width;
        float gridHeight = rectTransform.rect.height;

        //Credit to youtuber Connor King for correct spacing calculation 
        float cellWidth = (gridWidth / (float)Columns) - ((Spacing.x / (float)Columns) * (Columns - 1)) - (padding.left / (float)Columns) - (padding.right / (float)Columns);
        float cellHeight = (gridHeight / (float)Rows) - ((Spacing.y / (float)Rows) * (Rows -1)) - (padding.top / (float) Rows) - (padding.bottom / (float) Rows);

        //Adjust for fit
        cellWidth = fitX ? cellWidth : CellSize.x;
        cellHeight = fitY ? cellHeight : CellSize.y;

        CellSize = new Vector2(cellWidth, cellHeight);

        int rowIndex = 0;
        int colIndex = 0;

        //Set Layout
        for (int i = 0; i < cellCount; i++) {
            rowIndex = i / Columns;
            colIndex = i % Columns;

            var item = rectChildren[i];

            var xpos = (CellSize.x * colIndex) + (Spacing.x * colIndex) + padding.left;
            var ypos = (CellSize.y * rowIndex) + (Spacing.y * rowIndex) + padding.top;

            SetChildAlongAxis(item, 0, xpos, CellSize.x);
            SetChildAlongAxis(item, 1, ypos, CellSize.y);
        }
    }

    public override void SetLayoutHorizontal()
    {
     
    }

    public override void SetLayoutVertical()
    {

    }
}
