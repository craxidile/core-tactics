// Copyright (C) 2020 - 2022 Seeley Studio. All Rights Reserved.

#pragma warning disable IDE0090

using UnityEngine;

namespace Animatext.Effects
{
    [CreateAssetMenu(menuName = "Animatext Preset/Transition - Character/Basic/Basic - A05", fileName = "New TCBasicA05 Preset", order = 369)]
    public sealed class TCBasicA05 : DefaultTemplateEffect
    {
        public float singleTime = 1;
        public SortType sortType;
        public float rotation = 180;
        public AnchorType anchorType = AnchorType.Center;
        public Vector2 anchorOffset = new Vector2(0, 50);
        public EasingType easingType;
        [FadeMode] public ColorMode fadeMode = ColorMode.Multiply;
        public FloatRange fadeRange = new FloatRange(0, 0.5f);

        public override InfoFlags infoFlags
        {
            get { return InfoFlags.Character; }
        }

        protected override int unitCount
        {
            get { return characterCount; }
        }

        protected override float unitTime
        {
            get { return singleTime; }
        }

        protected override void Animate()
        {
            for (int i = 0; i < characterCount; i++)
            {
                float progress = GetCurrentProgress(SortUtility.Rank(i, characterCount, sortType));

                if (progress <= fadeRange.start)
                {
                    characters[i].Opacify(0, fadeMode);
                }
                else
                {
                    if (progress >= fadeRange.end)
                    {
                        characters[i].Opacify(1, fadeMode);
                    }
                    else
                    {
                        characters[i].Opacify(Mathf.InverseLerp(fadeRange.start, fadeRange.end, progress), fadeMode);
                    }

                    progress = 1 - EasingUtility.Ease(progress, easingType);

                    characters[i].Move(PositionUtility.Rotate(characters[i].anchor.center, rotation * progress, characters[i].GetAnchorPoint(anchorType) + anchorOffset) - characters[i].anchor.center);
                }
            }
        }
    }
}