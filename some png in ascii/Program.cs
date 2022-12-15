using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
namespace some_png_in_ascii
{
	public class Program
	{
		private static string theFolderOnDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + $"\\ascii images";
		private static string theFileNameInFolder = theFolderOnDesktop + "\\";// + DateTime.Now.ToString("dd.MM.yyyy HH-mm-ss") + ".txt";// + "\\last opened image.txt";
		public const double WIDTH_OFFSET = 2.5; //5.0f / 2.0f; //24.0f / 11.0f;
		public const int MAX_WIDTH = 900; //600

		[STAThread]
		public static void Main()
		{
			Console.ForegroundColor = ConsoleColor.Green;
			OpenFileDialog fd = new OpenFileDialog() { Filter = "Images | *.bmp; *.png; *.jpg; *.JPEG" };

			while (true)
			{
				Console.ReadKey(true);

				if (fd.ShowDialog() != DialogResult.OK) continue;
				Console.Clear();

				Bitmap bm = new Bitmap(fd.FileName);
				bm = ResizeBitmap(bm);
				bm.ToGrayscale();

				BitmapToASCIIConverter converter = new BitmapToASCIIConverter(bm);
				char[][] rows = converter.Convert();

				foreach (char[] row in rows) Console.WriteLine(row);

				char[][] reverseCRows = converter.ConvertInReverse();
				if (!File.Exists(theFolderOnDesktop)) Directory.CreateDirectory(theFolderOnDesktop); //File.Create(theFolderOnDesktop);
				//using (FileStream fs = new FileStream(theFolderOnDesktop, FileMode.OpenOrCreate))

				File.WriteAllLines(theFileNameInFolder + fd.SafeFileName + ".txt", reverseCRows.Select(x => new string(x)));

				Console.SetCursorPosition(0, 0);
			}
		}
		public static Bitmap ResizeBitmap(Bitmap bm)
		{
			double newHeight = bm.Height / WIDTH_OFFSET * MAX_WIDTH / bm.Width;
			if (bm.Width > MAX_WIDTH || bm.Height > newHeight) bm = new Bitmap(bm, new Size(MAX_WIDTH, (int)newHeight));
			return bm;
		}
		//public static string 
	}
}
