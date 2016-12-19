﻿using System.Collections.Generic;
using System.Linq;
using Sketching.Common.Interfaces;
using Xamarin.Forms;

namespace Sketching.Common.Views
{
	public partial class ToolSettingsView : ContentView
	{
		private readonly StackOrientation _orientation;
		private Dictionary<string, Color> _colorPalette;

		public ToolSettingsView(ITool tool)
		{
			BindingContext = new ToolSettingsViewModel(tool, Navigation);
			InitializeComponent();
			thinLineImage.Source = ImageSource.FromResource("Sketching.Common.Resources.ThinLine.png");
			thickLineImage.Source = ImageSource.FromResource("Sketching.Common.Resources.ThickLine.png");
			_orientation = Width > Height ? StackOrientation.Horizontal : StackOrientation.Vertical;
			CreateColorPalette();
			SetupColorGrid();
			FillColorGrid();
		}

		private void CreateColorPalette()
		{
			_colorPalette = new Dictionary<string, Color>
			{
				{ "White", Color.White },
				{ "Silver", Color.Silver },
				{ "Gray", Color.Gray },
				{ "Black", Color.Black },
				{ "Orange", Color.Orange },
				{ "Yellow", Color.Yellow },
				{ "Aqua", Color.Aqua },
				{ "Blue", Color.Blue },
				{ "Navy", Color.Navy },
				{ "Lime", Color.Lime },
				{ "Green", Color.Green },
				{ "Teal", Color.Teal },
				{ "Fuchsia", Color.Fuchsia },
				{ "Red", Color.Red },
				{ "Purple", Color.Purple },
			};
		}


		private void SetupColorGrid()
		{
			colorGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
			colorGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
			colorGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

			colorGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
			colorGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
			colorGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

			if (_orientation == StackOrientation.Vertical)
			{
				colorGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
				colorGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
			}
			else
			{
				colorGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
				colorGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
			}
		}

		private void FillColorGrid()
		{
			var i = 1;
			var left = -1;
			var top = 0;
			foreach (var label in _colorPalette.Select(color => new Label
			{
				HorizontalTextAlignment = TextAlignment.Center,
				VerticalTextAlignment = TextAlignment.Center,
				FontSize = 10.0,
				Text = string.Empty,
				TextColor = GetTextColor(color.Value),
				BackgroundColor = color.Value
			}))
			{
				var tapGestureRecognizer = new TapGestureRecognizer
				{
					Command = ((ToolSettingsViewModel)BindingContext).ColorSelectedCommand,
					CommandParameter = label.BackgroundColor
				};

				label.GestureRecognizers.Add(tapGestureRecognizer);

				left++;
				if (_orientation == StackOrientation.Vertical)
				{
					if (i == 4 || i == 7 || i == 10 || i == 13) // New line
					{
						left = 0;
						top++;
					}
				}
				else
				{
					if (i == 6 || i == 11) // New line
					{
						left = 0;
						top++;
					}
				}
				colorGrid.Children.Add(label, left, top);
				i++;
			}
		}

		private static Color GetTextColor(Color backgroundColor)
		{
			var backgroundColorDelta = ((backgroundColor.R * 0.3) + (backgroundColor.G * 0.6) + (backgroundColor.B * 0.1));
			return (backgroundColorDelta > 0.4f) ? Color.Black : Color.White; // Returns black or white text depending on the delta channel
		}
	}
}
