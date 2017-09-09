/*
* Copyright 2017 sheng chongshan. All rights reserved.
* email: shany.sheng@qq.com
* License: https://github.com/shanysheng/RenderPipeline/blob/master/LICENSE
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToJsonGameObject {

	public static void ExportGameObject (JSONObject jsonGo, GameObject go)
	{
		jsonGo.AddField("name", go.name);

		JSONObject scaleobj = ToJsonCommon.ToJsonObjectVector3 (go.transform.localScale);
		JSONObject rotateobj = ToJsonCommon.ToJsonObjectQuaternion (go.transform.localRotation);
		JSONObject translateobj = ToJsonCommon.ToJsonObjectVector3 (go.transform.localPosition);
		jsonGo.AddField ("scale", scaleobj);
		jsonGo.AddField ("rotate", rotateobj);
		jsonGo.AddField ("translate", translateobj);

		if (go.GetComponent<Camera>() != null)
			ToJsonCamera.ExportCamera(jsonGo, go.GetComponent<Camera>());

		if (go.GetComponent<Light>() != null)
			ToJsonLight.ExportLight(jsonGo, go.GetComponent<Light>());

		if (go.GetComponent<MeshRenderer>() != null)
			ToJsonMeshRenderer.ExportMeshRenderer(jsonGo, go.GetComponent<MeshRenderer>());

		if (go.GetComponent<SkinnedMeshRenderer>() != null)
			ToJsonSkinnedMeshRenderer.ExportSkinnedMeshRenderer(jsonGo, go.GetComponent<SkinnedMeshRenderer>());

		if (go.GetComponent<ParticleSystem>() != null)
			ToJsonParticleSystem.ExportParticleSystem(jsonGo, go.GetComponent<ParticleSystem>());
		

		ToJsonCollider.ExportCollider (go);

		Transform transform = go.transform;
		int childcount = transform.childCount;
		if (childcount > 0) {
			JSONObject childArray = JSONObject.arr;
			jsonGo.AddField ("children", childArray);
			for (int i = 0; i < childcount; ++i) {
				Transform child = transform.GetChild (i);
				GameObject childgo = child.gameObject;
				JSONObject childJsonGO = JSONObject.obj;
				ToJsonGameObject.ExportGameObject (childJsonGO, childgo);
				childArray.Add (childJsonGO);
			}
		}
	}
}
