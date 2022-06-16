using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class CustomGrid<TGridObject>
{
    public const int HEATMAP_MAX_VALUE = 100;
    public const int HEATMAP_MIN_VALUE = 0;

    public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChange;
    public class OnGridValueChangedEventArgs : EventArgs
    {
        public int x, y;
    }

    int m_width;
    int m_height;
    float m_cellSize;
    Vector2 m_originPosition;
    TGridObject[,] m_gridArray;

    public CustomGrid(int width, int height, float cellSize, Vector2 originPos, Func< CustomGrid<TGridObject>, int, int, TGridObject> createGridObject)
    {
        this.m_width = width;
        this.m_height = height;
        this.m_cellSize = cellSize;
        this.m_originPosition = originPos;

        m_gridArray = new TGridObject[width, height];

        for (int x = 0; x < m_gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < m_gridArray.GetLength(1); y++)
            {
                m_gridArray[x, y] = createGridObject(this, x, y);
            }
        }

        bool showDebugInfo = true;
        if (showDebugInfo == false)
            return;

        TextMesh[,] debugText = new TextMesh[width, height];

        for (int x = 0; x < m_gridArray.GetLength(0); x++) {
            for (int y = 0; y < m_gridArray.GetLength(1); y++) {
                Vector2 textPos = GetWorldPosition(x, y) + new Vector2(cellSize, cellSize) * 0.5f;
                debugText[x, y] = UtilsClass.CreateWorldText(m_gridArray[x, y]?.ToString(), null, textPos, 20, Color.white, TextAnchor.MiddleCenter);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 60.0f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 60.0f);
            }
        }

        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 60.0f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 60.0f);

        OnGridValueChange += (object sender, OnGridValueChangedEventArgs eventArgs) =>
        {
            debugText[eventArgs.x, eventArgs.y].text = m_gridArray[eventArgs.x, eventArgs.y]?.ToString();
        };
    }

    public int GetWidth()
    {
        return m_width;
    }

    public int GetHeight()
    {
        return m_height;
    }

    public float GetCellSize()
    {
        return m_cellSize;
    }

    public Vector2 GetWorldPosition(int x, int y)
    {
        return new Vector2(x, y) * m_cellSize + m_originPosition;
    }

    public void GetXY(Vector2 worldPos, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPos - m_originPosition).x / m_cellSize);
        y = Mathf.FloorToInt((worldPos - m_originPosition).y / m_cellSize);
    }

    public void TriggerGridObjectChange(int x, int y)
    {
        if (OnGridValueChange != null)
            OnGridValueChange(this, new OnGridValueChangedEventArgs { x = x, y = y });
    }

    public void SetGridObject(int x, int y, TGridObject value)
    {
        if ( x >= 0 && y >= 0 && x < m_width && y < m_height) {
            m_gridArray[x, y] = value;
            if (OnGridValueChange != null)
                OnGridValueChange(this, new OnGridValueChangedEventArgs { x = x, y = y });
        }
    }

    public void SetGridObject(Vector2 worldPos, TGridObject value)
    {
        int x, y;
        GetXY(worldPos, out x, out y);
        SetGridObject(x, y, value);
    }

    public TGridObject GetGridObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < m_width && y < m_height)
        {
            return m_gridArray[x, y];
        }
        else
        {
            return default(TGridObject);
        }
    }

    public TGridObject GetGridObject(Vector2 worldPos)
    {
        int x, y;
        GetXY(worldPos, out x, out y);
        return GetGridObject(x, y);
    }
}
