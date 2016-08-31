using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Widget;
using System.Collections.Generic;
using RendererSample.Controls;
using RendererSample.Droid;

[assembly: ExportRenderer(typeof(RatingControl), typeof(RatingControlRenderer))]
namespace RendererSample.Droid
{
	public class RatingControlRenderer : ViewRenderer<RatingControl, RatingBar>
	{

		protected override void OnElementChanged(ElementChangedEventArgs<RatingControl> e)
		{
			base.OnElementChanged(e);

			if (Control == null)
			{
				var ratingBar = new RatingBar(this.Context);
				ratingBar.StepSize = 1;
				ratingBar.RatingBarChange+=RatingBar_RatingBarChange;
				ratingBar.NumStars = this.Element.Count;
				ratingBar.Rating =(float) this.Element.Value;
				ratingBar.IsIndicator = this.Element.IsReadOnly;
				SetNativeControl(ratingBar);
			}

			if (e.NewElement != null)
			{
				//I don't have any events to register
			}

			if (e.OldElement != null)
			{
				//I don't have any events to un register
			}
		}



		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			RatingControl senderControl = sender as RatingControl;
			if (e.PropertyName == RatingControl.ValueProperty.PropertyName)
			{ 
					if (this.Control.IsIndicator) return;
					this.Control.Rating = (float)Math.Round(this.Element.Value);
				 
			}
			if (e.PropertyName == RatingControl.IsReadOnlyProperty.PropertyName)
			{
				this.Control.IsIndicator = senderControl.IsReadOnly;
			}
			base.OnElementPropertyChanged(sender, e);
		}

		void RatingBar_RatingBarChange(object sender, RatingBar.RatingBarChangeEventArgs e)
		{
			if (this.Control == null) return;
			if (this.Control.IsIndicator) return;
			this.Element.SetValue(RatingControl.ValueProperty,(int) e.Rating);
		}
	}
}

