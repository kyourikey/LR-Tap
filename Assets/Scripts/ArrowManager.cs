using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    public int Level = 0;
    public GameObject ArrowParentGameObject;
    public ArrowManager NextArrowManager;
    [SerializeField] private GameObject _arrowPrefab;
    [SerializeField] private GameObject _arrowManagerPrefab;

    enum Pattern
    {
        RLLR,
        LRRL,
    }

    private List<Arrow> _arrows = new List<Arrow>();
    private Vector3 _addPositionVector3 = new Vector3(3f, 0f, 0f);

    public void Generate(Arrow.Type arrowType)
    {
        if (_arrows.Count >= 16)
        {
            Clear();
        }

        var arrowObj = Instantiate(_arrowPrefab, ArrowParentGameObject.transform);
        arrowObj.name = arrowType.ToString();

        var arrow = arrowObj.GetComponent<Arrow>();
        arrow.ArrowType = arrowType;

        var arrowPosX = arrowType == Arrow.Type.Left ? 0f : 1f;
        var arrowPosition = new Vector3(arrowPosX, -_arrows.Count - (_arrows.Count/4), 0);
        arrowObj.transform.localPosition = arrowPosition;
        
        _arrows.Add(arrow);

        var count = _arrows.Count;
        if (count % 4 == 0)
        {
            if (_arrows[count - 4].ArrowType == Arrow.Type.Left &&
                _arrows[count - 3].ArrowType == Arrow.Type.Right &&
                _arrows[count - 2].ArrowType == Arrow.Type.Right &&
                _arrows[count - 1].ArrowType == Arrow.Type.Left)
            {
                if(NextArrowManager == null)
                    GenerateNextArrowManager();
                for (var i = 1; i <= 4; i++)
                {
                    _arrows[count - i].GetComponentInChildren<Renderer>().material.color = Color.green;
                }
                NextArrowManager.Generate(Arrow.Type.Left);
            }
            else if (_arrows[count - 4].ArrowType == Arrow.Type.Right &&
                _arrows[count - 3].ArrowType == Arrow.Type.Left &&
                _arrows[count - 2].ArrowType == Arrow.Type.Left &&
                _arrows[count - 1].ArrowType == Arrow.Type.Right)
            {
                if (NextArrowManager == null)
                    GenerateNextArrowManager();
                for (var i = 1; i <= 4; i++)
                {
                    _arrows[count - i].GetComponentInChildren<Renderer>().material.color = Color.green;
                }
                NextArrowManager.Generate(Arrow.Type.Right);
            }
            else
            {
                for (var i = 1; i <= 4; i++)
                {
                    _arrows[count - i].GetComponentInChildren<Renderer>().material.color = Color.red;
                }
            }
        }
    }

    public void Clear()
    {
        _arrows.Clear();
        foreach (Transform arrowObj in ArrowParentGameObject.transform)
        {
            Destroy(arrowObj.gameObject);
        }
    }

    public void Reset()
    {
        Clear();
        if(NextArrowManager != null)
            NextArrowManager.Reset();
    }

    private void GenerateNextArrowManager()
    {
        var nextArrowManagerObj = Instantiate(_arrowManagerPrefab);
        var arrowManager = nextArrowManagerObj.GetComponent<ArrowManager>();
        arrowManager.Level = Level + 1;
        nextArrowManagerObj.name = "ArrowManager_Level" + arrowManager.Level;
        var newParentObject = new GameObject("ParentObject_Level" + arrowManager.Level);
        newParentObject.transform.position = ArrowParentGameObject.transform.position + _addPositionVector3;
        arrowManager.ArrowParentGameObject = newParentObject;
        NextArrowManager = arrowManager;
    }
}
