using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace LootBox
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		public class Targy
		{
			public string nev { get; set; }
			public byte ritkasag { get; set; }

			public Targy(string sor)
			{
				nev = sor.Split(';')[0];
				ritkasag = Convert.ToByte(sor.Split(';')[1]);
			}

			public string ritkasagnev
			{
				get 
				{
					switch (ritkasag)
					{
						case 0: return "Common";
						case 1: return "Rare";
						case 2: return "Epic";
						default: return "Legendary";
					}
				}
			}
		}

		public List<Targy> targyak = new List<Targy>();
		public Window1()
		{
			InitializeComponent();
		}

		private void Bevitel(string item)
		{
			Targy uj = new Targy(item); //tárgyhozzáadás konstruktorral
			targyak.Add(uj);
			ListBoxItem elem = new ListBoxItem();
			elem.Content = uj.nev + "; " + uj.ritkasagnev;
			switch (uj.ritkasag)
			{
				case 0:
					{
						elem.Foreground = new SolidColorBrush(Color.FromRgb(17, 16, 16));
						break;
					}

				case 1:
					{
						elem.Foreground = new SolidColorBrush(Color.FromRgb(38, 57, 230));
						break;
					}

				case 2:
					{
						elem.Foreground = new SolidColorBrush(Color.FromRgb(247, 237, 15));
						break;
					}

				case 3:
					{
						elem.Foreground = new SolidColorBrush(Color.FromRgb(234, 28, 28));
						break;
					}
			}
			targyakListaja.Items.Add(elem);
		}
		private void betolt_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog allomanyMegnyitas = new OpenFileDialog(); //Az állomány megnyitás ablakának példányosítása
			allomanyMegnyitas.Filter = "Szöveges állományok (*.txt)|*.txt|Minden állomány (*.*)|*.*"; //A megfelelő állománytípusok beállítása
			if (allomanyMegnyitas.ShowDialog() == true) // Ha sikeres a ShowDialog, azaz a fájlkiválasztás
			{
				foreach (var item in File.ReadAllLines(allomanyMegnyitas.FileName)) //állománymegnyitás és végigolvasás
				{
					Bevitel(item);
				}
			}
		}

		private void mentes_Click(object sender, RoutedEventArgs e)
		{
			SaveFileDialog allomanyMentes = new SaveFileDialog(); //Az állománymentés ablakának példányosítása
			allomanyMentes.Filter = "Szöveges állományok (.txt)|.txt|Minden állomány (.)|."; //A megfelelő állománytípusok beállítása
			if (allomanyMentes.ShowDialog() == true) // Ha sikeres a ShowDialog, azaz a fájlkiválasztás
			{
				StreamWriter sw = new StreamWriter(allomanyMentes.FileName); //állománymegnyitás írásra
				foreach (var item in targyak)
				{
					sw.WriteLine(item.nev +"; "+item.ritkasagnev);
				}
				sw.Close();
			}
		}

		private void beviteles_Click(object sender, RoutedEventArgs e)
		{
			string nev = targyNeve.Text;
			int ritkasag = targyRitkasaga.SelectedIndex; //A listbox ritkaságának index visszaadása
			string item = nev + "; " + ritkasag ;
			Bevitel(item);
		}
	}
}
