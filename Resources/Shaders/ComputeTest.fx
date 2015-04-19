cbuffer cbSettings
{
	int textureWidth; 
	int textureHeight; 
	float4 fillColour;
};

RWTexture2D<float4> gOutput;

[numthreads(64, 1, 1)]
void CS(
	int3 groupThreadID : SV_GroupThreadID,
	int3 dispatchThreadID : SV_DispatchThreadID)
{
	gOutput[int2(dispatchThreadID.x, dispatchThreadID.y)] = fillColour;
}

technique11 FillTexture
{
    pass P0
    {
        SetVertexShader( NULL );
        SetPixelShader( NULL );
        SetComputeShader( CompileShader(cs_5_0, CS() ));
    }
}