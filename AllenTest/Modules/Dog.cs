// Derived class 1 (Nedarvning)
class Dog : Animal
{
    public Dog(string name) : base(name) { }

    public override void MakeSound()
    {
        Console.WriteLine($"{Name} barks: Woof Woof!");
    }
}