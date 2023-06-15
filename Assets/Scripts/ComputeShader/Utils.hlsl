#ifndef UTILS_HLSL
#define UTILS_HLSL


#define UNIT 1/10
#define ZERO 0
//rgba中，r表示种类，g表示当前格子中的数量


#define ELE_EMPTY 0
#define ELE_WALL 1
#define ELE_SAND 2
#define ELE_GAS 3
#define ELE_WATER 4


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

uint getNum(const float4 value)
{
    return value.y*10;
}

float setNum(const uint num)
{
    return (float)num/10;
}

float4 leaveCell(const float4 cell)
{
    if(getNum(cell)>0)
    {
        return float4(cell.x,setNum(getNum(cell)-1),cell.z,cell.w);
    }
    else
    {
        return float4(ELE_EMPTY,setNum(0),0,0);
    }
}

bool isEmpty(const float4 value)
{
    return value2Kind(value)==ELE_EMPTY;
}

uint2 randVector(const uint3 id, const int seed)
{
    uint value= floor(rand(id,seed)*5);
    switch (value)
    {
    case 0:return left(id);
    case 1:return right(id);
    case 2:return up(id);
    case 3:return down(id);
    default:return up(id);
    }
}
#endif