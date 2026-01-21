// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Localisation;

namespace osu.Game.Localisation
{
    public static class RulesetSettingsStrings
    {
        private const string prefix = @"osu.Game.Resources.Localisation.RulesetSettings";

        /// <summary>
        /// "Rulesets"
        /// </summary>
        public static LocalisableString Rulesets => new TranslatableString(getKey(@"rulesets"), @"Rulesets");

        /// <summary>
        /// "Snaking in sliders"
        /// </summary>
        public static LocalisableString SnakingInSliders => new TranslatableString(getKey(@"snaking_in_sliders"), @"Snaking in sliders");

        /// <summary>
        /// "Snaking out sliders"
        /// </summary>
        public static LocalisableString SnakingOutSliders => new TranslatableString(getKey(@"snaking_out_sliders"), @"Snaking out sliders");

        /// <summary>
        /// "Cursor trail"
        /// </summary>
        public static LocalisableString CursorTrail => new TranslatableString(getKey(@"cursor_trail"), @"Cursor trail");

        /// <summary>
        /// "Cursor ripples"
        /// </summary>
        public static LocalisableString CursorRipples => new TranslatableString(getKey(@"cursor_ripples"), @"Cursor ripples");

        /// <summary>
        /// "Playfield border style"
        /// </summary>
        public static LocalisableString PlayfieldBorderStyle => new TranslatableString(getKey(@"playfield_border_style"), @"Playfield border style");

        /// <summary>
        /// "None"
        /// </summary>
        public static LocalisableString BorderNone => new TranslatableString(getKey(@"no_borders"), @"None");

        /// <summary>
        /// "Corners"
        /// </summary>
        public static LocalisableString BorderCorners => new TranslatableString(getKey(@"corner_borders"), @"Corners");

        /// <summary>
        /// "Full"
        /// </summary>
        public static LocalisableString BorderFull => new TranslatableString(getKey(@"full_borders"), @"Full");

        /// <summary>
        /// "Scrolling direction"
        /// </summary>
        public static LocalisableString ScrollingDirection => new TranslatableString(getKey(@"scrolling_direction"), @"Scrolling direction");

        /// <summary>
        /// "Up"
        /// </summary>
        public static LocalisableString ScrollingDirectionUp => new TranslatableString(getKey(@"scrolling_up"), @"Up");

        /// <summary>
        /// "Down"
        /// </summary>
        public static LocalisableString ScrollingDirectionDown => new TranslatableString(getKey(@"scrolling_down"), @"Down");

        /// <summary>
        /// "Scroll speed"
        /// </summary>
        public static LocalisableString ScrollSpeed => new TranslatableString(getKey(@"scroll_speed"), @"Scroll speed");

        /// <summary>
        /// "Timing-based note colouring"
        /// </summary>
        public static LocalisableString TimingBasedColouring => new TranslatableString(getKey(@"Timing_based_colouring"), @"Timing-based note colouring");

        /// <summary>
        /// "{0}ms (speed {1:N1})"
        /// </summary>
        public static LocalisableString ScrollSpeedTooltip(int scrollTime, double scrollSpeed) => new TranslatableString(getKey(@"ruleset"), @"{0}ms (speed {1:N1})", scrollTime, scrollSpeed);

        /// <summary>
        /// "Touch control scheme"
        /// </summary>
        public static LocalisableString TouchControlScheme => new TranslatableString(getKey(@"touch_control_scheme"), @"Touch control scheme");

        /// <summary>
        /// "Mobile layout"
        /// </summary>
        public static LocalisableString MobileLayout => new TranslatableString(getKey(@"mobile_layout"), @"Mobile layout");

        /// <summary>
        /// "Portrait"
        /// </summary>
        public static LocalisableString Portrait => new TranslatableString(getKey(@"portrait"), @"Portrait");

        /// <summary>
        /// "Landscape"
        /// </summary>
        public static LocalisableString Landscape => new TranslatableString(getKey(@"landscape"), @"Landscape");

