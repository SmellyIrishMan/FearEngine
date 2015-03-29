#include "HelperFiles/Lighting.fx"
#include "HelperFiles/Texturing.fx"
#include "HelperFiles/NormalMapping.fx"

cbuffer cbPerFrame
{
	DirectionalLight gDirLight;
};

cbuffer cbPerObject
{
	float4x4 gWorld;
	float4x4 gWorldInvTranspose;
	float4x4 gWorldViewProj; 
};

Texture2D<float4> gAlbedo   : register(t0);
Texture2D<float4> gNormal   : register(t1);

struct VertexIn
{
	float3 PosL		: POSITION;
	float3 NormalL	: NORMAL;
	float3 TangentL	: TANGENT;
	float2 Tex		: TEXCOORD0;
};

struct VertexOut
{
	float4 PosH  	: SV_POSITION;
	float3 NormalW  : NORMAL;
	float3 TangentW : TANGENT;
	float2 Tex	 	: TEXCOORD0;
};

VertexOut VS(VertexIn vIn)
{
	VertexOut vOut;
	
	// Transform to homogeneous clip space.
	vOut.PosH = mul(float4(vIn.PosL, 1.0f), gWorldViewProj);
	
	vOut.NormalW = mul(vIn.NormalL, (float3x3)gWorldInvTranspose);
	vOut.TangentW = mul(vIn.TangentL, (float3x3)gWorld);
	vOut.Tex = vIn.Tex;
    
    return vOut;
}

float4 PS(VertexOut pIn) : SV_TARGET
{
	pIn.NormalW = normalize(pIn.NormalW);
	
	float3 normalMapSample = gNormal.Sample(samAnisotropic, pIn.Tex).rgb;
	float3 bumpedNormalW = NormalSampleToWorldSpace(normalMapSample, pIn.NormalW, pIn.TangentW);
	
	float4 finalColor = gDirLight.Ambient;
	
	float3 dirToLight = -gDirLight.Direction;
	float lightIntensity = saturate(dot(bumpedNormalW, dirToLight));
	
	float4 albedo = gAlbedo.Sample(samAnisotropic, pIn.Tex);
	
	finalColor = albedo * gDirLight.Diffuse * lightIntensity;
	
    return finalColor;
}

technique11 NormalMappingNoLighting
{
    pass P0
    {
        SetVertexShader( CompileShader( vs_5_0, VS() ) );
        SetPixelShader( CompileShader( ps_5_0, PS() ) );
    }
}