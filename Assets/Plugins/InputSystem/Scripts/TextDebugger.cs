using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDebugger : MonoBehaviour
{
    [SerializeField] private Text mText = default;

    [SerializeField] private Color mTextColor = Color.black;
    [SerializeField] private Color mErrorColor = Color.red;
    [SerializeField] private Color mWarningColor = Color.yellow;

    private static TextDebugger mInstance;

    public static TextDebugger Instance
    {
        get
        {
            if (mInstance == null)
            {
                var debuggers = FindObjectsOfType<TextDebugger>();
                if (debuggers.Length == 0)
                    Debug.LogError("Text debugger does not exist in scene. Create one");
                else if (debuggers.Length > 1)
                    Debug.LogError("There are more then one text debuggers in scene. Remove all but one");
                else
                    mInstance = debuggers[0];
            }

            return mInstance;
        }
    }

    private void Awake()
    {
        mText.color = mTextColor;
    }

    public void Log(string str)
    {
        mText.text += str + Environment.NewLine;
    }

    public void LogWarning(string str)
    {
        mText.text += $"<color=#{ColorUtility.ToHtmlStringRGB(mWarningColor)}>{str}</color>{Environment.NewLine}";
    }

    public void LogError(string str)
    {
        mText.text += $"<color=#{ColorUtility.ToHtmlStringRGB(mErrorColor)}>{str}</color>{Environment.NewLine}";
    }

    public void Clear()
    {
        mText.text = string.Empty;
    }
}
