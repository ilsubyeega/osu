// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Localisation;
using osu.Game.Localisation;
using osu.Game.Overlays.Settings;
using osu.Game.Rulesets.Osu.Configuration;
using osu.Game.Rulesets.UI;

namespace osu.Game.Rulesets.Osu.UI
{
    public partial class OsuSettingsSubsection : RulesetSettingsSubsection
    {
        protected override LocalisableString Header => "osu!";

        public OsuSettingsSubsection(Ruleset ruleset)
            : base(ruleset)
        {
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            var config = (OsuRulesetConfigManager)Config;

            var hitErrorEnabled = config.GetBindable<bool>(OsuRulesetSetting.HitErrorDisplayEnabled);

            var hitErrorStyleDropdown = new SettingsEnumDropdown<HitErrorDisplayStyle>
            {
                LabelText = RulesetSettingsStrings.HitErrorDisplayStyle,
                Current = config.GetBindable<HitErrorDisplayStyle>(OsuRulesetSetting.HitErrorDisplayStyle),
            };

            var hitErrorColorDropdown = new SettingsEnumDropdown<HitErrorColorScheme>
            {
                LabelText = RulesetSettingsStrings.HitErrorColorScheme,
                Current = config.GetBindable<HitErrorColorScheme>(OsuRulesetSetting.HitErrorColorScheme),
            };

            var hitErrorShowPerfect = new SettingsCheckbox
            {
                LabelText = RulesetSettingsStrings.HitErrorShowPerfect,
                Current = config.GetBindable<bool>(OsuRulesetSetting.HitErrorShowPerfect),
            };

            var hitErrorScaleWithCS = new SettingsCheckbox
            {
                LabelText = RulesetSettingsStrings.HitErrorScaleWithCS,
                Current = config.GetBindable<bool>(OsuRulesetSetting.HitErrorScaleWithCS),
            };

            var hitErrorInstantShow = new SettingsCheckbox
            {
                LabelText = RulesetSettingsStrings.HitErrorInstantShow,
                Current = config.GetBindable<bool>(OsuRulesetSetting.HitErrorInstantShow),
            };

            var hitErrorDisappearDelay = new SettingsSlider<double>
            {
                LabelText = RulesetSettingsStrings.HitErrorDisappearDelay,
                Current = config.GetBindable<double>(OsuRulesetSetting.HitErrorDisappearDelay),
                KeyboardStep = 50,
            };

            var hitErrorHideJudgements = new SettingsCheckbox
            {
                LabelText = RulesetSettingsStrings.HitErrorHideJudgements,
                Current = config.GetBindable<bool>(OsuRulesetSetting.HitErrorHideJudgements),
            };

            Children = new Drawable[]
            {
                new SettingsCheckbox
                {
                    LabelText = RulesetSettingsStrings.SnakingInSliders,
                    Current = config.GetBindable<bool>(OsuRulesetSetting.SnakingInSliders)
                },
                new SettingsCheckbox
                {
                    ClassicDefault = false,
                    LabelText = RulesetSettingsStrings.SnakingOutSliders,
                    Current = config.GetBindable<bool>(OsuRulesetSetting.SnakingOutSliders)
                },
                new SettingsCheckbox
                {
                    LabelText = RulesetSettingsStrings.CursorTrail,
                    Current = config.GetBindable<bool>(OsuRulesetSetting.ShowCursorTrail)
                },
                new SettingsCheckbox
                {
                    LabelText = RulesetSettingsStrings.CursorRipples,
                    Current = config.GetBindable<bool>(OsuRulesetSetting.ShowCursorRipples)
                },
                new SettingsEnumDropdown<PlayfieldBorderStyle>
                {
                    LabelText = RulesetSettingsStrings.PlayfieldBorderStyle,
                    Current = config.GetBindable<PlayfieldBorderStyle>(OsuRulesetSetting.PlayfieldBorderStyle),
                },
                new SettingsCheckbox
                {
                    LabelText = RulesetSettingsStrings.HitErrorDisplayEnabled,
                    Current = hitErrorEnabled,
                },
                hitErrorStyleDropdown,
                hitErrorColorDropdown,
                hitErrorShowPerfect,
                hitErrorScaleWithCS,
                hitErrorInstantShow,
                hitErrorDisappearDelay,
                hitErrorHideJudgements,
            };

            // Update visibility of hit error settings based on enabled state
            hitErrorEnabled.BindValueChanged(e =>
            {
                bool enabled = e.NewValue;
                hitErrorStyleDropdown.Alpha = enabled ? 1 : 0.5f;
                hitErrorColorDropdown.Alpha = enabled ? 1 : 0.5f;
                hitErrorShowPerfect.Alpha = enabled ? 1 : 0.5f;
                hitErrorScaleWithCS.Alpha = enabled ? 1 : 0.5f;
                hitErrorInstantShow.Alpha = enabled ? 1 : 0.5f;
                hitErrorDisappearDelay.Alpha = enabled ? 1 : 0.5f;
                hitErrorHideJudgements.Alpha = enabled ? 1 : 0.5f;
            }, true);
        }
    }
}
