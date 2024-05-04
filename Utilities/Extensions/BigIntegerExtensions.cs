namespace Utilities.Extensions;
using System.Numerics;
using System;

public static class BigIntegerExtension
{
     // Taken from stackoverflow https://stackoverflow.com/questions/17357760/how-can-i-generate-a-random-biginteger-within-a-certain-range
     // Generates a random BigInteger below a value N
    public static BigInteger GetRandomIntegerBelow(this BigInteger N)
    {
        byte[] bytes = N.ToByteArray ();
        BigInteger R;

        do {
            Random rand = new Random();
            rand.NextBytes (bytes);
            bytes [bytes.Length - 1] &= (byte)0x7F; //force sign bit to positive
            R = new BigInteger (bytes);
        } while (R >= N);

        return R;
    }
}