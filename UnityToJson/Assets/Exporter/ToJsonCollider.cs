/*
* Copyright 2017 sheng chongshan. All rights reserved.
* email: shany.sheng@qq.com
* License: https://github.com/shanysheng/RenderPipeline/blob/master/LICENSE
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToJsonCollider {

    public static void Export(ToJsonContext context, JSONObject jsonGo, GameObject go)
	{
		if (go.GetComponent<BoxCollider>() != null)
			ExportBoxCollider(jsonGo, go.GetComponent<BoxCollider>());

		if (go.GetComponent<SphereCollider>() != null)
			ExportSphereCollider(jsonGo, go.GetComponent<SphereCollider>());

		if (go.GetComponent<CapsuleCollider>() != null)
			ExportCapsuleCollider(jsonGo, go.GetComponent<CapsuleCollider>());

		if (go.GetComponent<MeshCollider>() != null)
			ExportMeshCollider(jsonGo, go.GetComponent<MeshCollider>());

		if (go.GetComponent<TerrainCollider>() != null)
			ExportTerrainCollider(jsonGo, go.GetComponent<TerrainCollider>());
	}


	public static void ExportBoxCollider(JSONObject jsonGo, BoxCollider boxcollider)
	{
		JSONObject jsonobj = JSONObject.obj;
		JSONObject centerobj = ToJsonCommon.ToJsonObjectVector3 (boxcollider.center);
		jsonobj.AddField ("center", centerobj);

		JSONObject sizeobj = ToJsonCommon.ToJsonObjectVector3 (boxcollider.size);
		jsonobj.AddField ("size", sizeobj);

		jsonGo.AddField ("boxcollider", jsonobj);
	}

	public static void ExportSphereCollider(JSONObject jsonGo, SphereCollider spherecollider)
	{
		JSONObject jsonobj = JSONObject.obj;

		JSONObject centerobj = ToJsonCommon.ToJsonObjectVector3 (spherecollider.center);
		jsonobj.AddField ("center", centerobj);
		jsonobj.AddField ("radius", spherecollider.radius);

		jsonGo.AddField ("spherecollider", jsonobj);
	}

	public static void ExportCapsuleCollider(JSONObject jsonGo, CapsuleCollider capulecollider)
	{
		JSONObject jsonobj = JSONObject.obj;

		JSONObject centerobj = ToJsonCommon.ToJsonObjectVector3 (capulecollider.center);
		jsonobj.AddField ("center", centerobj);
		jsonobj.AddField ("radius", capulecollider.radius);
		jsonobj.AddField ("height", capulecollider.height);
		jsonobj.AddField ("direction", capulecollider.direction);

		jsonGo.AddField ("capulecollider", jsonobj);
	}

	public static void ExportMeshCollider(JSONObject jsonGo, MeshCollider meshcollider)
	{
		JSONObject jsonobj = JSONObject.obj;

		jsonGo.AddField ("meshcollider", jsonobj);
	}

	public static void ExportTerrainCollider(JSONObject jsonGo, TerrainCollider terraincollider)
	{
		JSONObject jsonobj = JSONObject.obj;

		jsonGo.AddField ("terraincollider", jsonobj);
	}
}
