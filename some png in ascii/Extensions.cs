using System.Drawing;
namespace some_png_in_ascii
{
	public static class Extensions
	{
		public static void ToGrayscale(this Bitmap bm) //а
		{
			for (int y = 0; y < bm.Height; y++)
			{
				for (int x = 0; x < bm.Width; x++)
				{
					Color pixel = bm.GetPixel(x, y);
					int avg = (pixel.R + pixel.G + pixel.B) / 3; //Avarage(pixel.R, pixel.G, pixel.B);
					bm.SetPixel(x, y, Color.FromArgb(pixel.A, avg, avg, avg));
				}
			}
		}
	}
}