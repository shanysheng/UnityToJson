/*
* Copyright 2017 sheng chongshan. All rights reserved.
* email: shany.sheng@qq.com
* License: https://github.com/shanysheng/RenderPipeline/blob/master/LICENSE
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToJsonCamera 
{
    public static void Export(ToJsonContext context, JSONObject goObj, Camera incam)
    {
		JSONObject jsonobj = JSONObject.obj;

		jsonobj.AddField ("projection", incam.orthographic ? "orthographic" : "perspective");
		jsonobj.AddField ("fov", incam.fieldOfView);
		jsonobj.AddField ("near", incam.nearClipPlane);
		jsonobj.AddField ("far", incam.farClipPlane);

		JSONObject bgcolor = ToJsonCommon.ToJsonObjectColor (incam.backgroundColor);
		jsonobj.AddField ("bgcolor", bgcolor);
		jsonobj.AddField ("depth", incam.depth);

		jsonobj.AddField ("clearflag", incam.clearFlags.ToString());
		jsonobj.AddField ("cullflag", incam.cullingMask.ToString());

		JSONObject rect = ToJsonCommon.ToJsonObjectRect (incam.rect);
		jsonobj.AddField ("viewport", rect);

		jsonobj.AddField ("bhdr", incam.allowHDR);
		jsonobj.AddField ("bmsaa", incam.allowMSAA);
		jsonobj.AddField ("occlusioncull", incam.useOcclusionCulling);

		goObj.AddField ("cameradef", jsonobj);
    }
}
