using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AnimalShelter
{
  public class AnimalShelterTest : IDisposable
  {
    public AnimalShelterTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=animal_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      //Arrange, Act
      int result = Animal.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueIfDescriptionsAreTheSame()
    {
      //Arrange, Act
      Animal firstAnimal = new Animal("Bob", "Male", "01.02.2017", "Corgi", 1);
      Animal secondAnimal = new Animal("Bob", "Male", "01.02.2017", "Corgi", 1);

      //Assert
      Assert.Equal(firstAnimal, secondAnimal);
    }

    [Fact]
    public void Test_SaveToDatabase()
    {
      //Arrange
      Animal testAnimal = new Animal ("Bob", "Male", "01.02.2017", "Corgi", 1);

      //Act
      testAnimal.Save();
      List<Animal> result = Animal.GetAll();
      List<Animal> testList = new List<Animal>{testAnimal};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Save_AssignsIdToObjects()
    {
      //Arrange
      Animal testAnimal = new Animal("Bob", "Male", "01.02.2017", "Corgi", 1);

      //Act
      testAnimal.Save();
      Animal savedAnimal = Animal.GetAll()[0];

      int result = savedAnimal.GetId();
      int testId = testAnimal.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    public void Dispose()
    {
      Animal.DeleteAll();
    }

    [Fact]
    public void Test_Find_FindsAnimalInDatabase()
    {
      //Arrange
      Animal testAnimal = new Animal("Bob", "Male", "01.02.2017", "Corgi", 1);
      testAnimal.Save();

      //Act
      Animal foundAnimal = Animal.Find(testAnimal.GetId());

      //Assert
      Assert.Equal(testAnimal, foundAnimal);
    }

    [Fact]
   public void Test_SortByBreed()
   {
     //Arrange
     Animal testAnimal1 = new Animal("Bob", "Male", "01.02.2017", "Corgi", 1);
     Animal testAnimal2 = new Animal("Bob", "Male", "01.02.2017", "Westie", 1);
     Animal testAnimal3 = new Animal("Bob", "Male", "01.02.2017", "Lab", 1);
     testAnimal1.Save();
     testAnimal2.Save();
     testAnimal3.Save();

     //Act
     List<Animal> result = Animal.OrderByBreed();
     List<Animal> testList = new List<Animal>{testAnimal1, testAnimal3, testAnimal2};
     Console.WriteLine(result);
     Console.WriteLine(testList);

     //Assert
     Assert.Equal(testList, result);
   }

   [Fact]
  public void Test_SortByDate()
  {
    //Arrange
    Animal testAnimal1 = new Animal("Bob", "Male", "01.02.2017", "Corgi", 1);
    Animal testAnimal2 = new Animal("Bob", "Male", "01.01.2017", "Westie", 1);
    Animal testAnimal3 = new Animal("Bob", "Male", "01.03.2017", "Lab", 1);
    testAnimal1.Save();
    testAnimal2.Save();
    testAnimal3.Save();

    //Act
    List<Animal> result = Animal.OrderByDate();
    List<Animal> testList = new List<Animal>{testAnimal2, testAnimal1, testAnimal3};
    Console.WriteLine(result);
    Console.WriteLine(testList);

    //Assert
    Assert.Equal(testList, result);
  }
  }
}
