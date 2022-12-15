using System.Drawing;
using System.Linq;
namespace some_png_in_ascii
{
	internal class BitmapToASCIIConverter
	{
		private readonly char[] asciiTable;// = " .:!/r(l1Z4H9W8$@".ToCharArray(); //{ '.', ',', ':', '+', '*', '?', '%', 's', '#', '@' };
		private readonly char[] reverseASCIITable;// = asciiTable.Reverse().ToArray();
		private readonly Bitmap bitmap;
		public BitmapToASCIIConverter(Bitmap bm) 
		{
			bitmap = bm;
			asciiTable = " .:!/r(l1Z4H9W8$@".ToCharArray(); //{ '.', ',', ':', '+', '*', '?', '%', 's', '#', '@' };
			reverseASCIITable = asciiTable.Reverse().ToArray();
		}
		public char[][] MyConvert(char[] gradient) => Convert(gradient);
		public char[][] Convert() => Convert(asciiTable);
		public char[][] ConvertInReverse() => Convert(reverseASCIITable);

		private char[][] Convert(char[] asciiTable)
		{
			var res = new char[bitmap.Height][];

			for (int y = 0; y < bitmap.Height; y++)
			{
				res[y] = new char[bitmap.Width];
				for (int x = 0; x < bitmap.Width; x++)
				{
					int mapIndex = (int)Map(bitmap.GetPixel(x, y).R, 0, 255, 0, asciiTable.Length - 1);
					res[y][x] = asciiTable[mapIndex];
				}
			}

			return res;
		}
		private float Map(float valueToMap, float start1, float stop1, float start2, float stop2) => ((valueToMap - start1) / (stop1 - start1)) * (stop2 - start2) + start2;
	}
}
