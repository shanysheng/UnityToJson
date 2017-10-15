/*
* Copyright 2017 sheng chongshan. All rights reserved.
* email: shany.sheng@qq.com
* License: https://github.com/shanysheng/RenderPipeline/blob/master/LICENSE
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToJsonMeshRenderer {

	public static void ExportMeshRenderer(JSONObject goObj, MeshRenderer mrender)
    {
		JSONObject jsonobj = JSONObject.obj;

		jsonobj.AddField ("castshadow", mrender.shadowCastingMode.ToString());
		jsonobj.AddField ("receiveshadow", mrender.receiveShadows);

		if (mrender.sharedMaterial != null) {
			JSONObject matObj = ToJsonMaterial.ExportMaterial (mrender.sharedMaterial);
			jsonobj.AddField ("materialdef", matObj);
		}


		if (mrender.gameObject.GetComponent<MeshFilter> () != null) {
			MeshFilter filter = mrender.gameObject.GetComponent<MeshFilter> ();
			if (filter.sharedMesh != null)
				ToJsonMesh.ExportMesh (jsonobj, filter.sharedMesh);
		}

        goObj.AddField("meshrender", jsonobj);
    }
}
