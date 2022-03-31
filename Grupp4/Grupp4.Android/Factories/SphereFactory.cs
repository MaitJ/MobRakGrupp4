﻿using Android.App;
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

namespace Grupp4.Droid.Factories
{
    class SphereFactory : ModelFactory
    {

        public override ARModel CreateModel()
        {
            var nodeSphere = new Node()
            {
                LocalPosition = new Vector3().ToAR()
            };
            Console.WriteLine("Creating model release");

            ModelRenderable model;
            MaterialFactory
                .MakeOpaqueWithColor(Android.App.Application.Context,
                    new Google.AR.Sceneform.Rendering.Color(0, 1, 0))
                .ThenAccept(
                    new DelegateConsumer<Material>((m) =>
                    {
                        model = ShapeFactory.MakeSphere(0.1f, new Vector3(0, 0, 0).ToAR(), m);
                        nodeSphere.Renderable = model;
                    })
                );
            return nodeSphere.AsARModel();

        }
    }
}