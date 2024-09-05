﻿using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;
using ValueObject = PetFamily.Domain.Shared.ValueObject;

namespace PetFamily.Domain.PetMenegment.ValueObjects
{
    public class NumberPets : ValueObject
    {
        private NumberPets() { }
        private NumberPets(int foundAHouse, int lookingForHouse, int beingTreated)
        {
            FoundAHouse = foundAHouse;
            LookingForHouse = lookingForHouse;
            BeingTreated = beingTreated;
        }

        public int FoundAHouse { get; }
        public int LookingForHouse { get; }
        public int BeingTreated { get; }

        public static Result<NumberPets, Error> Create(int foundAHouse, int lookingForHouse, int beingTreated)
        {
            if (foundAHouse < 0)
                return Errors.General.ValueIsRequired("foundAHouse");

            if (lookingForHouse < 0)
                return Errors.General.ValueIsRequired("lookingForHouse");

            if (beingTreated < 0)
                return Errors.General.ValueIsRequired("beingTreated");

            var numberPets = new NumberPets(foundAHouse, lookingForHouse, beingTreated);

            return numberPets;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FoundAHouse;
            yield return LookingForHouse;
            yield return BeingTreated;
        }
    }
}
