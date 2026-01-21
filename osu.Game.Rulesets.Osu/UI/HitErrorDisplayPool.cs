// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Pooling;
using osu.Framework.Graphics.Textures;
using osu.Framework.Logging;
using osu.Game.Beatmaps;
using osu.Game.Rulesets.Judgements;
using osu.Game.Rulesets.Objects.Drawables;
using osu.Game.Rulesets.Osu.Configuration;
using osu.Game.Rulesets.Osu.Objects.Drawables;
using osu.Game.Rulesets.Scoring;

namespace osu.Game.Rulesets.Osu.UI
{
    /// <summary>
    /// Container that pools and manages <see cref="HitErrorDisplay"/> instances.
    /// </summary>
    public partial class HitErrorDisplayPool : CompositeDrawable
    {
        private readonly DrawablePool<HitErrorDisplay> pool;
        private readonly Container displayContainer;

        private readonly Bindable<bool> enabled = new Bindable<bool>();

        [Resolved]
        private IBeatmap beatmap { get; set; } = null!;

        public HitErrorDisplayPool()
        {
            RelativeSizeAxes = Axes.Both;

            InternalChildren = new Drawable[]
            {
                pool = new DrawablePool<HitErrorDisplay>(20),
                displayContainer = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                }
            };
        }

        [BackgroundDependencyLoader]
        private void load(OsuRulesetConfigManager? rulesetConfig)
        {
            rulesetConfig?.BindWith(OsuRulesetSetting.HitErrorDisplayEnabled, enabled);
        }

        /// <summary>
        /// Displays a hit error for the given result.
        /// </summary>
        /// <param name="judgedObject">The drawable hit object that was judged.</param>
        /// <param name="result">The judgement result.</param>
        public void ShowResult(DrawableHitObject judgedObject, JudgementResult result)
        {
            if (!enabled.Value)
                return;

            // Only show for basic hit results
            if (!isValidResult(result.Type))
                return;

            // debug log
            Logger.Log($"HitErrorDisplay: {result.Type} at {result.TimeOffset}ms");
            Logger.Log($"{judgedObject}");

            var display = pool.Get(d =>
            {
                d.TimeOffset = result.TimeOffset;
                d.Result = result.Type;

                // Get the position from the judged object
                // The sliderhead itself has no position, so we need to get the position from the parent slider.
                if (judgedObject is DrawableSliderHead head)
                {
                    d.Position = head.DrawableSlider.HitObject.Position;
                    d.ObjectScale = head.DrawableSlider.HitObject.Scale;
                }
                else if (judgedObject is DrawableHitCircle osuObject)
                {
                    d.Position = osuObject.Position;
                    d.ObjectScale = osuObject.HitObject.Scale;
                }
                else
                {
                    d.Position = judgedObject.Position;
                    d.ObjectScale = 1f;
                }

                // Get beat length from the beatmap's timing point at the hit time
                d.BeatLength = beatmap.ControlPointInfo.TimingPointAt(result.TimeAbsolute).BeatLength;
            });

            displayContainer.Add(display);
        }

        private static bool isValidResult(HitResult result)
        {
            switch (result)
            {
                case HitResult.Perfect:
                case HitResult.Great:
                case HitResult.Good:
                case HitResult.Ok:
                case HitResult.Meh:
                    return true;

                default:
                    return false;
            }
        }
    }
}
