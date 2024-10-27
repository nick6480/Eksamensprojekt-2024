// See https://aka.ms/new-console-template for more information
class Program
{
    static void Main(string[] args)
    {
        // Create animals
        Dog dog = new Dog("Rex");
        Bird bird = new Bird("Tweety");

        // Use composition: The heart is part of the animal
        dog.Heart.Beat();
        bird.Heart.Beat();

        // Use polymorphism (Nedarvning)
        dog.MakeSound();
        bird.MakeSound();

        // Aggregation: Owner has pets (but pets can exist without owner)
        Owner owner = new Owner("Alice");
        owner.AddPet(dog);
        owner.AddPet(bird);
        owner.ShowPets();
    }
}