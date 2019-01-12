using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AnimationChangeTool : EditorWindow
{

    private static List<AnimationClip> allAnimatinClips = new List<AnimationClip>();
    private float addvalue;
    //private static float curAniValue;

    [MenuItem("Tools/AnimationChangeTool")]
    static void ShowWindow()
    {
        var window = (AnimationChangeTool)GetWindow(typeof(AnimationChangeTool));
        window.maxSize = new Vector2(200, 250);
        window.Show();
        //GetCurKeys();
    }

    void ClearAll()
    {
        allAnimatinClips.Clear();
    }

    private void GetAnimationClips()
    {
        var selAnimatins = Selection.objects;
        foreach (var a in selAnimatins)
        {
            if (a.GetType() == typeof(AnimationClip))
            {
                allAnimatinClips.Add(a as AnimationClip);
            }
        }
    }

    //private static void GetCurKeys()
    //{
    //    foreach (AnimationClip a in allAnimatinClips)
    //    {
    //        var bindigs = AnimationUtility.GetCurveBindings(a);
    //        var AnimationCurves = AnimationUtility.GetEditorCurve(a, bindigs[5]);
    //        var keys = AnimationCurves.keys;
    //        curAniValue = keys[0].value;
    //    }
    //}


    private void SetKeys(float value)
    {
        foreach (AnimationClip a in allAnimatinClips)
        {
            var bindigs = AnimationUtility.GetCurveBindings(a);
            var AnimationCurves = AnimationUtility.GetEditorCurve(a, bindigs[5]);
            var keys = AnimationCurves.keys;
            AnimationCurve TmpAnimationCurve;

            for (int i = 0; i < keys.Length; ++i)
            {
                keys[i].value += value;
            }
            TmpAnimationCurve = new AnimationCurve(keys);
            a.SetCurve("", typeof(Transform), "m_LocalPosition.y", TmpAnimationCurve);

            //for (int i = 0; i < bindigs.Length; ++i)
            //{
            //    Debug.Log(i + " : " + bindigs[i].propertyName);
            //}
        }
    }

    //private void ResetKeys()
    //{
    //    foreach (AnimationClip a in allAnimatinClips)
    //    {
    //        var bindigs = AnimationUtility.GetCurveBindings(a);
    //        var AnimationCurves = AnimationUtility.GetEditorCurve(a, bindigs[5]);
    //        var keys = AnimationCurves.keys;
    //        AnimationCurve TmpAnimationCurve;

    //        for (int i = 0; i < keys.Length; ++i)
    //        {
    //            keys[i].value = curAniValue;
    //        }
    //        TmpAnimationCurve = new AnimationCurve(keys);
    //        a.SetCurve("", typeof(Transform), "m_LocalPosition.y", TmpAnimationCurve);

    //        //for (int i = 0; i < bindigs.Length; ++i)
    //        //{
    //        //    Debug.Log(i + " : " + bindigs[i].propertyName);
    //        //}
    //    }
    //}


    private void OnGUI()
    {
        GUILayout.BeginHorizontal();
        GUILayout.Label("输入每次移动的单位: ");
        addvalue = EditorGUILayout.FloatField(addvalue);
        GUILayout.EndHorizontal();
        if (GUILayout.Button("UP"))
        {
            ClearAll();

            GetAnimationClips();
            SetKeys(addvalue);
        }
        if (GUILayout.Button("Down"))
        {
            ClearAll();

            GetAnimationClips();
            SetKeys(-addvalue);
        }
    }
}
