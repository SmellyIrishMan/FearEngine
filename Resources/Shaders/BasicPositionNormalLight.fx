#include "HelperFiles/Lighting.fx"

cbuffer cbPerFrame
{
	DirectionalLight gDirLight;
};

cbuffer cbPerObject
{
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
    float3 NormW : NORMAL;
};

VertexOut VS(VertexIn vin)
{
	VertexOut vout;
	
	// Transform to homogeneous clip space.
	vout.PosH = mul(float4(vin.PosL, 1.0f), gWorldViewProj);
    vout.NormW = mul(vin.NormL, (float3x3)gWorldInvTranspose);
    
    return vout;
}

float4 PS(VertexOut pin) : SV_Target
{
	float4 finalColor = gDirLight.Ambient;
	float3 dirToLight = -gDirLight.Direction;
	float lightIntensity = saturate(dot(pin.NormW, dirToLight));

	finalColor += gDirLight.Diffuse * lightIntensity;

    return finalColor;
}

technique11 BasicPositionNormalLightTech
{
    pass P0
    {
        SetVertexShader( CompileShader( vs_5_0, VS() ) );
		SetGeometryShader( NULL );
        SetPixelShader( CompileShader( ps_5_0, PS() ) );
    }
}