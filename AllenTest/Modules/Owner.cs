// Aggregation example: Owner
class Owner
{
    public string OwnerName { get; set; }
    public List<Animal> Pets { get; set; } // Aggregation: Owner har en samling af dyr

    public Owner(string ownerName)
    {
        OwnerName = ownerName;
        Pets = new List<Animal>();
    }

    public void AddPet(Animal pet)
    {
        Pets.Add(pet);
    }

    public void ShowPets()
    {
        Console.WriteLine($"{OwnerName}'s pets:");
        foreach (var pet in Pets)
        {
            Console.WriteLine($"- {pet.Name}");
        }
    }
}