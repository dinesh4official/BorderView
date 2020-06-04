using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace BorderView.Control
{
    [Preserve(AllMembers = true)]
    public class BorderView : ContentView
    {
        #region Bindable Properties

        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create("BorderColor", typeof(Color), typeof(BorderView), Color.FromHex("#E7E8E9"), BindingMode.Default, null);

        public static readonly BindableProperty BorderThicknessProperty = BindableProperty.Create("BorderThickness", typeof(Thickness), typeof(BorderView), new Thickness(), BindingMode.Default, null);

        public static readonly BindableProperty NeedSameBorderColorProperty = BindableProperty.Create("NeedSameBorderColor", typeof(bool), typeof(BorderView), true, BindingMode.Default, null);

        public static readonly BindableProperty LeftBorderColorProperty = BindableProperty.Create("LeftBorderColor", typeof(Color), typeof(BorderView), Color.FromHex("#E7E8E9"), BindingMode.Default, null);

        public static readonly BindableProperty RightBorderColorProperty = BindableProperty.Create("RightBorderColor", typeof(Color), typeof(BorderView), Color.FromHex("#E7E8E9"), BindingMode.Default, null);

        public static readonly BindableProperty TopBorderColorProperty = BindableProperty.Create("TopBorderColor", typeof(Color), typeof(BorderView), Color.FromHex("#E7E8E9"), BindingMode.Default, null);

        public static readonly BindableProperty BottomBorderColorProperty = BindableProperty.Create("BottomBorderColor", typeof(Color), typeof(BorderView), Color.FromHex("#E7E8E9"), BindingMode.Default, null);

        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { this.SetValue(BorderColorProperty, value); }
        }

        public Thickness BorderThickness
        {
            get { return (Thickness)GetValue(BorderThicknessProperty); }
            set { this.SetValue(BorderThicknessProperty, value); }
        }

        public bool NeedSameBorderColor
        {
            get { return (bool)GetValue(NeedSameBorderColorProperty); }
            set { this.SetValue(NeedSameBorderColorProperty, value); }
        }

        public Color LeftBorderColor
        {
            get { return (Color)GetValue(LeftBorderColorProperty); }
            set { this.SetValue(LeftBorderColorProperty, value); }
        }

        public Color RightBorderColor
        {
            get { return (Color)GetValue(RightBorderColorProperty); }
            set { this.SetValue(RightBorderColorProperty, value); }
        }

        public Color TopBorderColor
        {
            get { return (Color)GetValue(TopBorderColorProperty); }
            set { this.SetValue(TopBorderColorProperty, value); }
        }

        public Color BottomBorderColor
        {
            get { return (Color)GetValue(BottomBorderColorProperty); }
            set { this.SetValue(BottomBorderColorProperty, value); }
        }

        #endregion

        #region Constructor

        public BorderView()
        {

        }

        #endregion
    }
}