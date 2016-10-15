using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class BoardView : MonoBehaviour
{
    public const int LONG_SIDE = 4;
    public const int SHORT_SIDE = 2;
    public const int LONG_SIDE_COUNT = 2;
    public const int SHORT_SIDE_COUNT = 1;

    public GameObject FieldViewPrefab;
    public int DistanceFactor = 1;
    public bool Refresh;

    private List<GameObject> _fieldViews = new List<GameObject>();
    private int _distanceCache = int.MinValue;
	
    public int FieldCount { get { return GameConstants.PLAYER_AMOUNT * GameConstants.FIELD_AMOUNT_PER_PLAYER; } }

    void Update()
    {
#if UNITY_EDITOR
        if(!Application.isPlaying)
        {
            if (Refresh)
            {
                Refresh = false;
                for (int i = _fieldViews.Count - 1; i >= 0; i--)
                {
                    DestroyImmediate(_fieldViews[i]);
                    _fieldViews.RemoveAt(i);
                }
            }
            if (_fieldViews.Count != FieldCount || _distanceCache != DistanceFactor)
            {
                DrawBoard();
            }
        }
#endif
    }

    public void DrawBoard()
    {
        if(FieldViewPrefab == null) return;

        //Create the correct amount of fields:
        for (int i = _fieldViews.Count; i < FieldCount; i++)
        {
            _fieldViews.Add(InstantiateField());       
        }
        for(int i = _fieldViews.Count-1; i >= FieldCount ; i--)
        {
            DestroyImmediate(_fieldViews[i]);
            _fieldViews.RemoveAt(i);
        }

        var x = 0;
        var y = -5;
        var xDir = FieldCount / 4;
        var xFactor = -1;
        var yDir = 0;
        var yFactor = 1;
        var increasingX = true;
        var fieldsOnSide = 1;
        var sideCount = 0;
        var longSide = false;
        //Set positions:
        for (int i = 0; i < FieldCount; i++)
        {
            x += increasingX ? xFactor : 0;
            y += !increasingX ? yFactor : 0;

            _fieldViews[i].transform.position = new Vector3(x * DistanceFactor, 0, y* DistanceFactor);
            _fieldViews[i].name = string.Format("field_{0}", i);

            xDir = (xDir + 1) % (FieldCount / 2);
            yDir = (yDir + 1) % (FieldCount / 2);
            xFactor = xFactor * (xDir == 0 ? -1 : 1);
            yFactor = yFactor * (yDir == 0 ? -1 : 1);

            fieldsOnSide = (fieldsOnSide + 1) % (longSide ? LONG_SIDE : SHORT_SIDE);

            if(fieldsOnSide == 0)
            {
                increasingX = !increasingX;
                sideCount = (sideCount+1) % (longSide ? LONG_SIDE_COUNT : SHORT_SIDE_COUNT);
                if(sideCount == 0)
                {
                    longSide = !longSide;
                }
            }         
        }

        var lineRenderer = GetComponent<LineRenderer>();
        if(lineRenderer)
        {
            lineRenderer.SetVertexCount(FieldCount+1);
            for (int i = 0; i < FieldCount; i++)
            {
                lineRenderer.SetPosition(i, _fieldViews[i].transform.localPosition);
            }
            lineRenderer.SetPosition(FieldCount, _fieldViews[0].transform.localPosition);
        }
    }

    private GameObject InstantiateField()
    {
        var go = Instantiate(FieldViewPrefab);
        go.transform.parent = transform;
        go.transform.localScale = Vector3.one;
        return go;
    }
}
