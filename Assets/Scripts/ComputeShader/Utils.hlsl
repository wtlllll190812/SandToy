#ifndef UTILS_HLSL
#define UTILS_HLSL

#define ELE_EMPTY 0
#define ELE_SAND 1

float rand(uint3 id,int seed)
{
    return frac(sin(id.x + id.y+id.x*id.y + seed) * 232143.231231);
}

uint value2Kind(const float4 f)
{
    return f.x*255%255;
}

float kind2Value(const uint kind)
{
    return (float)(kind+1)/255;
}
#endif