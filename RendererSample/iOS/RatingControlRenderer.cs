using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using System.Collections.Generic;
using RendererSample.Controls;
using RendererSample.iOS;

[assembly: ExportRenderer(typeof(RatingControl), typeof(RatingControlRenderer))]
namespace RendererSample.iOS
{
	public class RatingControlRenderer : ViewRenderer<RatingControl, UIStackView>
	{
		UIImage _filledStar = new UIImage("1472304472_star-4.png");
		UIImage _unfilledStar = new UIImage("1472305975_star-0.png");
		bool _isReadOnly = false;

		private Dictionary<int, bool> _startState;

		public RatingControlRenderer()
		{
			_startState = new Dictionary<int, bool>();
		
		}

		protected override void OnElementChanged(ElementChangedEventArgs<RatingControl> e)
		{
			base.OnElementChanged(e);

			if (Control == null)
			{
				
				SetNativeControl(new UIStackView());
				InitializeUIComponent();
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
			if (e.PropertyName == RatingControl.ValueProperty.PropertyName
			    && !_isReadOnly)
			{
				nint changedVaue =(nint)Math.Round(senderControl.Value);
			
				UpdateStars(changedVaue-1);
			}
			if (e.PropertyName == RatingControl.IsReadOnlyProperty.PropertyName)
			{
				_isReadOnly = this.Element.IsReadOnly;
			}
			base.OnElementPropertyChanged(sender, e);
		}

		void InitializeUIComponent()
		{
			this.Control.Axis = UILayoutConstraintAxis.Horizontal;
			_isReadOnly = this.Element.IsReadOnly;
			this.Control.Distribution = UIStackViewDistribution.EqualCentering;
			for (int i = 0; i < this.Element.Count; i++)
			{
				bool starState = i < this.Element.Value;

				var star = GetStarIcon(i, starState);

				this.Control.AddArrangedSubview(star);
			}
		}

		void UpdateStars(nint tag)
		{
			this.Element.SetValue(RatingControl.ValueProperty, (double)tag+1);
			if (tag < 0) return;
			if (_startState[(int)tag])
			{

				UnFillStars((int)tag);

			}
			else
			{
				FillStars((int)tag);

			}
		}

		void FillStars(int tag)
		{
			for (int i = 0; i <= tag; i++)
			{
				var state = _startState[i];

				if (!state)
				{
					var star = GetStarIcon(i, true);
					var currentStar = this.Control.ArrangedSubviews[i];
					currentStar.RemoveFromSuperview();
					this.Control.InsertArrangedSubview(star, (nuint)i);
				}
			}
		}

		void UnFillStars(int tag)
		{
			for (int i = _startState.Count - 1; i > tag; i--)
			{
				var item = _startState[i];

				if (item)
				{
					var star = GetStarIcon(i, false);
					var currentStar = this.Control.ArrangedSubviews[i];
					currentStar.RemoveFromSuperview();
					this.Control.InsertArrangedSubview(star, (nuint)i);
				}
			}
			 
		}


		UIImageView GetStarIcon(int tag, bool isFilled)
		{
			UIImageView starImageView = null;
			if (isFilled)
				starImageView = new UIImageView(_filledStar);
			else
				starImageView = new UIImageView(_unfilledStar);
			_startState[tag] = isFilled;
			starImageView.Tag = tag;
			UIGestureRecognizer TapRecognizer = new UITapGestureRecognizer(OnStar_Clicked);
			starImageView.ContentMode = UIViewContentMode.ScaleAspectFit;
			starImageView.WidthAnchor.ConstraintEqualTo(20);
			starImageView.AddGestureRecognizer(TapRecognizer);
			starImageView.UserInteractionEnabled = true;

			return starImageView;
		}


		void OnStar_Clicked(UITapGestureRecognizer obj)
		{
			if (_isReadOnly) return;
			nint tag = obj.View.Tag;
			UpdateStars(tag);

		}

	}
}