float3 NormalSampleToWorldSpace(float3 normalMapSample, float3 unitNormalW, float3 tangentW)
{
	float3 N = unitNormalW;
	float3 T = normalize(tangentW - dot(tangentW, N) * N);
	float3 B = cross(N, T);
	
	float3x3 TBN = float3x3(T, B, N);
	
	float3 normalT = 2.0f * normalMapSample - 1.0f;
	float3 bumpedNormalW = mul(normalT, TBN);
	
	return bumpedNormalW;
}