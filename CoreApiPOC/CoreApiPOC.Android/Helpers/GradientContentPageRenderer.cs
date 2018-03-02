using System;
using Xamarin.Forms.Platform.Android;
using CoreApiPOC;
using CoreApiPOC.Droid.Helpers;

[assembly: Xamarin.Forms.ExportRenderer(typeof(GradientContentPage), typeof(GradientContentPageRenderer))]
namespace CoreApiPOC.Droid.Helpers
{
    public class GradientContentPageRenderer : PageRenderer
    {
        private Xamarin.Forms.Color StartColor { get; set; }
        private Xamarin.Forms.Color EndColor { get; set; }

        protected override void DispatchDraw(global::Android.Graphics.Canvas canvas)
        {
            var gradient = new Android.Graphics.LinearGradient(0, 0, Width, 0,
                this.StartColor.ToAndroid(),
                this.EndColor.ToAndroid(),
                Android.Graphics.Shader.TileMode.Mirror);
            var paint = new Android.Graphics.Paint()
            {
                Dither = true,
            };
            paint.SetShader(gradient);
            canvas.DrawPaint(paint);
            base.DispatchDraw(canvas);
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Page> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null || Element == null)
            {
                try
                {
                    var page = e.NewElement as GradientContentPage;
                    if (page != null)
                    {
                        this.StartColor = page.StartColor;
                        this.EndColor = page.EndColor;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(@"ERROR: ", ex.Message);
                }
            }
        }
    }
}