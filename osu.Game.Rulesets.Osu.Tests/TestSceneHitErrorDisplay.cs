// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Testing;
using osu.Game.Rulesets.Osu.Configuration;
using osu.Game.Rulesets.Osu.UI;
using osu.Game.Rulesets.Scoring;
using osu.Game.Tests.Visual;
using osuTK;

namespace osu.Game.Rulesets.Osu.Tests
{
    public partial class TestSceneHitErrorDisplay : OsuTestScene
    {
        private HitErrorDisplay hitErrorDisplay = null!;
        private OsuRulesetConfigManager config = null!;

        [BackgroundDependencyLoader]
        private void load()
        {
            config = new OsuRulesetConfigManager(null, new OsuRuleset().RulesetInfo);
        }

        [SetUpSteps]
        public void SetUpSteps()
        {
            AddStep("create display", () =>
            {
                Child = new DependencyProvidingContainer
                {
                    RelativeSizeAxes = Axes.Both,
                    CachedDependencies = new (System.Type, object)[]
                    {
                        (typeof(OsuRulesetConfigManager), config),
                    },
                    Child = new Container
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        Size = new Vector2(400),
                        Child = hitErrorDisplay = new HitErrorDisplay
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                        },
                    }
                };
            });
        }

        [Test]
        public void TestMillisecondsStylePositive()
        {
            AddStep("set milliseconds style", () => config.SetValue(OsuRulesetSetting.HitErrorDisplayStyle, HitErrorDisplayStyle.Milliseconds));
            AddStep("set +15ms offset", () =>
            {
                hitErrorDisplay.TimeOffset = 15;
                hitErrorDisplay.Result = HitResult.Ok;
                hitErrorDisplay.BeatLength = 500;
            });
            AddStep("trigger display", () => triggerPrepareForUse());
            AddUntilStep("display visible", () => hitErrorDisplay.Alpha > 0);
        }

        [Test]
        public void TestMillisecondsStyleNegative()
        {
            AddStep("set milliseconds style", () => config.SetValue(OsuRulesetSetting.HitErrorDisplayStyle, HitErrorDisplayStyle.Milliseconds));
            AddStep("set -25ms offset", () =>
            {
                hitErrorDisplay.TimeOffset = -25;
                hitErrorDisplay.Result = HitResult.Great;
                hitErrorDisplay.BeatLength = 500;
            });
            AddStep("trigger display", () => triggerPrepareForUse());
            AddUntilStep("display visible", () => hitErrorDisplay.Alpha > 0);
        }

        [Test]
        public void TestNumericStyle()
        {
            AddStep("set numeric style", () => config.SetValue(OsuRulesetSetting.HitErrorDisplayStyle, HitErrorDisplayStyle.Numeric));
            AddStep("set +12 offset", () =>
            {
                hitErrorDisplay.TimeOffset = 12;
                hitErrorDisplay.Result = HitResult.Good;
                hitErrorDisplay.BeatLength = 500;
            });
            AddStep("trigger display", () => triggerPrepareForUse());
            AddUntilStep("display visible", () => hitErrorDisplay.Alpha > 0);
        }

        [Test]
        public void TestBeatFractionStyle()
        {
            AddStep("set beat fraction style", () => config.SetValue(OsuRulesetSetting.HitErrorDisplayStyle, HitErrorDisplayStyle.BeatFraction));
            AddStep("set offset to 1/32 of beat", () =>
            {
                hitErrorDisplay.TimeOffset = 500.0 / 32.0; // ~15.6ms at 120 BPM
                hitErrorDisplay.Result = HitResult.Great;
                hitErrorDisplay.BeatLength = 500;
            });
            AddStep("trigger display", () => triggerPrepareForUse());
            AddUntilStep("display visible", () => hitErrorDisplay.Alpha > 0);
        }

        [Test]
        public void TestTextLabelStyleEarly()
        {
            AddStep("set text label style", () => config.SetValue(OsuRulesetSetting.HitErrorDisplayStyle, HitErrorDisplayStyle.TextLabel));
            AddStep("set negative offset (early)", () =>
            {
                hitErrorDisplay.TimeOffset = -30;
                hitErrorDisplay.Result = HitResult.Ok;
                hitErrorDisplay.BeatLength = 500;
            });
            AddStep("trigger display", () => triggerPrepareForUse());
            AddUntilStep("display visible", () => hitErrorDisplay.Alpha > 0);
        }

        [Test]
        public void TestTextLabelStyleLate()
        {
            AddStep("set text label style", () => config.SetValue(OsuRulesetSetting.HitErrorDisplayStyle, HitErrorDisplayStyle.TextLabel));
            AddStep("set positive offset (late)", () =>
            {
                hitErrorDisplay.TimeOffset = 30;
                hitErrorDisplay.Result = HitResult.Meh;
                hitErrorDisplay.BeatLength = 500;
            });
            AddStep("trigger display", () => triggerPrepareForUse());
            AddUntilStep("display visible", () => hitErrorDisplay.Alpha > 0);
        }

        [Test]
        public void TestTextLabelStylePerfect()
        {
            AddStep("set text label style", () => config.SetValue(OsuRulesetSetting.HitErrorDisplayStyle, HitErrorDisplayStyle.TextLabel));
            AddStep("set perfect offset", () =>
            {
                hitErrorDisplay.TimeOffset = 0;
                hitErrorDisplay.Result = HitResult.Perfect;
                hitErrorDisplay.BeatLength = 500;
            });
            AddStep("trigger display", () => triggerPrepareForUse());
            AddUntilStep("display visible", () => hitErrorDisplay.Alpha > 0);
        }

        [Test]
        public void TestStandardColorScheme()
        {
            AddStep("set standard color scheme", () => config.SetValue(OsuRulesetSetting.HitErrorColorScheme, HitErrorColorScheme.Standard));
            testAllResults();
        }

        [Test]
        public void TestPerfectGrayColorScheme()
        {
            AddStep("set perfect gray color scheme", () => config.SetValue(OsuRulesetSetting.HitErrorColorScheme, HitErrorColorScheme.PerfectGray));
            testAllResults();
        }

        [Test]
        public void TestFastSlowColorScheme()
        {
            AddStep("set fast/slow color scheme", () => config.SetValue(OsuRulesetSetting.HitErrorColorScheme, HitErrorColorScheme.FastSlow));
            testAllResults();
        }

        [Test]
        public void TestHidePerfectHits()
        {
            AddStep("hide perfect hits", () => config.SetValue(OsuRulesetSetting.HitErrorShowPerfect, false));
            AddStep("set perfect offset", () =>
            {
                hitErrorDisplay.TimeOffset = 0;
                hitErrorDisplay.Result = HitResult.Perfect;
                hitErrorDisplay.BeatLength = 500;
            });
            AddStep("trigger display", () => triggerPrepareForUse());
            AddWaitStep("wait", 2);
            AddAssert("display expired quickly", () => !hitErrorDisplay.IsAlive || hitErrorDisplay.Alpha == 0);
        }

        [Test]
        public void TestScaleWithCS()
        {
            AddStep("enable scale with CS", () => config.SetValue(OsuRulesetSetting.HitErrorScaleWithCS, true));
            AddStep("set small scale", () =>
            {
                hitErrorDisplay.TimeOffset = 10;
                hitErrorDisplay.Result = HitResult.Great;
                hitErrorDisplay.ObjectScale = 0.5f;
                hitErrorDisplay.BeatLength = 500;
            });
            AddStep("trigger display", () => triggerPrepareForUse());
            AddUntilStep("display visible", () => hitErrorDisplay.Alpha > 0);
        }

        [Test]
        public void TestInstantShow()
        {
            AddStep("enable instant show", () => config.SetValue(OsuRulesetSetting.HitErrorInstantShow, true));
            AddStep("set offset", () =>
            {
                hitErrorDisplay.TimeOffset = 10;
                hitErrorDisplay.Result = HitResult.Great;
                hitErrorDisplay.BeatLength = 500;
            });
            AddStep("trigger display", () => triggerPrepareForUse());
            AddAssert("display immediately visible", () => hitErrorDisplay.Alpha > 0);
        }

        [Test]
        public void TestDisappearDelay()
        {
            AddStep("set short delay", () => config.SetValue(OsuRulesetSetting.HitErrorDisappearDelay, 100.0));
            AddStep("set offset", () =>
            {
                hitErrorDisplay.TimeOffset = 10;
                hitErrorDisplay.Result = HitResult.Great;
                hitErrorDisplay.BeatLength = 500;
            });
            AddStep("trigger display", () => triggerPrepareForUse());
            AddUntilStep("display fades out", () => hitErrorDisplay.Alpha < 1 || !hitErrorDisplay.IsAlive);
        }

        private void testAllResults()
        {
            AddStep("show Great", () =>
            {
                hitErrorDisplay.TimeOffset = -10;
                hitErrorDisplay.Result = HitResult.Great;
                hitErrorDisplay.BeatLength = 500;
            });
            AddStep("trigger display", () => triggerPrepareForUse());
            AddWaitStep("wait", 3);

            AddStep("show Good", () =>
            {
                hitErrorDisplay.TimeOffset = 20;
                hitErrorDisplay.Result = HitResult.Good;
                hitErrorDisplay.BeatLength = 500;
            });
            AddStep("trigger display", () => triggerPrepareForUse());
            AddWaitStep("wait", 3);

            AddStep("show Ok", () =>
            {
                hitErrorDisplay.TimeOffset = -30;
                hitErrorDisplay.Result = HitResult.Ok;
                hitErrorDisplay.BeatLength = 500;
            });
            AddStep("trigger display", () => triggerPrepareForUse());
            AddWaitStep("wait", 3);

            AddStep("show Meh", () =>
            {
                hitErrorDisplay.TimeOffset = 40;
                hitErrorDisplay.Result = HitResult.Meh;
                hitErrorDisplay.BeatLength = 500;
            });
            AddStep("trigger display", () => triggerPrepareForUse());
            AddWaitStep("wait", 3);
        }

        private void triggerPrepareForUse()
        {
            // Use reflection to call the protected method
            var method = typeof(HitErrorDisplay).GetMethod("PrepareForUse",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            method?.Invoke(hitErrorDisplay, null);
        }
    }
}
