// Copyright (c) CodeBlock.Dev. All rights reserved.
// See LICENSE in the project root for license information.

namespace CodeBlock.DevKit.Core.Extensions;

public static class RandomDataGenerator
{
    private static readonly Random _rand = new Random();
    private const string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    public static string GetRandomString(int length = 8)
    {
        var stringChars = new char[length];

        for (int i = 0; i < stringChars.Length; i++)
            stringChars[i] = CHARS[_rand.Next(CHARS.Length)];

        return new string(stringChars);
    }

    public static int GetRandomInt(int min = int.MinValue, int max = int.MaxValue)
    {
        return _rand.Next(min, max);
    }

    public static long GetRandomLong(int min = int.MinValue, int max = int.MaxValue)
    {
        return _rand.Next(min, max);
    }

    public static string GetRandomNumber(int length)
    {
        string randomNumber = "";

        for (int i = 0; i < length; i++)
            randomNumber += GetRandomInt(0, 9);

        return randomNumber;
    }

    public static DateTime GetRandomDateTime()
    {
        DateTime start = DateTime.Now.AddYears(-1);
        DateTime end = DateTime.Now;
        int range = (end - start).Days;
        return start.AddDays(_rand.Next(range));
    }
}
