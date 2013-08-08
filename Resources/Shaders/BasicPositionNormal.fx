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
    return float4(pin.NormW, 1.0f);
}

technique11 BasicPositionNormalTech
{
    pass P0
    {
        SetVertexShader( CompileShader( vs_5_0, VS() ) );
        SetPixelShader( CompileShader( ps_5_0, PS() ) );
    }
}