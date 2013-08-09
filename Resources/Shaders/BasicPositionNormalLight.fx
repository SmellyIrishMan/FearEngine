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
    vout.NormW = vin.NormL;
    
    return vout;
}

float4 PS(VertexOut pin) : SV_Target
{
	float4 finalColor = gLightAmbient;
	float3 dirToLight = -gLightDir;
	float lightIntensity = saturate(dot(pin.NormW, dirToLight));

	finalColor += gLightDiffuse * lightIntensity;

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