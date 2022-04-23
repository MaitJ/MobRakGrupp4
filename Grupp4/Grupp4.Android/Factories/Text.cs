using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XamAR.Core.Models;
using XamAR.Factory;
using Google.AR.Sceneform;
using Google.AR.Sceneform.Rendering;
using XamAR.Platform.Android.Sceneform;
using XamAR.Platform.Android.Sceneform.Extensions;

namespace Grupp4.Droid.Factories
{
    public class Text : ModelFactory<string>
    {
        public override ARModel CreateModel(string tag)
        {

            var nodeText = new Node()
            {
                LocalPosition = new Google.AR.Sceneform.Math.Vector3(0, 0.2f, 0.0f)
            };

            var text = new TextView(Application.Context)
            {
                Text = tag
            };

            text.SetTextColor(Android.Graphics.Color.AliceBlue);
            text.SetBackgroundColor(Android.Graphics.Color.GreenYellow);
            text.SetPadding(10, 10, 10, 10);

            ViewRenderable.InvokeBuilder().SetView(Application.Context, text).
                Build().ThenAccept(new DelegateConsumer<Renderable>((renderable) =>
                {
                    nodeText.Renderable = renderable;
                }));

            return nodeText.AsARModel();

        }
    }
}