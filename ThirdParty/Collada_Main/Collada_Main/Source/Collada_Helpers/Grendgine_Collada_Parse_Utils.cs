using System;

namespace grendgine_collada
{
	public class Grendgine_Collada_Parse_Utils
	{
		public static int[] String_To_Int(string int_array)
		{
            char[] charSeparators = new char[] { '\n', ' ' };
            string[] str = int_array.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
			int[] array = new int[str.GetLongLength(0)];
			try
			{
				for (long i = 0; i < str.GetLongLength(0); i++)
				{
					array[i] = Convert.ToInt32(str[i]);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
				Console.WriteLine();
				Console.WriteLine(int_array);
			}
			return array;
		}
		
		public static float[] String_To_Float(string float_array)
		{
            char[] charSeparators = new char[] { '\n', ' ' };
            string[] str = float_array.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
			float[] array = new float[str.GetLongLength(0)];
			try
			{
				for (long i = 0; i < str.GetLongLength(0); i++)
				{
					array[i] = Convert.ToSingle(str[i]);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
				Console.WriteLine();
				Console.WriteLine(float_array);
			}
			return array;
		}
	
		public static bool[] String_To_Bool(string bool_array)
		{
            char[] charSeparators = new char[] { '\n', ' ' };
            string[] str = bool_array.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
			bool[] array = new bool[str.GetLongLength(0)];
			try
			{
				for (long i = 0; i < str.GetLongLength(0); i++)
				{
					array[i] = Convert.ToBoolean(str[i]);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
				Console.WriteLine();
				Console.WriteLine(bool_array);
			}
			return array;
		}
	}
}