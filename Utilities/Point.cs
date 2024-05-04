namespace Utilities;
using System.Numerics;
using System;
using Utilities.Extensions;

public class Point
{
    public BigInteger X {get; set;}
    public BigInteger Y {get; set;}

    public Point(BigInteger x, BigInteger y)
    {
        X = x;
        Y = y;
    }

    // Creates a random point on some polynomial P
    public Point(Polynomial p)
    {
        var randomX = p.Field.GetRandomIntegerBelow();
        var randomY = p.GetYFromX(randomX);
        X = randomX;
        Y = randomY;
    }

}