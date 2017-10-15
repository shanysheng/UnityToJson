/*
* Copyright 2017 sheng chongshan. All rights reserved.
* email: shany.sheng@qq.com
* License: https://github.com/shanysheng/RenderPipeline/blob/master/LICENSE
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToJsonGameObject {

    public static void Export(ToJsonContext context, JSONObject jsonGo, GameObject go)
	{
		jsonGo.AddField("name", go.name);

		JSONObject scaleobj = ToJsonCommon.ToJsonObjectVector3 (go.transform.localScale);
		JSONObject rotateobj = ToJsonCommon.ToJsonObjectQuaternion (go.transform.localRotation);
		JSONObject translateobj = ToJsonCommon.ToJsonObjectVector3 (go.transform.localPosition);
		jsonGo.AddField ("scale", scaleobj);
		jsonGo.AddField ("rotate", rotateobj);
		jsonGo.AddField ("translate", translateobj);

		if (go.GetComponent<Camera>() != null)
            ToJsonCamera.Export(context, jsonGo, go.GetComponent<Camera>());

		if (go.GetComponent<Light>() != null)
            ToJsonLight.Export(context, jsonGo, go.GetComponent<Light>());

		if (go.GetComponent<MeshRenderer>() != null)
            ToJsonMeshRenderer.Export(context, jsonGo, go.GetComponent<MeshRenderer>());

		if (go.GetComponent<SkinnedMeshRenderer>() != null)
            ToJsonSkinnedMeshRenderer.Export(context, jsonGo, go.GetComponent<SkinnedMeshRenderer>());

		if (go.GetComponent<ParticleSystem>() != null)
            ToJsonParticleSystem.Export(context, jsonGo, go.GetComponent<ParticleSystem>());
		
        ToJsonCollider.Export(context, jsonGo, go);

		Transform transform = go.transform;
		int childcount = transform.childCount;
		if (childcount > 0) {
			JSONObject childArray = JSONObject.arr;
			jsonGo.AddField ("children", childArray);
			for (int i = 0; i < childcount; ++i) {
				Transform child = transform.GetChild (i);
				GameObject childgo = child.gameObject;
				JSONObject childJsonGO = JSONObject.obj;
                ToJsonGameObject.Export(context, childJsonGO, childgo);
				childArray.Add (childJsonGO);
			}
		}
	}
}
