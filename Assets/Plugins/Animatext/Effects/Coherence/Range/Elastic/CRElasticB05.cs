// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0090

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Coherence - Range/Elastic/Elastic - B05", fileName = "New CRElasticB05 Preset", order = 369)]
    public sealed class CRElasticB05 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public float startRotation = 0;
        public Vector2 startScale = Vector2.one;
        public Vector2 startSkew = Vector2.zero;
        public float rotation = 45;
        public Vector2 scale = Vector2.zero;
        public Vector2 skew = new Vector2(0, 45);
        public AnchorType anchorType = AnchorType.Center;
        public Vector2 anchorOffset = Vector2.zero;
        public int oscillations = 2;
        public float stiffness = 5;
        public EasingType easingType;

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
            progress = EasingUtility.Elastic(progress, oscillations, stiffness);

            Vector2 anchorPoint = range.GetAnchorPoint(anchorType) + anchorOffset;

            range.Rotate(Mathf.LerpUnclamped(startRotation, rotation, progress), anchorPoint);
            range.Scale(Vector2.LerpUnclamped(startScale, scale, progress), anchorPoint);
            range.Skew(Vector2.LerpUnclamped(startSkew, skew, progress), anchorPoint);
        }
    }
}