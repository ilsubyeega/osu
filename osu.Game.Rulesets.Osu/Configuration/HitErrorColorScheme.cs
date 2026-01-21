// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.ComponentModel;
using osu.Framework.Localisation;
using osu.Game.Localisation;

namespace osu.Game.Rulesets.Osu.Configuration
{
    public enum HitErrorColorScheme
    {
        [LocalisableDescription(typeof(RulesetSettingsStrings), nameof(RulesetSettingsStrings.HitErrorColorStandard))]
        [Description("Standard (blue/green/yellow)")]
        Standard,

        [LocalisableDescription(typeof(RulesetSettingsStrings), nameof(RulesetSettingsStrings.HitErrorColorPerfectGray))]
        [Description("Perfect to gray")]
        PerfectGray,

        [LocalisableDescription(typeof(RulesetSettingsStrings), nameof(RulesetSettingsStrings.HitErrorColorFastSlow))]
        [Description("Fast/slow (red/blue)")]
        FastSlow,
    }
}
