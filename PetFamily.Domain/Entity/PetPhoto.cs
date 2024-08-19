﻿using CSharpFunctionalExtensions;
using PetFamily.Domain.ValueObjects;
using System.IO;

namespace PetFamily.Domain.Entity
{
    public class PetPhoto : Shared.Entity<PetPhotoId>
    {
        //ef core
        private PetPhoto(PetPhotoId id) : base(id) { }

        private PetPhoto(PetPhotoId id, string path, bool isMain) : base(id)
        {
            Path = path;
            IsMain = isMain;
        }

        public string Path { get; private set; } = default!;
        public bool IsMain { get; private set; }

        public static Result<PetPhoto> Create(PetPhotoId id, string path, bool isMain)
        {
            if (string.IsNullOrWhiteSpace(path))
                Result.Failure<PetPhoto>("path is null or white space");

            var pet = new PetPhoto(id, path, isMain);

            return Result.Success(pet);
        }
    }
}
