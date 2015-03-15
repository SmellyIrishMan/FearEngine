Texture2D gDiffuseMap;

cbuffer cbPerFrame
{
	float4 gLightAmbient; 
	float4 gLightDiffuse; 
	float3 gLightDir; 
};

cbuffer cbPerObject
{
	float4x4 gWorldViewProj;
};

struct VertexIn
{
	float3 PosL  : POSITION;
    float3 NormL : NORMAL;
    float3 Tex : TEXCOORD;
};

struct VertexOut
{
	float4 PosH  : SV_POSITION;
    float3 NormW : NORMAL;
    float3 Tex : TEXCOORD;
};

SamplerState samAnisotropic
{
    FILTER = ANISOTROPIC;
    MaxAnisotropy = 4;
};

VertexOut VS(VertexIn vin)
{
	VertexOut vout;
	
	// Transform to homogeneous clip space.
	vout.PosH = mul(float4(vin.PosL, 1.0f), gWorldViewProj);
    vout.NormW = vin.NormL;
	vout.Tex = vin.Tex;
    
    return vout;
}

float4 PS(VertexOut pin) : SV_Target
{
	float4 finalColor = gLightAmbient;
	float3 dirToLight = -gLightDir;
	float lightIntensity = saturate(dot(pin.NormW, dirToLight));

	finalColor += gLightDiffuse * lightIntensity;

	float4 diffuseColor = gDiffuseMap.Sample(samAnisotropic, pin.Tex);

	finalColor = finalColor * diffuseColor;

    return finalColor;
}

technique11 BasicPositionNormalLightTexturedTech
{
    pass P0
    {
        SetVertexShader( CompileShader( vs_5_0, VS() ) );
		SetGeometryShader( NULL );
        SetPixelShader( CompileShader( ps_5_0, PS() ) );
    }
}