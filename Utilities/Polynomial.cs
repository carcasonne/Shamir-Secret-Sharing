namespace Utilities;
using System.Numerics;
using System;
using Utilities.Extensions;

public class Polynomial 
{
    public BigInteger BaseValue {get; set;}
    public int Degree;
    public BigInteger Field {get; set;}
    public BigInteger[] Coefficients {get; set; }

    public Polynomial(BigInteger baseValue, int degree, BigInteger field) 
    {
        BaseValue = baseValue;
        Degree = degree;
        Field = field;
        Coefficients = generateCoefficients(degree, field);
    }

    // Gets the Y value associated with X from the polynomial
    public BigInteger GetYFromX(BigInteger x)
    {
        var y = BaseValue;
        for(int i = 0; i < Coefficients.Length; i++)
            y += Coefficients[i] * BigInteger.Pow(x, i + 1);

        return y;
    }

    // Returns the secret
    public static BigInteger GetBaseValueFromPoints(Point[] points, int degree) => 
        LagrangeInterpolation(points, 0, degree);
    

    // Generates a random BigInteger value for each coefficient in an array where array[0] corresponds to x^1, array[1] to x^2 etc.
    private BigInteger[] generateCoefficients(int degree, BigInteger field)
    {
        var arr = new BigInteger[degree];
        for (int i = 0; i < degree; i++)
            arr[i] = field.GetRandomIntegerBelow();
            
        return arr;
    }

    private static BigInteger LagrangeInterpolation(Point[] points, BigInteger x, int degree) 
    {
        if (points.Length < degree + 1) 
            throw new Exception("Need more points G");

        BigInteger sumVal = 0;
        for (int j = 0; j < points.Length; j++)
        {
            var top = new BigInteger(1);
            var bottom = new BigInteger(1);
            for (int i = 0; i < points.Length; i++)
            {
                if (i == j)
                    continue;
                top = top * (x - points[i].X);
                bottom = bottom * (points[j].X - points[i].X);
            }
            sumVal += (top/bottom) * points[j].Y;
        }   
        return sumVal;
    }
    
}
