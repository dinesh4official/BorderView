using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
#if Android
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BorderView.Droid.Mapping;
using Xamarin.Forms.Platform.Android;
#elif iOS
using CoreAnimation;
using CoreGraphics;
using Foundation;
using BorderView.iOS.Mapping;
using Xamarin.Forms.Platform.iOS;
#endif
using FormsBorderView = BorderView.Control.BorderView;
using FormsView = Xamarin.Forms.View;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(FormsBorderView), typeof(BorderViewRenderer))]
#if Android
namespace BorderView.Droid.Mapping
#elif iOS
namespace BorderView.iOS.Mapping
#endif
{
    [Preserve(AllMembers = true)]
    public class BorderViewRenderer : ViewRenderer
    {
        #region Fields

#if Android
        private Paint toppaintBorder;
        private Paint bottompaintBorder;
        private Paint leftpaintBorder;
        private Paint rightpaintBorder;
#elif iOS
        private CALayer leftBorderLayer;
        private CALayer topBorderLayer;
        private CALayer rightBorderLayer;
        private CALayer bottomBorderLayer;
#endif
        #endregion

        #region Properties

        private FormsBorderView BorderView
        {
            get { return this.Element as FormsBorderView; }
        }

        #endregion

        #region Constructor

#if Android
        public BorderViewRenderer(Context context) : base(context)
        {
            this.SetWillNotDraw(false);
            toppaintBorder = new Paint(PaintFlags.AntiAlias);
            toppaintBorder.SetStyle(Paint.Style.Fill);

            bottompaintBorder = new Paint(PaintFlags.AntiAlias);
            bottompaintBorder.SetStyle(Paint.Style.Fill);

            leftpaintBorder = new Paint(PaintFlags.AntiAlias);
            leftpaintBorder.SetStyle(Paint.Style.Fill);

            rightpaintBorder = new Paint(PaintFlags.AntiAlias);
            rightpaintBorder.SetStyle(Paint.Style.Fill);
        }
#elif iOS
        public BorderViewRenderer() : base()
        {

        }

#endif

        #endregion

        #region Override Methods

#if Android
        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);

            if (this.BorderView == null)
                return;

            var thickness = this.BorderView.BorderThickness;

            if (thickness.Left > 0)
                canvas.DrawRect(new Rect(0, 0, (int)thickness.Left, this.Height), leftpaintBorder);

            if (thickness.Top > 0)
                canvas.DrawRect(new Rect(0, 0, this.Width, (int)thickness.Top), toppaintBorder);

            if (thickness.Right > 0)
                canvas.DrawRect(new Rect(this.Width - (int)thickness.Right, 0, this.Width, this.Height), rightpaintBorder);

            if (thickness.Bottom > 0)
                canvas.DrawRect(new Rect(0, this.Height - (int)thickness.Bottom, this.Width, this.Height), bottompaintBorder);
        }
