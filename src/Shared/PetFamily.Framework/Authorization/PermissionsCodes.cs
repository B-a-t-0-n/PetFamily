namespace PetFamily.Framework.Authorization;

public static class PermissionsCodes
{
    public static class Volunteers
    {
        public const string Create = "volunteer.create";
        public const string Update = "volunteer.update";
        public const string Delete = "volunteer.delete";
        public const string PetCreate = "volunteer.pet.create";
        public const string PetUpdate = "volunteer.pet.update";
        public const string PetDelete = "volunteer.pet.delete";
    }
    public static class Species
    {
        public const string Create = "species.create";
        public const string Delete = "species.delete";
        public const string BreedCreate = "species.breed.create";
        public const string BreedDelete = "species.breed.delete";
    }
}
