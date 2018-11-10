using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class InputManager : MonoBehaviour
{
    public enum InputType
    {
        Left,
        Right,
        None
    }

    public ReactiveProperty<bool> IsUpdate = new ReactiveProperty<bool>(false);
    
    private List<InputType> _inputData = new List<InputType>();
    public List<InputType> InputData
    {
        get { return _inputData; }
    }

    public InputType LastInput
    {
        get
        {
            if (_inputData.Count > 0)
                return _inputData.Last();

            return InputType.None;
        }
    }
    
    void Update()
    {
        if (GetLeftInput())
        {
            _inputData.Add(InputType.Left);
            IsUpdate.Value = true;
        }
        else if (GetRightInput())
        {
            _inputData.Add(InputType.Right);
            IsUpdate.Value = true;
        }
        else if (GetSpaceInput())
        {
            ResetInputData();
            IsUpdate.Value = true;
        }
        else
        {
            IsUpdate.Value = false;
        }

        //debug input clear
        if (Input.GetKeyDown(KeyCode.Space))
            ResetInputData();
    }

    public void ResetInputData()
    {
        _inputData.Clear();
    }

    private bool GetLeftInput()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        return Input.GetKeyDown(KeyCode.LeftArrow);
#endif

        return false; //safety
    }

    private bool GetRightInput()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        return Input.GetKeyDown(KeyCode.RightArrow);
#endif

        return false; //safety
    }
    
    private bool GetSpaceInput()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        return Input.GetKeyDown(KeyCode.Space);
#endif

        return false; //safety
    }
}
