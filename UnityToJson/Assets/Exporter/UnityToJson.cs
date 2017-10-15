/*
* Copyright 2017 sheng chongshan. All rights reserved.
* email: shany.sheng@qq.com
* License: https://github.com/shanysheng/RenderPipeline/blob/master/LICENSE
*/

using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class UnityToJsonExporter
{
	[MenuItem("UnityToJson/ExportActiveScene")]
	static void ExportActiveScene() 
	{
        ToJsonContext jsoncontext = new ToJsonContext();

		string targetpath = EditorUtility.SaveFilePanel("Save json file to", "", "test", "json");
		if (targetpath.Length != 0)
		{
			FileStream filestream = File.Open(targetpath, FileMode.Create);
			StreamWriter jsonfile = new StreamWriter(filestream);

			Scene scene = SceneManager.GetActiveScene ();
			JSONObject sceneobj = JSONObject.obj;
			JSONObject childArray = JSONObject.arr;

			sceneobj.AddField ("name", scene.name);
			sceneobj.AddField ("children", childArray);

			int root_count = scene.rootCount;
			GameObject[] goArray = scene.GetRootGameObjects ();
			for (int i = 0; i < root_count; ++i) {
				GameObject go = goArray [i];
				JSONObject rootobj = JSONObject.obj;
                ToJsonGameObject.Export(jsoncontext, rootobj, go);
				childArray.Add (rootobj);
			}

			jsonfile.Write(sceneobj.Print(true));
			jsonfile.Close();
			filestream.Close();
		}
	}
}
