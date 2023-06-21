#ifndef UTILS_HLSL
#define UTILS_HLSL
#define ELEMENT_NUMBER 32

#define DIR_KEEP 0
#define DIR_LEFT 1
#define DIR_RIGHT 2
#define DIR_UP 3
#define DIR_DOWN 4

#define EMPTY 0
#define BEDROCK 1

#define SAND 2
#define WET_SAND 3
#define MUD 4
#define WET_MUD 5
#define ROCK 6
#define ICE 7
#define WATER 8

#define STEAM 9
#define GAS 10
#define POISON_GAS 11
#define FIRE 12

#define OIL 13
#define ACID 14
#define MAGMA 15

#define ROOT 16
#define PLANT 17
#define WOOD 18
#define SEED 19
#define FLOWER 20

#define FIRE_SOURCE 26
#define F_FIRE_SOURCE 27

#define JIANMU 21
#define LIHUO 22
#define XIRANG 23
#define XUANTIE 24
#define RUOSHUI 25


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

bool checkKind(uint2 id,uint kind);
void move(uint2 id,uint2 nbrId);
void setKind(uint2 id,uint kind);
void setY(uint2 id,uint value);
void setZ(uint2 id,uint value);
void setW(uint2 id,uint value);
void setTemperature(uint2 id,float value);
void init(uint3 id);

float rand(const uint3 id,int seed)
{
    return frac(sin(id.x + id.y+id.x*id.y + seed) * 210143.231231);
}

uint float2int(float f)
{
    return f*ELEMENT_NUMBER;
}

float int2float(const uint kind)
{
    return (float)kind/ELEMENT_NUMBER;
}

bool isEmpty(const float4 value)
{
    return float2int(value.x)==EMPTY;
}

bool isGas(const float4 value)
{
    uint kind=float2int(value.x);
    return kind==GAS
            ||kind==STEAM
            ||kind==POISON_GAS;
}

bool isAir(const float4 value)
{
    uint kind=float2int(value.x);
    return kind==EMPTY
            ||kind==GAS;
}

bool canPass(const float4 value)
{
    uint kind=float2int(value.x);
    return kind==EMPTY
        ||kind==SAND
        ||kind==WATER;
}

uint2 randDir(uint3 id,int seed,uint mode)
{
    uint value= floor(rand(id,seed)*(mode==DIR_KEEP?4:5));
    switch (value)
    {
    case 0:return uint2(0,1);
    case 1:return uint2(1,0);
    case 2:return uint2(-1,0);
    case 3:return uint2(0,-1);
    default: return mode==DIR_UP?uint2(0,1):uint2(0,-1);
    }
}


#endif