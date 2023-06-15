#ifndef UTILS_HLSL
#define UTILS_HLSL

#define ELE_EMPTY 0
#define ELE_WALL 1
#define ELE_SAND 2
#define ELE_GAS 3
#define ELE_WATER 4

uint2 left(const uint3 id){return id.xy+uint2(-1,0);}
uint2 right(const uint3 id){return id.xy+uint2(1,0);}
uint2 left_2(const uint3 id){return id.xy+uint2(-2,0);}
uint2 right_2(const uint3 id){return id.xy+uint2(2,0);}
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

bool isLiquid(const float4 value)
{
    uint kind=value2Kind(value);
    return kind==ELE_GAS
            ||kind==ELE_WATER;
}

bool isGas(const float4 value)
{
    return value2Kind(value)==ELE_GAS;
}

bool isAir(const float4 value)
{
    uint kind=value2Kind(value);
    return kind==ELE_EMPTY
            ||kind==ELE_GAS;
}

bool canPass(const float4 value)
{
    uint kind=value2Kind(value);
    return kind==ELE_EMPTY
        ||kind==ELE_SAND
        ||kind==ELE_WATER;
}

uint2 randDir(uint3 id,int seed)
{
    uint value= floor(rand(id,seed)*5);
    switch (value)
    {
    case 0:return uint2(0,1);
    case 1:return uint2(1,0);
    case 2:return uint2(-1,0);
    case 3:return uint2(0,-1);
    default: return uint2(0,1);
    }
}
#endif