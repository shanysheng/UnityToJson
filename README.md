# UnityToJson
Export unity3d scene,mesh,material etc to json file

## Gameobject

```
        {
			"name":"Main Camera",
			"scale":[1,1,1],
			"rotate":[0,0,0,1],
			"translate":[0,1,-10],
			"cameradef":{}
        }
```
## Camera
```
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
```
## Example
```
{
	"name":"",
	"children":[
		{
			"name":"Main Camera",
			"scale":[1,1,1],
			"rotate":[0,0,0,1],
			"translate":[0,1,-10],
			"cameradef":{
				"projection":"perspective",
				"fov":60,
				"near":0.3,
				"far":1000,
				"bgcolor":[0.1921569,0.3019608,0.4745098,0],
				"depth":-1,
				"clearflag":"Skybox",
				"cullflag":"-1",
				"viewport":[0,0,1,1],
				"bhdr":true,
				"bmsaa":true,
				"occlusioncull":true
			}
		},
		{
			"name":"Directional Light",
			"scale":[1,1,1],
			"rotate":[0.4082179,-0.2345697,0.1093816,0.8754261],
			"translate":[0,3,0]
		}
	]
}
```