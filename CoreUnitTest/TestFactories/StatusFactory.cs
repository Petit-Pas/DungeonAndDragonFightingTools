using DnDToolsLibrary.Attacks.Damage;
using DnDToolsLibrary.Attacks.Damage.Type;
using DnDToolsLibrary.Characteristics;
using DnDToolsLibrary.Status;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreUnitTest.TestFactories
{
    public static class StatusFactory
    {
        public static OnHitStatus Slow => new OnHitStatus()
        {
            ApplySavingCharacteristic = CharacteristicsEnum.Wisdom,
            Affected = null,
            ApplySavingDifficulty = 0,
            CanRedoSavingThrow = true,
            DurationIsBasedOnStartOfTurn = true,
            DurationIsCalculatedOnCasterTurn = true,
            DisplayName = "Slowed",
            DotDamageList = new DotTemplateList(),
            EndsOnCasterLossOfConcentration = true,
            SavingIsRemadeAtStartOfTurn = true,
            Caster = null,
            Description = "Description",
            HasAMaximumDuration = true,
            HasApplyCondition = false,
            HasSpellSaving = true,
            Header = "Header",
            Name = "Name",
            OnApplyDamageList = new DamageTemplateList(),
            RemainingRounds = 10,
            SpellApplicationModifier = ApplicationModifierEnum.Canceled,
            SpellSavingWasSuccessful = false,
            ToolTip = "Tooltip",
        };

        public static OnHitStatus ImmediateDamageNormal => new OnHitStatus()
        {
            ApplySavingCharacteristic = CharacteristicsEnum.Wisdom,
            CanRedoSavingThrow = true,
            DurationIsBasedOnStartOfTurn = true,
            DurationIsCalculatedOnCasterTurn = true,
            DisplayName = "ImmediateDamageNormal",
            DotDamageList = new DotTemplateList(),
            EndsOnCasterLossOfConcentration = true,
            SavingIsRemadeAtStartOfTurn = true,
            Affected = null,
            ApplySavingDifficulty = 0,
            Caster = null,
            Description = "Description",
            HasAMaximumDuration = true,
            HasApplyCondition = false,
            HasSpellSaving = true,
            Header = "Header",
            Name = "Name",
            OnApplyDamageList = new DamageTemplateList() { 
                new DamageTemplate("1d1+9", DamageTypeEnum.Fire),
            },
            RemainingRounds = 10,
            SpellApplicationModifier = ApplicationModifierEnum.Canceled,
            SpellSavingWasSuccessful = false,
            ToolTip = "Tooltip",
        };

        public static OnHitStatus ImmediateDamageHalved => new OnHitStatus()
        {
            ApplySavingCharacteristic = CharacteristicsEnum.Wisdom,
            CanRedoSavingThrow = true,
            DurationIsBasedOnStartOfTurn = true,
            DurationIsCalculatedOnCasterTurn = true,
            DisplayName = "ImmediateDamageHalved",
            DotDamageList = new DotTemplateList(),
            EndsOnCasterLossOfConcentration = true,
            SavingIsRemadeAtStartOfTurn = true,
            Affected = null,
            ApplySavingDifficulty = 0,
            Caster = null,
            Description = "Description",
            HasAMaximumDuration = true,
            HasApplyCondition = false,
            HasSpellSaving = true,
            Header = "Header",
            Name = "Name",
            OnApplyDamageList = new DamageTemplateList() {
                new DamageTemplate("1d1+9", DamageTypeEnum.Fire, DamageModifierEnum.Halved),
            },
            RemainingRounds = 10,
            SpellApplicationModifier = ApplicationModifierEnum.Canceled,
            SpellSavingWasSuccessful = false,
            ToolTip = "Tooltip",
        };

        public static OnHitStatus ImmediateDamageCanceled => new OnHitStatus()
        {
            ApplySavingCharacteristic = CharacteristicsEnum.Wisdom,
            CanRedoSavingThrow = true,
            DurationIsBasedOnStartOfTurn = true,
            DurationIsCalculatedOnCasterTurn = true,
            DisplayName = "ImmediateDamageCanceled",
            DotDamageList = new DotTemplateList(),
            EndsOnCasterLossOfConcentration = true,
            SavingIsRemadeAtStartOfTurn = true,
            Affected = null,
            ApplySavingDifficulty = 0,
            Caster = null,
            Description = "Description",
            HasAMaximumDuration = true,
            HasApplyCondition = false,
            HasSpellSaving = true,
            Header = "Header",
            Name = "Name",
            OnApplyDamageList = new DamageTemplateList() {
                new DamageTemplate("1d1+9", DamageTypeEnum.Fire, DamageModifierEnum.Canceled),
            },
            RemainingRounds = 10,
            SpellApplicationModifier = ApplicationModifierEnum.Canceled,
            SpellSavingWasSuccessful = false,
            ToolTip = "Tooltip",
        };

        public static OnHitStatus InfernalWound => new OnHitStatus()
        {
            ApplySavingCharacteristic = CharacteristicsEnum.Constitution,
            CanRedoSavingThrow = false,
            DurationIsBasedOnStartOfTurn = false,
            DurationIsCalculatedOnCasterTurn = true,
            DisplayName = "InfernalWound",
            DotDamageList = new DotTemplateList() {
                new DotTemplate("12", DamageTypeEnum.Force),
            },
            EndsOnCasterLossOfConcentration = false,
            SavingIsRemadeAtStartOfTurn = false,
            Affected = null,
            ApplySavingDifficulty = 17,
            Caster = null,
            Description = "Description",
            HasAMaximumDuration = false,
            HasApplyCondition = true,
            HasSpellSaving = false,
            Header = "Header",
            Name = "Name",
            OnApplyDamageList = new DamageTemplateList()
            {
            },
            RemainingRounds = 0,
            SpellApplicationModifier = ApplicationModifierEnum.Canceled,
            SpellSavingWasSuccessful = false,
            ToolTip = "Tooltip",
        };

        public static OnHitStatus AutomaticApplication => new OnHitStatus()
        {
            ApplySavingCharacteristic = CharacteristicsEnum.Wisdom,
            CanRedoSavingThrow = false,
            DurationIsBasedOnStartOfTurn = true,
            DurationIsCalculatedOnCasterTurn = false,
            DisplayName = "AutomaticApplication",
            DotDamageList = new DotTemplateList(),
            EndsOnCasterLossOfConcentration = false,
            SavingIsRemadeAtStartOfTurn = false,
            Affected = null,
            ApplySavingDifficulty = 0,
            Caster = null,
            Description = "Description",
            HasAMaximumDuration = false,
            HasApplyCondition = false,
            HasSpellSaving = false,
            Header = "Header",
            Name = "Name",
            OnApplyDamageList = new DamageTemplateList()
            {
            },
            RemainingRounds = 10,
            SpellApplicationModifier = ApplicationModifierEnum.Canceled,
            SpellSavingWasSuccessful = false,
            ToolTip = "Tooltip",
        };
    }
}
