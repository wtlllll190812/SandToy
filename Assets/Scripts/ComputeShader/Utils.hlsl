#ifndef UTILS_HLSL
#define UTILS_HLSL

#define ELE_EMPTY 0
#define ELE_SAND 2

uint2 left(const uint3 id){return id.xy+uint2(-1,0);}
uint2 right(const uint3 id){return id.xy+uint2(1,0);}
uint2 up(const uint3 id){return id.xy+uint2(0,1);}
uint2 down(const uint3 id){return id.xy+uint2(0,-1);}
uint2 leftUp(const uint3 id){return id.xy+uint2(-1,1);}
uint2 rightUp(const uint3 id){return id.xy+uint2(1,1);}
uint2 leftDown(const uint3 id){return id.xy+uint2(-1,-1);}
uint2 rightDown(const uint3 id){return id.xy+uint2(1,-1);}

float rand(const uint3 id,int seed)
{
    return frac(sin(id.x + id.y+id.x*id.y + seed) * 210143.231231);
}

uint value2Kind(const float4 f)
{
    return f.x*32;
}

float kind2Value(const uint kind)
{
    return (float)kind/32;
}

bool isEmpty(const float4 value)
{
    return value2Kind(value)==ELE_EMPTY;
}

#endif