using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Google.AR.Sceneform;
using Google.AR.Sceneform.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using XamAR.Core.Models;
using XamAR.Factory;
using XamAR.Platform.Android.Sceneform;
using XamAR.Platform.Android.Sceneform.Extensions;
using Xamarin.Essentials;

namespace Grupp4.Droid.Factories
{
    public class ArModel : ModelFactory
    {
        public override ARModel CreateModel()
        {
            var nodeItem = new Node()
            {
                LocalPosition = new Vector3(0, 0, 0).ToAR()
            };

            var builder = ModelRenderable.InvokeBuilder();
            var javaClass = Java.Lang.Class.FromType(builder.GetType());

            var methods = javaClass.GetMethods();
            var method = methods[11];
            method.Invoke(builder, Platform.CurrentActivity, Resource.Raw.TestModel);
            builder.Build((m) =>
            {
                var model = m;
                MaterialFactory.MakeOpaqueWithColor(Android.App.Application.Context,
                    new Google.AR.Sceneform.Rendering.Color(1.00f, 0.87f, 0.62f)).ThenAccept(
                        new DelegateConsumer<Material>((m) =>
                        {
                            m.SetInt("metallic", 1);
                            m.SetFloat("reflectance", 1.5f);
                            m.SetFloat3("baseColor", new Google.AR.Sceneform.Math.Vector3(1.00f, 0.87f, 0.62f));
                            model.SetMaterial(0, m);
                            nodeItem.Renderable = model;
                        }));
            });

            return nodeItem.AsARModel();
        }
    }
}