using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace System.Windows.Controls
{
	/// <summary>
	/// Interaction logic for CircularItemsGroupVisualization.xaml
	/// </summary>
	public partial class CircularItemsGroupVisualization : UserControl
	{
		private List<ImageSource> m_images = new List<ImageSource>();
		public ObservableCollection<string> SelectedEntitiesIds = new ObservableCollection<string>();
		public Point Center = new Point();

		public List<ImageSource> Images
		{
			get { return m_images; }
			set { m_images = value; }
		}
		System.Windows.Media.Effects.DropShadowEffect m_effect = new System.Windows.Media.Effects.DropShadowEffect();




		public CircularItemsGroupVisualization()
		{
			this.Loaded += CircularItemsGroupsVisualization_Loaded;

			InitializeComponent();
		}

		void CircularItemsGroupsVisualization_Loaded(object sender, RoutedEventArgs e)
		{
			m_effect.BlurRadius = 15;
			m_effect.Color = //(Color)ColorConverter.ConvertFromString("#FF009DFF"); 
				Colors.Black;
			m_effect.ShadowDepth = 0;
		}

		public void Visualize(List<ImageSource> images, double count, string displayName)
		{
			canvas.Children.Clear();

			this.m_images = images;

			// adding items to the canvas; the center of the canvas should be 84/84, or 42/42
			// in all cases we need to resize provided images to be small enough;
			// in our prototype app we had radius = 1.5 * half of width/height; what is fixed in our case?
			// so in total we can have 84zerc
			// a good question, seems nothing is fixed; so, we'll set width to be fixed; 
			// we will make height also fixed

			if (m_images.Count > 0)
			{
				BrushConverter bc = new BrushConverter();

					var height = (this.canvas.Height / 3);
					var width = (this.canvas.Width / 3);
				
				if (m_images.Count==1)
				{
					height = (this.canvas.Height / 2);
					width = (this.canvas.Width / 2);
				}

				// first item
				Border img0 = new Border();
				img0.HorizontalAlignment = Windows.HorizontalAlignment.Stretch;
				img0.VerticalAlignment = Windows.VerticalAlignment.Stretch;
				img0.Background = (SolidColorBrush)bc.ConvertFromString("#FFE6E6E6"); // Brushes.Black;
				img0.Height = height;
				img0.Width = width;
				img0.BorderThickness = new Thickness(1);
				img0.BorderBrush = (SolidColorBrush)bc.ConvertFromString("#FFF3F3F3");
				img0.CornerRadius = new CornerRadius(1);

				img0.Effect = Effect;

				Rectangle r0 = new Rectangle();
				r0.RadiusX = 1;
				r0.RadiusY = 1;
				r0.Width = width - 2;
				r0.Height = height - 2;
				
				// brush
				var b0 = new ImageBrush(m_images[0]);
				b0.Stretch = Stretch.Uniform;

				r0.Fill = b0;

				img0.Child = r0;

				// prepare the rest

				// radius
				double r = height / 2;
				double R = r * 1.5;

				// center
				double xc = this.canvas.Width / 2;
				double yc = this.canvas.Height / 2;

				// good, its coordinates should be Xc, Yc - r where r= 16.8 (42 / 2.5)
				// 2.5 cause r+0.5r+r to get a round into the quadrat
				double x0 = xc - r;
				double y0 = yc - r;

				Canvas.SetLeft(img0, x0);
				Canvas.SetTop(img0, y0);

				// adding to canvas
				canvas.Children.Add(img0);

				// setting up the display name
				if (count == 1)
					this.majorText.Text = displayName;
				else if (count > 1)
				{
					this.majorText.Text = count + " items selected";
				}

				if (m_images.Count > 1)
				{
					// alpha is degree
					double alphai = 0;
					// delta 
					double deltaAlpha = 360 / (m_images.Count - 1);

					int i = 0;

					List<double> degrees = new List<double>();

					for (i = 0; i < m_images.Count; i++)
					{
						Border img = new Border();
						img.HorizontalAlignment = Windows.HorizontalAlignment.Stretch;
						img.VerticalAlignment = Windows.VerticalAlignment.Stretch;
						img.Background = (SolidColorBrush)bc.ConvertFromString("#FFE6E6E6");
						img.Height = height;
						img.Width = width;
						img.BorderThickness = new Thickness(1);
						img.BorderBrush = (SolidColorBrush)bc.ConvertFromString("#FFF3F3F3");
						img.CornerRadius = new CornerRadius(1);

						img.Effect = Effect;

						Rectangle re = new Rectangle();
						re.RadiusX = 1;
						re.RadiusY = 1;
						re.Width = width - 2;
						re.Height = height - 2;

						// brush
						var br = new ImageBrush(m_images[i]);
						br.Stretch = Stretch.Uniform;

						//
						re.Fill = br;

						img.Child = re;

						// alpha i is alpha + delta 
						alphai = alphai + deltaAlpha;

						var xi = xc + R * Math.Cos(DegreeToRadian(alphai));
						var yi = yc - R * Math.Sin(DegreeToRadian(alphai));

						// but, xi and yi are centers of new positions, so we need to deconstruct those

						var xicorr = xi - r;
						var yicorr = yi - r;

						// now, assigning new data
						Canvas.SetLeft(img, xicorr);
						Canvas.SetTop(img, yicorr);

						Canvas.SetZIndex(img, i);

						this.canvas.Children.Add(img);

						var degree = alphai % 360;

						if (degree < 0)
						{
							degree += 360;
						}

						degrees.Add(degree);
					}

					var imgs = this.canvas.Children.OfType<Border>().ToList();

					for (int j = 0; j < imgs.Count; j++)
					{
						Canvas.SetZIndex(imgs[imgs.Count - i-1], i);
					}
				}
			}
		}

		private double DegreeToRadian(double angle)
		{
			return Math.PI * angle / 180.0;
		}
	} // class
} // namespace