#endif
        protected override void OnElementChanged(ElementChangedEventArgs<FormsView> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
#if iOS
                leftBorderLayer = new CALayer();
                topBorderLayer = new CALayer();
                rightBorderLayer = new CALayer();
                bottomBorderLayer = new CALayer();

                this.NativeView.Layer.MasksToBounds = true;
                this.NativeView.Layer.AllowsEdgeAntialiasing = true;
                this.NativeView.Layer.BorderColor = Color.Transparent.ToCGColor();

                this.NativeView.Layer.AddSublayer(leftBorderLayer);
                this.NativeView.Layer.AddSublayer(topBorderLayer);
                this.NativeView.Layer.AddSublayer(rightBorderLayer);
                this.NativeView.Layer.AddSublayer(bottomBorderLayer);
#endif
                this.UpdateBorderViewColor();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
#if Android
            if (e.PropertyName == "BorderColor" || e.PropertyName == "NeedSameBorderColor" || e.PropertyName == "LeftBorderColor" || e.PropertyName == "RightBorderColor" || e.PropertyName == "TopBorderColor" || e.PropertyName == "BottomBorderColor")
            {
                this.UpdateBorderViewColor();
                this.Invalidate();
            }
            else if (e.PropertyName == "Height" || e.PropertyName == "Width" || e.PropertyName == "BorderThickness")
                this.Invalidate();
#elif iOS
            if (e.PropertyName == "Height" || e.PropertyName == "Width" || e.PropertyName == "BorderColor" || e.PropertyName == "BorderThickness" || e.PropertyName == "NeedSameBorderColor" || e.PropertyName == "LeftBorderColor" || e.PropertyName == "RightBorderColor" || e.PropertyName == "TopBorderColor" || e.PropertyName == "BottomBorderColor")
            {
                this.DrawBorderView();
            }
#endif
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
#if Android
                if (this.toppaintBorder != null)
                {
                    this.toppaintBorder.Dispose();
                    this.toppaintBorder = null;
                }

                if (this.bottompaintBorder != null)
                {
                    this.bottompaintBorder.Dispose();
                    this.bottompaintBorder = null;
                }

                if (this.leftpaintBorder != null)
                {
                    this.leftpaintBorder.Dispose();
                    this.leftpaintBorder = null;
                }

                if (this.rightpaintBorder != null)
                {
                    this.rightpaintBorder.Dispose();
                    this.rightpaintBorder = null;
                }
#elif iOS
                if (leftBorderLayer != null)
                {
                    leftBorderLayer.Dispose();
                    leftBorderLayer = null;
                }

                if (topBorderLayer != null)
                {
                    topBorderLayer.Dispose();
                    topBorderLayer = null;
                }

                if (rightBorderLayer != null)
                {
                    rightBorderLayer.Dispose();
                    rightBorderLayer = null;
                }

                if (bottomBorderLayer != null)
                {
                    bottomBorderLayer.Dispose();
                    bottomBorderLayer = null;
                }
#endif
            }
        }

        #endregion

        private void UpdateBorderViewColor()
        {
            if (!this.BorderView.NeedSameBorderColor)
            {
#if Android
                leftpaintBorder.Color = this.BorderView.LeftBorderColor.ToAndroid();
                toppaintBorder.Color = this.BorderView.TopBorderColor.ToAndroid();
                rightpaintBorder.Color = this.BorderView.RightBorderColor.ToAndroid();
                bottompaintBorder.Color = this.BorderView.BottomBorderColor.ToAndroid();
#elif iOS
                leftBorderLayer.BorderColor = this.BorderView.LeftBorderColor.ToCGColor();
                topBorderLayer.BorderColor = this.BorderView.TopBorderColor.ToCGColor();
                rightBorderLayer.BorderColor = this.BorderView.RightBorderColor.ToCGColor();
                bottomBorderLayer.BorderColor = this.BorderView.BottomBorderColor.ToCGColor();
#endif
            }
            else
            {
#if Android
                leftpaintBorder.Color = toppaintBorder.Color = rightpaintBorder.Color = bottompaintBorder.Color = this.BorderView.BorderColor.ToAndroid();
#elif iOS
                leftBorderLayer.BorderColor = topBorderLayer.BorderColor = rightBorderLayer.BorderColor = bottomBorderLayer.BorderColor = this.BorderView.BorderColor.ToCGColor();
#endif
            }
        }

#if iOS
        #region Private Methods

        private void DrawBorderView()
        {
            if (this.BorderView == null || this.BorderView.Bounds.Height <= 0 || this.BorderView.Width <= 0)
                return;

            var borderthickness = this.BorderView.BorderThickness;
            var formsrect = this.Element.Bounds;
            this.NativeView.Frame = new CGRect(this.NativeView.Frame.X, this.NativeView.Frame.Y, formsrect.Width, formsrect.Height);
            var nativerect = this.NativeView.Bounds;

            if (borderthickness.Left > 0)
            {
                leftBorderLayer.BorderWidth = (nfloat)borderthickness.Left;
                leftBorderLayer.Frame = new CGRect(nativerect.X, nativerect.Y, borderthickness.Left, nativerect.Height);
            }

            if (borderthickness.Right > 0)
            {
                rightBorderLayer.BorderWidth = (nfloat)borderthickness.Right;
                rightBorderLayer.Frame = new CGRect(nativerect.Right - borderthickness.Right, nativerect.Y, borderthickness.Right, nativerect.Height);
            }

            if (borderthickness.Top > 0)
            {
                topBorderLayer.BorderWidth = (nfloat)borderthickness.Top;
                topBorderLayer.Frame = new CGRect(nativerect.X, nativerect.Y, nativerect.Width, borderthickness.Top);
            }

            if (borderthickness.Bottom > 0)
            {
                bottomBorderLayer.BorderWidth = (nfloat)borderthickness.Bottom;
                bottomBorderLayer.Frame = new CGRect(nativerect.X, nativerect.Height - borderthickness.Bottom, nativerect.Width, nativerect.Height);
            }
        }

        #endregion
#endif
    }
}