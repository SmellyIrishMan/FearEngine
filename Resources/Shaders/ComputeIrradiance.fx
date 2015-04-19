cbuffer cbSettings
{
	int gTextureWidth; 
	int gTextureHeight;
	int gCubeLOD;
	int gCubeLODCount;
};

SamplerState Sampler
{
    Filter = MIN_MAG_MIP_LINEAR;
    AddressU = Wrap;
    AddressV = Wrap;
};

TextureCube<float4> gSource;
RWTexture2DArray<float4> gOutput;

static const float PI = 3.1415926535897932384626433832795;

[numthreads(64, 1, 1)]
void CS(int3 dispatchThreadID : SV_DispatchThreadID)
{
	int TexX = dispatchThreadID.x;
	int TexY = dispatchThreadID.y;
	int Slice = dispatchThreadID.z;	//The face of the TextureCube
	
	float2 texCoord = float2(TexX, TexY) / 2048.0f;	//Since we have 2048 pixels, this gives us values in the 0...1 range.
	
	texCoord = (texCoord * 2.0f) - 1.0f;
	float zValue = (float(dispatchThreadID.z) / 6.0f);
	float3 test = float3(texCoord.x, texCoord.y, 1.0f);
	test = normalize(test);
	float4 colour = gSource.SampleLevel(Sampler, test, 0);
	
	gOutput[int3(TexX, TexY, Slice)] = colour;
}

// http://holger.dammertz.org/stuff/notes_HammersleyOnHemisphere.html
float radicalInverse_VdC(uint bits) {
     bits = (bits << 16u) | (bits >> 16u);
     bits = ((bits & 0x55555555u) << 1u) | ((bits & 0xAAAAAAAAu) >> 1u);
     bits = ((bits & 0x33333333u) << 2u) | ((bits & 0xCCCCCCCCu) >> 2u);
     bits = ((bits & 0x0F0F0F0Fu) << 4u) | ((bits & 0xF0F0F0F0u) >> 4u);
     bits = ((bits & 0x00FF00FFu) << 8u) | ((bits & 0xFF00FF00u) >> 8u);
     return float(bits) * 2.3283064365386963e-10; // / 0x100000000
 }
 // http://holger.dammertz.org/stuff/notes_HammersleyOnHemisphere.html
 float2 Hammersley(uint i, uint N) {
     return float2(float(i)/float(N), radicalInverse_VdC(i));
 }

/* float3 ImportanceSampleGGX( float2 Xi, float Roughness, float3 N )
{
	float a = Roughness * Roughness;
	float Phi = 2 * PI * Xi.x;
	float CosTheta = sqrt( (1 - Xi.y) / ( 1 + (a*a - 1) * Xi.y ) );
	float SinTheta = sqrt( 1 - CosTheta * CosTheta );
	float3 H;
	H.x = SinTheta * cos( Phi );
	H.y = SinTheta * sin( Phi );
	H.z = CosTheta;
	
	float3 UpVector = abs(N.z) < 0.999 ? float3(0,0,1) : float3(1,0,0);
	float3 TangentX = normalize( cross( UpVector, N ) );
	float3 TangentY = cross( N, TangentX );
	
	// Tangent to world space
	return TangentX * H.x + TangentY * H.y + N * H.z;
}

float3 PrefilterEnvMap( float Roughness, float3 R )
{
	float3 N = R;
	float3 V = R;
	float3 PrefilteredColor = 0;
	const uint NumSamples = 1024;
	for( uint i = 0; i < NumSamples; i++ )
	{
		float2 Xi = Hammersley( i, NumSamples );
		float3 H = ImportanceSampleGGX( Xi, Roughness, N );
		float3 L = 2 * dot( V, H ) * H - V;
		float NoL = saturate( dot( N, L ) );
		if( NoL > 0 )
		{
			PrefilteredColor += EnvMap.SampleLevel( EnvMapSampler , L, 0 ).rgb * NoL;
			TotalWeight += NoL;
		}
	}
	return PrefilteredColor / TotalWeight;
} */

technique11 ComputeIrradianceMips
{
    pass P0
    {
        SetVertexShader( NULL );
        SetPixelShader( NULL );
        SetComputeShader( CompileShader(cs_5_0, CS() ));
    }
}