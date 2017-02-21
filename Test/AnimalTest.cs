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
      Animal firstAnimal = new Animal("Bob", "Male", "01.02.2017", "Corgi");
      Animal secondAnimal = new Animal("Bob", "Male", "01.02.2017", "Corgi");

      //Assert
      Assert.Equal(firstAnimal, secondAnimal);
    }

    public void Dispose()
    {
      Animal.DeleteAll();
    }
  }
}
