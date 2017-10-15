/*
* Copyright 2017 sheng chongshan. All rights reserved.
* email: shany.sheng@qq.com
* License: https://github.com/shanysheng/RenderPipeline/blob/master/LICENSE
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


/*
Shader "Standard"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo", 2D) = "white" {}
		
		_Cutoff("Alpha Cutoff", Range(0.0, 1.0)) = 0.5

		_Glossiness("Smoothness", Range(0.0, 1.0)) = 0.5
		_GlossMapScale("Smoothness Scale", Range(0.0, 1.0)) = 1.0
		[Enum(Metallic Alpha,0,Albedo Alpha,1)] _SmoothnessTextureChannel ("Smoothness texture channel", Float) = 0

		[Gamma] _Metallic("Metallic", Range(0.0, 1.0)) = 0.0
		_MetallicGlossMap("Metallic", 2D) = "white" {}

		[ToggleOff] _SpecularHighlights("Specular Highlights", Float) = 1.0
		[ToggleOff] _GlossyReflections("Glossy Reflections", Float) = 1.0

		_BumpScale("Scale", Float) = 1.0
		_BumpMap("Normal Map", 2D) = "bump" {}

		_Parallax ("Height Scale", Range (0.005, 0.08)) = 0.02
		_ParallaxMap ("Height Map", 2D) = "black" {}

		_OcclusionStrength("Strength", Range(0.0, 1.0)) = 1.0
		_OcclusionMap("Occlusion", 2D) = "white" {}

		_EmissionColor("Color", Color) = (0,0,0)
		_EmissionMap("Emission", 2D) = "white" {}
		
		_DetailMask("Detail Mask", 2D) = "white" {}

		_DetailAlbedoMap("Detail Albedo x2", 2D) = "grey" {}
		_DetailNormalMapScale("Scale", Float) = 1.0
		_DetailNormalMap("Normal Map", 2D) = "bump" {}

		[Enum(UV0,0,UV1,1)] _UVSec ("UV Set for secondary textures", Float) = 0


		// Blending state
		[HideInInspector] _Mode ("__mode", Float) = 0.0
		[HideInInspector] _SrcBlend ("__src", Float) = 1.0
		[HideInInspector] _DstBlend ("__dst", Float) = 0.0
		[HideInInspector] _ZWrite ("__zw", Float) = 1.0
	}


Shader "Standard (Specular setup)"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo", 2D) = "white" {}
		
		_Cutoff("Alpha Cutoff", Range(0.0, 1.0)) = 0.5

		_Glossiness("Smoothness", Range(0.0, 1.0)) = 0.5
		_GlossMapScale("Smoothness Factor", Range(0.0, 1.0)) = 1.0
		[Enum(Specular Alpha,0,Albedo Alpha,1)] _SmoothnessTextureChannel ("Smoothness texture channel", Float) = 0

		_SpecColor("Specular", Color) = (0.2,0.2,0.2)
		_SpecGlossMap("Specular", 2D) = "white" {}
		[ToggleOff] _SpecularHighlights("Specular Highlights", Float) = 1.0
		[ToggleOff] _GlossyReflections("Glossy Reflections", Float) = 1.0

		_BumpScale("Scale", Float) = 1.0
		_BumpMap("Normal Map", 2D) = "bump" {}

		_Parallax ("Height Scale", Range (0.005, 0.08)) = 0.02
		_ParallaxMap ("Height Map", 2D) = "black" {}

		_OcclusionStrength("Strength", Range(0.0, 1.0)) = 1.0
		_OcclusionMap("Occlusion", 2D) = "white" {}

		_EmissionColor("Color", Color) = (0,0,0)
		_EmissionMap("Emission", 2D) = "white" {}
		
		_DetailMask("Detail Mask", 2D) = "white" {}

		_DetailAlbedoMap("Detail Albedo x2", 2D) = "grey" {}
		_DetailNormalMapScale("Scale", Float) = 1.0
		_DetailNormalMap("Normal Map", 2D) = "bump" {}

		[Enum(UV0,0,UV1,1)] _UVSec ("UV Set for secondary textures", Float) = 0


		// Blending state
		[HideInInspector] _Mode ("__mode", Float) = 0.0
		[HideInInspector] _SrcBlend ("__src", Float) = 1.0
		[HideInInspector] _DstBlend ("__dst", Float) = 0.0
		[HideInInspector] _ZWrite ("__zw", Float) = 1.0
	}
 */

public class ToJsonMaterial {

    private static string GetTextureAssetPath(Texture2D srctex)
    {
        string assetsstr = "Assets/";

        string texturePath = AssetDatabase.GetAssetPath(srctex);

        if(texturePath.Length > assetsstr.Length)
        {
            string relativepath = texturePath.ToLower().Substring(assetsstr.Length);
            return relativepath; 
        }

        return "";
    }

