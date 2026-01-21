// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.ComponentModel;
using osu.Framework.Localisation;
using osu.Game.Localisation;

namespace osu.Game.Rulesets.Osu.Configuration
{
    public enum HitErrorDisplayStyle
    {
        [LocalisableDescription(typeof(RulesetSettingsStrings), nameof(RulesetSettingsStrings.HitErrorStyleMilliseconds))]
        [Description("Milliseconds (+12ms)")]
        Milliseconds,

        [LocalisableDescription(typeof(RulesetSettingsStrings), nameof(RulesetSettingsStrings.HitErrorStyleNumeric))]
        [Description("Numeric (+12)")]
        Numeric,

        [LocalisableDescription(typeof(RulesetSettingsStrings), nameof(RulesetSettingsStrings.HitErrorStyleBeatFraction))]
        [Description("Beat fractions (+1/64)")]
        BeatFraction,

        [LocalisableDescription(typeof(RulesetSettingsStrings), nameof(RulesetSettingsStrings.HitErrorStyleTextLabel))]
        [Description("Text labels (EARLY/LATE)")]
        TextLabel,
    }
}
