/*
* Copyright 2017 sheng chongshan. All rights reserved.
* email: shany.sheng@qq.com
* License: https://github.com/shanysheng/RenderPipeline/blob/master/LICENSE
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToJsonCommon {

	public static JSONObject ToJsonObjectRect(Rect rect )
	{
		JSONObject result = JSONObject.arr;
		result.Add(rect.x);
		result.Add(rect.y);
		result.Add(rect.width);
		result.Add(rect.height);
		result.writesingleline = true; 
		return result;
	}

	public static JSONObject ToJsonObjectColor(Color color )
	{
		JSONObject result = JSONObject.arr;
		result.Add(color.r);
		result.Add(color.g);
		result.Add(color.b);
		result.Add(color.a);
		result.writesingleline = true; 
		return result;
	}

	public static JSONObject ToJsonObjectVector3(Vector3 vector3)
	{
		JSONObject result = JSONObject.arr;
		result.Add(vector3.x);
		result.Add(vector3.y);
		result.Add(vector3.z);
		result.writesingleline = true; 
		return result;
	}

	public static JSONObject ToJsonObjectVecotr4(Vector4 vector4)
	{
		JSONObject result = JSONObject.arr;
		result.Add(vector4.x);
		result.Add(vector4.y);
		result.Add(vector4.z);
		result.Add(vector4.w);
		result.writesingleline = true;
		return result;
	}

	public static JSONObject ToJsonObjectQuaternion(Quaternion quat)
	{
		JSONObject result = JSONObject.arr;
		result.Add(quat.x);
		result.Add(quat.y);
		result.Add(quat.z);
		result.Add(quat.w);
		result.writesingleline = true; 
		return result;
	}
}
