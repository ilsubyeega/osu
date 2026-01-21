// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Pooling;
using osu.Game.Graphics;
using osu.Game.Graphics.Sprites;
using osu.Game.Rulesets.Osu.Configuration;
using osu.Game.Rulesets.Scoring;
using osuTK;
using osuTK.Graphics;
using Vortice.DXGI;

namespace osu.Game.Rulesets.Osu.UI
{
    /// <summary>
    /// A poolable drawable that displays hit timing error for osu! hit objects.
    /// </summary>
    public partial class HitErrorDisplay : PoolableDrawable
    {
        private const float move_distance = 10f;
        private const double fade_in_duration = 400;
        private const double move_duration = 200;
        private const double fade_out_duration = 200;

        private const float alpha = 0.6f;

        private OsuSpriteText errorText = null!;
        private Container content = null!;

        // Properties set before PrepareForUse
        public double TimeOffset { get; set; }
        public HitResult Result { get; set; }
        public float ObjectScale { get; set; } = 1f;
        public double BeatLength { get; set; } = 500;

        // Configuration bindables
        private readonly Bindable<HitErrorDisplayStyle> displayStyle = new Bindable<HitErrorDisplayStyle>();
        private readonly Bindable<HitErrorColorScheme> colorScheme = new Bindable<HitErrorColorScheme>();
        private readonly Bindable<bool> showPerfect = new Bindable<bool>();
        private readonly Bindable<bool> scaleWithCS = new Bindable<bool>();
        private readonly Bindable<bool> instantShow = new Bindable<bool>();
        private readonly Bindable<double> disappearDelay = new Bindable<double>();

        [BackgroundDependencyLoader]
        private void load(OsuRulesetConfigManager? rulesetConfig)
        {
            AutoSizeAxes = Axes.Both;
            Origin = Anchor.Centre;

            InternalChild = content = new Container
            {
                AutoSizeAxes = Axes.Both,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Child = errorText = new OsuSpriteText
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Font = OsuFont.Torus.With(size: 16, weight: FontWeight.Bold),
                    Shadow = true,
                    ShadowColour = Color4.Black.Opacity(0.5f),
                }
            };

            rulesetConfig?.BindWith(OsuRulesetSetting.HitErrorDisplayStyle, displayStyle);
            rulesetConfig?.BindWith(OsuRulesetSetting.HitErrorColorScheme, colorScheme);
            rulesetConfig?.BindWith(OsuRulesetSetting.HitErrorShowPerfect, showPerfect);
            rulesetConfig?.BindWith(OsuRulesetSetting.HitErrorScaleWithCS, scaleWithCS);
            rulesetConfig?.BindWith(OsuRulesetSetting.HitErrorInstantShow, instantShow);
            rulesetConfig?.BindWith(OsuRulesetSetting.HitErrorDisappearDelay, disappearDelay);
        }

        protected override void PrepareForUse()
        {
            base.PrepareForUse();

            // Check if we should skip perfect hits
            bool isPerfect = Result == HitResult.Perfect || Result == HitResult.Great;

            if (isPerfect && !showPerfect.Value)
            {
                Expire();
                return;
            }

            // Update text and colour
            errorText.Text = formatErrorText(TimeOffset, displayStyle.Value, BeatLength, isPerfect);
            errorText.Colour = getColour(TimeOffset, Result, colorScheme.Value, isPerfect);

            // Apply scale
            float scale = scaleWithCS.Value ? ObjectScale : 1f;
            content.Scale = new Vector2(scale);

            // Clear transforms and set initial state
            ClearTransforms(true);

            Alpha = 0;
            content.Y = -move_distance;

            this
                .ScaleTo(1f)
                .FadeTo(alpha, instantShow.Value ? 0 : fade_in_duration, Easing.OutQuint);

            // Move upward animation
            content.MoveToY(0, move_duration, Easing.OutQuint);

            // Delay then fade out
            this.Delay(fade_in_duration + disappearDelay.Value)
                .ScaleTo(0f, fade_out_duration * 1.5, Easing.InQuint)
                .FadeOut(fade_out_duration, Easing.InQuint)
                .Delay(fade_out_duration)
                .Expire();
        }

        private static string formatErrorText(double offset, HitErrorDisplayStyle style, double beatLength, bool isPerfect)
        {
            int offsetMs = (int)Math.Round(offset);
            string sign = offset >= 0 ? "+" : "";

            switch (style)
            {
                case HitErrorDisplayStyle.Milliseconds:
                    return $"{sign}{offsetMs}ms";

                case HitErrorDisplayStyle.Numeric:
                    return $"{sign}{offsetMs}";

                case HitErrorDisplayStyle.BeatFraction:
                    return formatBeatFraction(offset, beatLength);

                case HitErrorDisplayStyle.TextLabel:
                    if (isPerfect)
                        return "GREAT";
                    return offset < 0 ? "EARLY" : "LATE";

                default:
                    return $"{sign}{offsetMs}ms";
            }
        }

        private static string formatBeatFraction(double offset, double beatLength)
        {
            if (beatLength <= 0)
                beatLength = 500;

            double absOffset = Math.Abs(offset);
            string sign = offset >= 0 ? "+" : "-";

            // Try common beat divisors
            int[] divisors = { 1, 2, 3, 4, 6, 8 };

            foreach (int divisor in divisors)
            {
                double fractionMs = beatLength / divisor;
                double count = absOffset / fractionMs;

                // If it's close to a whole number of this fraction
                if (Math.Abs(count - Math.Round(count)) < 0.1 && Math.Round(count) >= 1)
                {
                    int wholeCount = (int)Math.Round(count);

                    if (wholeCount == 1)
                        return $"{sign}1/{divisor}";

                    return $"{sign}{wholeCount}/{divisor}";
                }
            }

            // Fallback: just show as fraction of beat
            double beatFraction = absOffset / beatLength;

            if (beatFraction < 0.01)
                return "0";

            // Find closest simple fraction
            return $"{sign}~1/{(int)Math.Round(1 / beatFraction)}";
        }

        private static Color4 getColour(double offset, HitResult result, HitErrorColorScheme scheme, bool isPerfect)
        {
            switch (scheme)
            {
                case HitErrorColorScheme.Standard:
                    return getStandardColour(result);

                case HitErrorColorScheme.PerfectGray:
                    if (isPerfect)
                        return Color4.Gray;
                    return getStandardColour(result);

                case HitErrorColorScheme.FastSlow:
                    if (isPerfect)
                        return Color4.White;
                    return offset < 0 ? Color4.IndianRed : Color4.LightBlue;

                default:
                    return Color4.White;
            }
        }

        private static Color4 getStandardColour(HitResult result)
        {
            switch (result)
            {
                case HitResult.Perfect:
                case HitResult.Great:
                    return new Color4(0.4f, 0.6f, 1f, 1f); // Blue

                case HitResult.Good:
                case HitResult.Ok:
                    return new Color4(0.4f, 1f, 0.4f, 1f); // Green

                case HitResult.Meh:
                    return new Color4(1f, 0.9f, 0.2f, 1f); // Yellow

                default:
                    return Color4.White;
            }
        }
    }
}
