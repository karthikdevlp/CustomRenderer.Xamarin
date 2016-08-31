using System;
using Xamarin.Forms;
namespace RendererSample.Controls
{
	public class RatingControl:View
	{
		public double Value
		{
			get
			{
				return (double)GetValue(ValueProperty);
			}
			set
			{
				SetValue(ValueProperty,value);
			}
		}
		public static BindableProperty ValueProperty = BindableProperty.Create<RatingControl, double>(p => p.Value,0);


		public int Count
		{
			get
			{
				return (int)GetValue(CountProperty);
			}
			set
			{ 
				SetValue(CountProperty,value);
			}
		}

		public static BindableProperty CountProperty = BindableProperty.Create<RatingControl, int>(p => p.Count, 0);

		public bool IsReadOnly
		{
			get { return (bool)GetValue(IsReadOnlyProperty); }
			set { SetValue(IsReadOnlyProperty,value);}
		}

		public static BindableProperty IsReadOnlyProperty = BindableProperty.Create<RatingControl, bool>(p => p.IsReadOnly, false);

		public RatingControl()
		{ 
		}


	}
}

