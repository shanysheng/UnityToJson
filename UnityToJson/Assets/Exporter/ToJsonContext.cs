/*
* Copyright 2017 sheng chongshan. All rights reserved.
* email: shany.sheng@qq.com
* License: https://github.com/shanysheng/RenderPipeline/blob/master/LICENSE
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToJsonContext
{
    public static string targetPath;
    public static Dictionary<string, string> exportedMeshDict;

    public ToJsonContext()
    {
        exportedMeshDict = new Dictionary<string, string>();
    }
}