    public static JSONObject ExportMaterial(Material material)
    {
        JSONObject jsonobj = JSONObject.obj;

        jsonobj.AddField("name", material.name);

        Shader shader = material.shader;
        string tempstr = shader.name.ToLower();
        jsonobj.AddField("shader", tempstr);

        if (material.HasProperty("_Cutoff") == true)
        {
            float alpharef = material.GetFloat("_Cutoff");
            jsonobj.AddField("alpharef", alpharef);
        }

        // diffuse
        if (material.HasProperty("_Color") == true)
        {
            Color matcolor = material.GetColor("_Color");
            JSONObject colorobj = ToJsonCommon.ToJsonObjectColor(matcolor);
            jsonobj.AddField("albedocolor", colorobj);
        }

        if (material.HasProperty("_MainTex") == true)
        {
            Texture2D albedoMap = (Texture2D)material.GetTexture("_MainTex");
            string texpath = GetTextureAssetPath(albedoMap);
            jsonobj.AddField("albedomap", texpath);
        }

        // smooth
        if (material.HasProperty("_GlossMapScale") == true)
        {
            float smoothnessscale = material.GetFloat("_GlossMapScale");
            jsonobj.AddField("smoothnessscale", smoothnessscale);
        }

        if (material.HasProperty("_SmoothnessTextureChannel") == true)
        {
            int smoothnesstexturechannel = material.GetInt("_SmoothnessTextureChannel");
            jsonobj.AddField("smoothnesstexturechannel", smoothnesstexturechannel);
        }

        if(shader.name.ToLower().Contains("specular setup"))
        {
            ExportMaterialStandardSpecular(jsonobj, material);
        }
        else
        {
            ExportMaterialStandard(jsonobj, material);
        }

        // bump
        if (material.HasProperty("_BumpScale") == true)
        {
            float bumpscale = material.GetFloat("_BumpScale");
            jsonobj.AddField("bumpscale", bumpscale);
        }

        if (material.HasProperty("_BumpMap") == true)
        {
            Texture2D bumpmap = (Texture2D)material.GetTexture("_BumpMap");
            string texpath = GetTextureAssetPath(bumpmap);
            jsonobj.AddField("bumpmap", texpath);
        }

        // parallax
        if (material.HasProperty("_Parallax") == true)
        {
            float parallaxscale = material.GetFloat("_Parallax");
            jsonobj.AddField("parallaxscale", parallaxscale);
        }

        if (material.HasProperty("_ParallaxMap") == true)
        {
            Texture2D parallaxmap = (Texture2D)material.GetTexture("_ParallaxMap");
            string texpath = GetTextureAssetPath(parallaxmap);
            jsonobj.AddField("parallaxmap", texpath);
        }

        // occlusion
        if (material.HasProperty("_OcclusionStrength") == true)
        {
            float occlusionscale = material.GetFloat("_OcclusionStrength");
            jsonobj.AddField("occlusionscale", occlusionscale);
        }

        if (material.HasProperty("_OcclusionMap") == true)
        {
            Texture2D occlusionmap = (Texture2D)material.GetTexture("_OcclusionMap");
            string texpath = GetTextureAssetPath(occlusionmap);
            jsonobj.AddField("occlusionmap", texpath);
        }

        // emmission
        if (material.HasProperty("_EmissionColor") == true)
        {
            Color emissioncolor = material.GetColor("_EmissionColor");
            JSONObject colorobj = ToJsonCommon.ToJsonObjectColor(emissioncolor);
            jsonobj.AddField("emissioncolor", colorobj);
        }

        if (material.HasProperty("_EmissionMap") == true)
        {
            Texture2D emissionmap = (Texture2D)material.GetTexture("_EmissionMap");
            string texpath = GetTextureAssetPath(emissionmap);
            jsonobj.AddField("emissionmap", texpath);
        }

        // detail 
        if (material.HasProperty("_DetailMask") == true)
        {
            Texture2D detailmask = (Texture2D)material.GetTexture("_ParallaxMap");
            string texpath = GetTextureAssetPath(detailmask);
            jsonobj.AddField("detailmask", texpath);
        }

        if (material.HasProperty("_DetailAlbedoMap") == true)
        {
            Texture2D detailalbedomap = (Texture2D)material.GetTexture("_DetailAlbedoMap");
            string texpath = GetTextureAssetPath(detailalbedomap);
            jsonobj.AddField("detailalbedomap", texpath);
        }

        if (material.HasProperty("_DetailNormalMapScale") == true)
        {
            float detailnormalmapscale = material.GetFloat("_DetailNormalMapScale");
            jsonobj.AddField("detailnormalmapscale", detailnormalmapscale);
        }

        if (material.HasProperty("_DetailNormalMap") == true)
        {
            Texture2D detailnormalmap = (Texture2D)material.GetTexture("_DetailNormalMap");
            string texpath = GetTextureAssetPath(detailnormalmap);
            jsonobj.AddField("detailnormalmap", texpath);
        }

        return jsonobj;
    }

    public static void ExportMaterialStandard(JSONObject jsonobj, Material material)
    {
        // metallic
        if (material.HasProperty("_MetallicGlossMap") == true)
        {
            Texture2D metallicmap = (Texture2D)material.GetTexture("_MetallicGlossMap");
            string texpath = GetTextureAssetPath(metallicmap);
            jsonobj.AddField("metallicmap", texpath);

            if (material.HasProperty("_Metallic") == true)
            {
                float metallicscale = material.GetFloat("_Metallic");
                jsonobj.AddField("metallicscale", metallicscale);
            }
        }
    }

    public static void ExportMaterialStandardSpecular(JSONObject jsonobj, Material material)
	{
        // specular
        if (material.HasProperty("_SpecGlossMap") == true)
        {
            Texture2D specularmap = (Texture2D)material.GetTexture("_SpecGlossMap");
            string texpath = GetTextureAssetPath(specularmap);
            jsonobj.AddField("specularmap", texpath);

            if (material.HasProperty("_SpecColor") == true)
            {
                Color specularcolor = material.GetColor("_SpecColor");
                JSONObject colorobj = ToJsonCommon.ToJsonObjectColor(specularcolor);
                jsonobj.AddField("specularcolor", colorobj);
            }
        }
	}
}
