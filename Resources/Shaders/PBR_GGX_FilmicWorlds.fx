#include "HelperFiles/Lighting.fx"
#include "HelperFiles/PBRLighting.fx"

cbuffer cbPerFrame
{
	DirectionalLight gDirLight;
	float3 gEyeW;
};

cbuffer cbPerObject
{
	float4x4 gWorld;
	float4x4 gWorldInvTranspose; 
	float4x4 gWorldViewProj; 
};

struct VertexIn
{
	float3 PosL  : POSITION;
    float3 NormL : NORMAL;
};

struct VertexOut
{
	float4 PosH  : SV_POSITION;
	float3 PosW	 : POSITION;
    float3 NormW : NORMAL;
};

VertexOut VS(VertexIn vIn)
{
	VertexOut vOut;
	
	// Transform to homogeneous clip space.
	vOut.PosH = mul(float4(vIn.PosL, 1.0f), gWorldViewProj);
	
	vOut.PosW = mul(float4(vIn.PosL, 1.0f), gWorld).xyz;
    vOut.NormW = mul(vIn.NormL, (float3x3)gWorldInvTranspose);
    
    return vOut;
}

float4 PS(VertexOut pIn) : SV_Target
{
	float diffuseIntensity = ComputeDiffuseForDirectionalLight(gDirLight, pIn.NormW);
	
	float roughness = 0.2f;
	float refractiveIndex = 0.15f;
	float3 directionToEye = normalize(gEyeW - pIn.PosW);
	float specularIntensity = LightingFuncGGX_REF(pIn.NormW, directionToEye, -gDirLight.Direction, roughness, refractiveIndex);

	float4 finalColor = gDirLight.Diffuse * diffuseIntensity;
	finalColor += float4(0.85f, 0.95f, 0.85f, 1.0f) * specularIntensity;

    return finalColor;
}

technique11 PBR_GGX_FilmicWorlds
{
    pass P0
    {
        SetVertexShader( CompileShader( vs_5_0, VS() ) );
		SetGeometryShader( NULL );
        SetPixelShader( CompileShader( ps_5_0, PS() ) );
    }
}