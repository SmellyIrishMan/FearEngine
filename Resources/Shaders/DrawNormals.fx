cbuffer cbPerObject
{
	float4x4 gWorldViewProj; 
};

struct VertexIn
{
	float3 PosL  : SV_POSITION;
    float3 NormL : NORMAL;
};

struct VertexOut
{
	float4 PosH  : SV_POSITION;
    float3 NormW : NORMAL;
};

float normalLength = 1.5f;

VertexIn VS(VertexIn vin)
{    
    return vin;
}

[maxvertexcount(2)]
void GS (point VertexIn Input[1], inout LineStream<VertexOut> OutputStream)
{
	VertexOut output = (VertexOut)0;
	output.PosH = mul(float4(Input[0].PosL, 1.0f), gWorldViewProj);
    output.NormW = Input[0].NormL;
	OutputStream.Append(output);

	float3 endPoint = Input[0].PosL + (Input[0].NormL * normalLength);
	output.PosH = mul(float4(endPoint, 1.0f), gWorldViewProj);
    output.NormW = Input[0].NormL;
	OutputStream.Append(output);

	OutputStream.RestartStrip();
}

float4 PS(VertexOut pin) : SV_Target
{
    return float4(pin.NormW, 1.0f);
}

technique11 DrawNormalsTech
{
    pass P0
    {
        SetVertexShader( CompileShader( vs_5_0, VS() ) );
        SetGeometryShader( CompileShader( gs_5_0, GS() ) );
        SetPixelShader( CompileShader( ps_5_0, PS() ) );
    }
}