using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private ArrowManager _arrowManager;

    void Start()
    {
        _inputManager.IsUpdate.Skip(1).Where(x => x).Subscribe(_ =>
        {
            switch (_inputManager.LastInput)
            {
                case InputManager.InputType.Left:
                    _arrowManager.Generate(Arrow.Type.Left);
                    break;
                case InputManager.InputType.Right:
                    _arrowManager.Generate(Arrow.Type.Right);
                    break;
                case InputManager.InputType.None:
                    _arrowManager.Reset();
                    break;
            }
        }).AddTo(this);
    }
}
