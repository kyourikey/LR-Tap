using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public enum Type
    {
        Left,
        Right
    }

    public ReactiveProperty<Type> ArrowType = new ReactiveProperty<Type>();

    [SerializeField]
    private GameObject _arrowGameObject;

    void Start()
    {
        ArrowType.DistinctUntilChanged().Subscribe(x =>
        {
            _arrowGameObject.transform.rotation = Quaternion.Euler(0f, x == Type.Left ? 0f : 180f, 0f);
        });
    }
}
