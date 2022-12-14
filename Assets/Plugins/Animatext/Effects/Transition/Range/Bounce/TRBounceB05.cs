// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0090

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Transition - Range/Bounce/Bounce - B05", fileName = "New TRBounceB05 Preset", order = 369)]
    public sealed class TRBounceB05 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public float rotation = 45;
        public Vector2 scale = Vector2.zero;
        public Vector2 skew = new Vector2(0, 45);
        public AnchorType anchorType = AnchorType.Center;
        public Vector2 anchorOffset = Vector2.zero;
        public int bounces = 2;
        public float bounciness = 0.5f;
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

                progress = 1 - EasingUtility.Ease(progress, easingType);
                progress = EasingUtility.EaseBounce(progress, bounces, bounciness);

                Vector2 anchorPoint = range.GetAnchorPoint(anchorType) + anchorOffset;

                range.Rotate(rotation * progress, anchorPoint);
                range.Scale(Vector2.LerpUnclamped(Vector2.one, scale, progress), anchorPoint);
                range.Skew(skew * progress, anchorPoint);
            }
        }
    }
}