#ifndef CORE
#define CORE

#define flt double
#define X 0
#define Y 1
#define Z 2

bool equals(const flt& a, const flt& b);
flt pow(const flt& x);

#include "math.h"
#include "vec2.h"
#include "vec3.h"
#include "matrix4.h"

const flt PI = 3.141592653589793;
const flt TwoPI = 6.2831853071795864769;
const flt PIOn180 = 0.0174532925199432957;
const flt InvPI = 0.3183098861837906715;
const flt InvTwoPI = 0.15915494309189533577;
const flt SmallEpsilon = 0.00001; 
const flt Epsilon = 0.0001;
const flt BigEpsilon = 0.001;
const flt SleepEpilson = 0.3;
const flt PI4div3 = 4.18879;
const flt InvRandMax = 1.0 / 0x7fff;

bool equals(const flt& a, const flt& b)
{
	return abs(a - b) <= SmallEpsilon;
}

flt pow(const flt& x)
{
	return pow(x, 2);
}


#endif