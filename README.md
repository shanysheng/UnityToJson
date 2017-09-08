# UnityToJson
Export unity3d scene,mesh,material etc to json file

## Gameobject
		{
			"name":"Main Camera",
			"scale":[1,1,1],
			"rotate":[0,0,0,1],
			"translate":[0,1,-10],
			"cameradef":{}
        }

## Camera

			"cameradef":{
				"projection": orthographic or perspective,
				"fov": field of view,
				"near": near clip plane,
				"far": far clip plane,
				"bgcolor":[r,g,b,a] back ground color,
				"depth": depth default value,
				"clearflag":"color and depth buffer clear flag",
				"cullflag":"camera culling flag(layer)",
				"viewport":[x,y,width,height]view port regin0~1,
				"bhdr":enable hdr buffer,
				"bmsaa":enable msaa ,
				"occlusioncull": enable occlusion culling
			}
