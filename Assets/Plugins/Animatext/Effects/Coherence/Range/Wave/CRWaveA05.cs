// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0090

using UnityEngine;

namespace Animatext.Effects
{
    // [CreateAssetMenu(menuName = "Animatext Preset/Coherence - Range/Wave/Wave - A05", fileName = "New CRWaveA05 Preset", order = 369)]
    public sealed class CRWaveA05 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public float startRotation = 0;
        public float rotation = 180;
        public AnchorType anchorType = AnchorType.Center;
        public Vector2 anchorOffset = new Vector2(0, 50);
        public int waves = 1;
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
            progress = EasingUtility.Wave(progress, waves);

            range.Move(PositionUtility.Rotate(range.anchor.center, Mathf.LerpUnclamped(startRotation, rotation, progress), range.GetAnchorPoint(anchorType) + anchorOffset) - range.anchor.center);
        }
    }
}