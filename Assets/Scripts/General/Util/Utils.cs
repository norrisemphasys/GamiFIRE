using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Security.Cryptography;
using System.Text;

public static class Utils
{
    #region STRING ENCRYPTION

    public static string GetMD5Hash(string input)
    {
        using (MD5 md5Hash = MD5.Create())
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }

    // Verify a hash against a string.
    public static bool VerifyMD5Hash(string input, string hash)
    {
        // Hash the input.
        string hashOfInput = GetMD5Hash(input);

        // Create a StringComparer an compare the hashes.
        StringComparer comparer = StringComparer.OrdinalIgnoreCase;

        return 0 == comparer.Compare(hashOfInput, hash);
    }

    #endregion

    #region SHUFFLE
    public static T[] Shuffle<T>(this System.Random rng, T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            int k = rng.Next(n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }

        return array;
    }

    public static void Shuffle<T>(this IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }

    #endregion

    #region TIME HELPER
    public static void Delay(UnityEngine.MonoBehaviour mono, Action callback, float time)
    {
        mono.StartCoroutine(DelayEnum(callback, time));
    }

    public static IEnumerator DelayEnum(Action callback, float time = 0)
    {
        yield return new WaitForSeconds(time);
        callback?.Invoke();
    }

    #endregion
}
