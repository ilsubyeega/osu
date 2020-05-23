// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Diagnostics;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Game.Rulesets.Objects.Types;
using osu.Game.Rulesets.Scoring;
using osuTK;

namespace osu.Game.Rulesets.Osu.Objects.Drawables
{
    public class DrawableSliderHead : DrawableHitCircle
    {
        private readonly IBindable<Vector2> positionBindable = new Bindable<Vector2>();
        private readonly IBindable<int> pathVersion = new Bindable<int>();

        protected override OsuSkinComponents CirclePieceComponent => OsuSkinComponents.SliderHeadHitCircle;

        private readonly Slider slider;

        public DrawableSliderHead(Slider slider, SliderHeadCircle h)
            : base(h)
        {
            this.slider = slider;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            positionBindable.BindTo(HitObject.PositionBindable);
            pathVersion.BindTo(slider.Path.Version);

            positionBindable.BindValueChanged(_ => updatePosition());
            pathVersion.BindValueChanged(_ => updatePosition(), true);
        }
        protected override void CheckForResult(bool userTriggered, double timeOffset)
        {
            Debug.Assert(HitObject.HitWindows != null);

            if (!userTriggered)
            {
                if (!HitObject.HitWindows.CanBeHit(timeOffset))
                    ApplyResult(r => r.Type = HitResult.Miss);

                return;
            }

            var result = HitObject.HitWindows.ResultFor(timeOffset);

            if (result == HitResult.None || CheckHittable?.Invoke(this, Time.Current) == false)
            {
                Shake(Math.Abs(timeOffset) - HitObject.HitWindows.WindowFor(HitResult.Miss));
                return;
            }
            
            ApplyResult(r => r.Type = HitResult.Great);
        }

        protected override void Update()
        {
            base.Update();

            double completionProgress = Math.Clamp((Time.Current - slider.StartTime) / slider.Duration, 0, 1);

            //todo: we probably want to reconsider this before adding scoring, but it looks and feels nice.
            if (!IsHit)
                Position = slider.CurvePositionAt(completionProgress);
        }

        public Action<double> OnShake;

        protected override void Shake(double maximumLength) => OnShake?.Invoke(maximumLength);

        private void updatePosition() => Position = HitObject.Position - slider.Position;
    }
}
