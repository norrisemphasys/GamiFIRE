using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class TextUtil
{
    public static void WriteToTextArray(string[] lines, string filename)
    {
        // Set a variable to the Documents path.
        string docPath = Application.persistentDataPath + "/TextFile/";

        if (!Directory.Exists(docPath))
            Directory.CreateDirectory(docPath);

        string fileName = filename + ".txt";

        // Write the string array to a new file named "WriteLines.txt".
        using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, fileName)))
        {
            foreach (string line in lines)
                outputFile.WriteLine(line);
        }
    }

    public static string GetUniqueTextToSpeechFilename(string text)
    { 
        string hash = string.Format("{0}", text);
        return string.Format(@"{0}", GetMd5Hash(text).Replace("-", ""));
    }

    public static string CleanString(string dirtyString)
    {
        HashSet<char> removeChars = new HashSet<char>("&^$#@!()+-:;<>¡¯\'-_*");
        StringBuilder result = new StringBuilder(dirtyString.Length);
        foreach (char c in dirtyString)
            if (!removeChars.Contains(c)) // prevent dirty chars
                result.Append(c);
        return result.ToString();
    }

    public static string GetUniqueTextToSpeechFilenamev2(string text)
    {
        string hash = string.Format("{0}_{1}", "Male", text);
        return string.Format(@"{0}", GetMd5Hash(hash).Replace("-", ""));
    }

    private static string GetMd5Hash(string input)
    {
        using (MD5 md5Hash = MD5.Create())
        {
            byte[] buffer = Encoding.UTF8.GetBytes(input);

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(buffer);

            // Create a new StringBuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data and format each one as a hexadecimal string.
            foreach (byte @byte in data)
            {
                sBuilder.Append(@byte.ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}
