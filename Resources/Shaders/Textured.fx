cbuffer cbPerObject
{
	float4x4 gWorldViewProj; 
};

Texture2D gDiffuseMap;

SamplerState samAnisotropic
{
	Filter = ANISOTROPIC;
	MaxAnisotropy = 4;
	
	AddressU = WRAP;
	AddressV = WRAP;
};

struct VertexIn
{
	float3 PosL	: POSITION;
	float2 Tex	: TEXCOORD0;
};

struct VertexOut
{
	float4 PosH  : SV_POSITION;
	float2 Tex	 : TEXCOORD0;
};

VertexOut VS(VertexIn vIn)
{
	VertexOut vOut;
	
	// Transform to homogeneous clip space.
	vOut.PosH = mul(float4(vIn.PosL, 1.0f), gWorldViewProj);
	vOut.Tex = vIn.Tex;
    
    return vOut;
}

float4 PS(VertexOut pIn) : SV_TARGET
{
	float4 texColor = gDiffuseMap.Sample(samAnisotropic, pIn.Tex);
    return texColor;
}

technique11 TexturedNoLighting
{
    pass P0
    {
        SetVertexShader( CompileShader( vs_5_0, VS() ) );
        SetPixelShader( CompileShader( ps_5_0, PS() ) );
    }
}