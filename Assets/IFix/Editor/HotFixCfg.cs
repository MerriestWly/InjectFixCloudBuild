using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using IFix;
using UnityEngine;

[Configure]
public class HotFixCfg
{
    [IFix]
    static IEnumerable<Type> hotfix
    {
        get { return Assembly.Load("Assembly-CSharp").GetTypes(); }
    }
}