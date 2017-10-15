/*
* Copyright 2017 sheng chongshan. All rights reserved.
* email: shany.sheng@qq.com
* License: https://github.com/shanysheng/RenderPipeline/blob/master/LICENSE
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToJsonLight {

    public static void Export(ToJsonContext context, JSONObject goObj, Light light)
	{
		JSONObject jsonobj = JSONObject.obj;
		jsonobj.AddField ("type", light.type.ToString ());

		JSONObject colorobj = ToJsonCommon.ToJsonObjectColor (light.color);
		jsonobj.AddField ("color", colorobj);
		jsonobj.AddField ("intensity", light.intensity);

		jsonobj.AddField ("range", light.range);
		jsonobj.AddField ("spotangle", light.spotAngle);

		jsonobj.AddField ("mode", light.renderMode.ToString ());
		jsonobj.AddField ("shadowmode", light.shadows.ToString());
		jsonobj.AddField ("shadowbias", light.shadowBias);
		jsonobj.AddField ("shadownearplane", light.shadowNearPlane);
		jsonobj.AddField ("shadowstrength", light.shadowStrength);

		jsonobj.AddField ("cullflag", light.cullingMask.ToString ());

		goObj.AddField ("lightdef", jsonobj);
	}
}
