// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0090

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Transition - Range/Step/Step - A05", fileName = "New TRStepA05 Preset", order = 369)]
    public sealed class TRStepA05 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public float rotation = 180;
        public AnchorType anchorType = AnchorType.Center;
        public Vector2 anchorOffset = new Vector2(0, 50);
        public int steps = 5;
        public StepType stepType = StepType.Round;
        public EasingType easingType;
        [FadeMode] public ColorMode fadeMode = ColorMode.Multiply;
        public FloatRange fadeRange = new FloatRange(0, 0.25f);

        public override InfoFlags infoFlags
        {
            get { return InfoFlags.Range; }
        }

        protected override int unitCount
        {
            get { return 1; }
        }

        protected override float unitTime
        {
            get { return singleTime; }
        }

        protected override void Animate()
        {
            float progress = GetCurrentProgress(0);

            progress = EasingUtility.Ease(progress, easingType);
            progress = EasingUtility.EaseStep(progress, steps, stepType);

            if (progress <= fadeRange.start)
            {
                range.Opacify(0, fadeMode);
            }
            else
            {
                if (progress >= fadeRange.end)
                {
                    range.Opacify(1, fadeMode);
                }
                else
                {
                    range.Opacify(Mathf.InverseLerp(fadeRange.start, fadeRange.end, progress), fadeMode);
                }

                progress = 1 - progress;

                range.Move(PositionUtility.Rotate(range.anchor.center, rotation * progress, range.GetAnchorPoint(anchorType) + anchorOffset) - range.anchor.center);
            }
        }
    }
}