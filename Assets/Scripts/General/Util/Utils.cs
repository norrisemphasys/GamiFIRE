using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

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

    #region MATH HELPER
    public static Vector3 GetPointInPath(Vector3 start, Vector3 middle, Vector3 end, float t)
    {
        float r = 1f - t;
        return (r * r * start) + (2f * r * t * middle) + (t * t * end);
    }

    public static Vector3 GetMiddlePoint(Vector3 start, Vector3 end, float t) 
    {
        return Vector3.Lerp(start, end, t);
    }
	#endregion

	#region EASE HELPER
	public static float Linear(float t) => t;

	public static float InQuad(float t) => t * t;
	public static float OutQuad(float t) => 1 - InQuad(1 - t);
	public static float InOutQuad(float t)
	{
		if (t < 0.5) return InQuad(t * 2) / 2;
		return 1 - InQuad((1 - t) * 2) / 2;
	}

	public static float InCubic(float t) => t * t * t;
	public static float OutCubic(float t) => 1 - InCubic(1 - t);
	public static float InOutCubic(float t)
	{
		if (t < 0.5) return InCubic(t * 2) / 2;
		return 1 - InCubic((1 - t) * 2) / 2;
	}

	public static float InQuart(float t) => t * t * t * t;
	public static float OutQuart(float t) => 1 - InQuart(1 - t);
	public static float InOutQuart(float t)
	{
		if (t < 0.5) return InQuart(t * 2) / 2;
		return 1 - InQuart((1 - t) * 2) / 2;
	}

	public static float InQuint(float t) => t * t * t * t * t;
	public static float OutQuint(float t) => 1 - InQuint(1 - t);
	public static float InOutQuint(float t)
	{
		if (t < 0.5) return InQuint(t * 2) / 2;
		return 1 - InQuint((1 - t) * 2) / 2;
	}

	public static float InSine(float t) => 1 - (float)Math.Cos(t * Math.PI / 2);
	public static float OutSine(float t) => (float)Math.Sin(t * Math.PI / 2);
	public static float InOutSine(float t) => (float)(Math.Cos(t * Math.PI) - 1) / -2;

	public static float InExpo(float t) => (float)Math.Pow(2, 10 * (t - 1));
	public static float OutExpo(float t) => 1 - InExpo(1 - t);
	public static float InOutExpo(float t)
	{
		if (t < 0.5) return InExpo(t * 2) / 2;
		return 1 - InExpo((1 - t) * 2) / 2;
	}

	public static float InCirc(float t) => -((float)Math.Sqrt(1 - t * t) - 1);
	public static float OutCirc(float t) => 1 - InCirc(1 - t);
	public static float InOutCirc(float t)
	{
		if (t < 0.5) return InCirc(t * 2) / 2;
		return 1 - InCirc((1 - t) * 2) / 2;
	}

	public static float InElastic(float t) => 1 - OutElastic(1 - t);
	public static float OutElastic(float t)
	{
		float p = 0.3f;
		return (float)Math.Pow(2, -10 * t) * (float)Math.Sin((t - p / 4) * (2 * Math.PI) / p) + 1;
	}
	public static float InOutElastic(float t)
	{
		if (t < 0.5) return InElastic(t * 2) / 2;
		return 1 - InElastic((1 - t) * 2) / 2;
	}

	public static float InBack(float t)
	{
		float s = 1.70158f;
		return t * t * ((s + 1) * t - s);
	}
	public static float OutBack(float t) => 1 - InBack(1 - t);
	public static float InOutBack(float t)
	{
		if (t < 0.5) return InBack(t * 2) / 2;
		return 1 - InBack((1 - t) * 2) / 2;
	}

	public static float InBounce(float t) => 1 - OutBounce(1 - t);
	public static float OutBounce(float t)
	{
		float div = 2.75f;
		float mult = 7.5625f;

		if (t < 1 / div)
		{
			return mult * t * t;
		}
		else if (t < 2 / div)
		{
			t -= 1.5f / div;
			return mult * t * t + 0.75f;
		}
		else if (t < 2.5 / div)
		{
			t -= 2.25f / div;
			return mult * t * t + 0.9375f;
		}
		else
		{
			t -= 2.625f / div;
			return mult * t * t + 0.984375f;
		}
	}
	public static float InOutBounce(float t)
	{
		if (t < 0.5) return InBounce(t * 2) / 2;
		return 1 - InBounce((1 - t) * 2) / 2;
	}
	#endregion

	#region Probability Implementation

	static List<float> cumulativeProbability;
	public static int GetPrizeByProbability(List<float> probability) //[50,10,20,20]
	{
		//if your game will use this a lot of time it is best to build the arry just one time
		//and remove this function from here.
		if (!MakeCumulativeProbability(probability))
			return -1; //when it return false then the list excceded 100 in the last index

		float rnd = UnityEngine.Random.Range(1, 101); //Get a random number between 0 and 100

		for (int i = 0; i < probability.Count; i++)
		{
			if (rnd <= cumulativeProbability[i]) //if the probility reach the correct sum
			{
				return i; //return the item index that has been chosen 
			}
		}
		return -1; //return -1 if some error happens
	}

	public static int[] GetRandomDistribution(int sum, int amountOfNumbers)
	{
		int[] numbers = new int[amountOfNumbers];

		var random = new System.Random();

		for (int i = 0; i < amountOfNumbers; i++)
		{
			numbers[i] = random.Next(sum);
		}

		var compeleteSum = numbers.Sum();

		// Scale the numbers down to 0 -> sum
		for (int i = 0; i < amountOfNumbers; i++)
		{
			numbers[i] = (int)(((double)numbers[i] / compeleteSum) * sum);
		}

		// Due to rounding the number will most likely be below sum
		var resultSum = numbers.Sum();

		// Add +1 until we reach "sum"
		for (int i = 0; i < sum - resultSum; i++)
		{
			numbers[random.Next(0, amountOfNumbers)]++;
		}

		return numbers;
	}

	//this function creates the cumulative list
	static bool MakeCumulativeProbability(List<float> probability)
	{
		float probabilitiesSum = 0;
		if (cumulativeProbability == null)
			cumulativeProbability = new List<float>(); //reset the Array
		else
			cumulativeProbability.Clear();

		for (int i = 0; i < probability.Count; i++)
		{
			probabilitiesSum += probability[i]; //add the probability to the sum
			cumulativeProbability.Add(probabilitiesSum); //add the new sum to the list

			//All Probabilities need to be under 100% or it'll throw an exception
			if (probabilitiesSum > 100f)
			{
				Debug.LogError("Probabilities exceed 100%");
				return false;
			}
		}
		return true;
	}

	#endregion
}
