//These values here should be calculated by passing in the shadow map dimensions.
static const float SMAP_DX = 1.0f / 2048.0f;
static const float SMAP_DY = 1.0f / 2048.0f;

float CalcShadowFactor(SamplerComparisonState shadowSamp, Texture2D shadowMap, float4 shadowPosH)
{
	// Complete projection by doing division by w.
	shadowPosH.xyz /= shadowPosH.w;
	
	// Depth in NDC space.
	float depthFromLightToThisPixel = shadowPosH.z;

	// Texel size.
	const float dx = SMAP_DX;
	const float dy = SMAP_DY;

	float percentLit = 0.0f;
	const float2 offsets[9] = 
	{
		float2(-dx,  -dy), float2(0.0f,  -dy), float2(dx,  -dy),
		float2(-dx, 0.0f), float2(0.0f, 0.0f), float2(dx, 0.0f),
		float2(-dx,  +dy), float2(0.0f,  +dy), float2(dx,  +dy)
	};

	[unroll]
	for(int i = 0; i < 9; ++i)
	{
		percentLit += shadowMap.SampleCmpLevelZero(shadowSamp, shadowPosH.xy + offsets[i], depthFromLightToThisPixel).r;
	}

	return percentLit /= 9.0f;
}