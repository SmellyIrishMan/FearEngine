#include "HelperFiles/Shadowing.fx"

cbuffer cbPerObject
{
	float4x4 gWorldViewProj; 
	float4x4 gLightTextureSpaceTransform;
};

Texture2D<float4> gShadowMap   : register(t0);
SamplerComparisonState gShadowSampler;

struct VertexIn
{
	float3 PosL  : POSITION;
};

struct VertexOut
{
	float4 PosH  : SV_POSITION;
	float4 ShadowPosH  : TEXCOORD0;
};

VertexOut VS(VertexIn vIn)
{
	VertexOut vOut;
	
	// Transform to homogeneous clip space.
	vOut.PosH = mul(float4(vIn.PosL, 1.0f), gWorldViewProj);
    vOut.ShadowPosH = mul(float4(vIn.PosL, 1.0f), gLightTextureSpaceTransform);	//ShadowPos is going to be in the [0,1] range and not [-1,1] range.
    
    return vOut;
}

float4 PS(VertexOut pIn) : SV_Target
{	
	float litPercent = CalcShadowFactor(gShadowSampler, gShadowMap, pIn.ShadowPosH);
	
	float3 diffuse = float3(0.9f, 0.9f, 0.9f) * litPercent;
    return float4(diffuse.x, diffuse.y, diffuse.z, 1.0f);
}

technique11 RenderShadowsOnly
{
    pass P0
    {
        SetVertexShader( CompileShader( vs_5_0, VS() ) );
		SetGeometryShader( NULL );
        SetPixelShader( CompileShader( ps_5_0, PS() ) );
    }
}