        /// <summary>
        /// "Landscape (expanded columns)"
        /// </summary>
        public static LocalisableString LandscapeExpandedColumns => new TranslatableString(getKey(@"landscape_expanded_columns"), @"Landscape (expanded columns)");

        /// <summary>
        /// "Touch overlay"
        /// </summary>
        public static LocalisableString TouchOverlay => new TranslatableString(getKey(@"touch_overlay"), @"Touch overlay");

        /// <summary>
        /// "Show hit error display"
        /// </summary>
        public static LocalisableString HitErrorDisplayEnabled => new TranslatableString(getKey(@"hit_error_display_enabled"), @"Show hit error display (experimental)");

        /// <summary>
        /// "Hit error style"
        /// </summary>
        public static LocalisableString HitErrorDisplayStyle => new TranslatableString(getKey(@"hit_error_display_style"), @"Hit error style");

        /// <summary>
        /// "Hit error colour scheme"
        /// </summary>
        public static LocalisableString HitErrorColorScheme => new TranslatableString(getKey(@"hit_error_color_scheme"), @"Hit error colour scheme");

        /// <summary>
        /// "Show perfect hits"
        /// </summary>
        public static LocalisableString HitErrorShowPerfect => new TranslatableString(getKey(@"hit_error_show_perfect"), @"Show perfect hits");

        /// <summary>
        /// "Scale with circle size"
        /// </summary>
        public static LocalisableString HitErrorScaleWithCS => new TranslatableString(getKey(@"hit_error_scale_with_cs"), @"Scale with circle size");

        /// <summary>
        /// "Instant show (no fade-in)"
        /// </summary>
        public static LocalisableString HitErrorInstantShow => new TranslatableString(getKey(@"hit_error_instant_show"), @"Instant show (no fade-in)");

        /// <summary>
        /// "Disappear delay"
        /// </summary>
        public static LocalisableString HitErrorDisappearDelay => new TranslatableString(getKey(@"hit_error_disappear_delay"), @"Disappear delay");

        /// <summary>
        /// "Hide original judgements"
        /// </summary>
        public static LocalisableString HitErrorHideJudgements => new TranslatableString(getKey(@"hit_error_hide_judgements"), @"Hide original judgements");

        /// <summary>
        /// "Milliseconds (+12ms)"
        /// </summary>
        public static LocalisableString HitErrorStyleMilliseconds => new TranslatableString(getKey(@"hit_error_style_milliseconds"), @"Milliseconds (+12ms)");

        /// <summary>
        /// "Numeric (+12)"
        /// </summary>
        public static LocalisableString HitErrorStyleNumeric => new TranslatableString(getKey(@"hit_error_style_numeric"), @"Numeric (+12)");

        /// <summary>
        /// "Beat fractions (+1/64)"
        /// </summary>
        public static LocalisableString HitErrorStyleBeatFraction => new TranslatableString(getKey(@"hit_error_style_beat_fraction"), @"Beat fractions (+1/64)");

        /// <summary>
        /// "Text labels (EARLY/LATE)"
        /// </summary>
        public static LocalisableString HitErrorStyleTextLabel => new TranslatableString(getKey(@"hit_error_style_text_label"), @"Text labels (EARLY/LATE)");

        /// <summary>
        /// "Standard (blue/green/yellow)"
        /// </summary>
        public static LocalisableString HitErrorColorStandard => new TranslatableString(getKey(@"hit_error_color_standard"), @"Standard (blue/green/yellow)");

        /// <summary>
        /// "Perfect to gray"
        /// </summary>
        public static LocalisableString HitErrorColorPerfectGray => new TranslatableString(getKey(@"hit_error_color_perfect_gray"), @"Perfect to gray");

        /// <summary>
        /// "Fast/slow (red/blue)"
        /// </summary>
        public static LocalisableString HitErrorColorFastSlow => new TranslatableString(getKey(@"hit_error_color_fast_slow"), @"Fast/slow (red/blue)");

        private static string getKey(string key) => $@"{prefix}:{key}";
    }
}
