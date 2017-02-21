using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AnimalShelter
{
  public class TypeTest : IDisposable
  {
    public TypeTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=animal_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_TypesEmptyAtFirst()
    {
      //Arrange, Act
      int result = Type.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueForSameName()
    {
      //Arrange, Act
      Type firstType = new Type("Dog");
      Type secondType = new Type("Dog");

      //Assert
      Assert.Equal(firstType, secondType);
    }

    [Fact]
    public void Test_Save_SavesTypeToDatabase()
    {
      //Arrange
      Type testType = new Type("Dog");
      testType.Save();

      //Act
      List<Type> result = Type.GetAll();
      List<Type> testList = new List<Type>{testType};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Save_AssignsIdToTypeObject()
    {
      //Arrange
      Type testType = new Type("Dog");
      testType.Save();

      //Act
      Type savedType = Type.GetAll()[0];

      int result = savedType.GetId();
      int testId = testType.GetId();

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindsTypeInDatabase()
    {
      //Arrange
      Type testType = new Type("Dog");
      testType.Save();

      //Act
      Type foundType = Type.Find(testType.GetId());

      //Assert
      Assert.Equal(testType, foundType);
    }
    [Fact]
    public void Test_GetAnimals_RetrievesAllAnimalswithType()
    {
      Type testType = new Type("Dog");
      testType.Save();

      Animal firstAnimal = new Animal ("Bob", "Male", "01.02.2017", "Corgi", testType.GetId());
      firstAnimal.Save();
      Animal secondAnimal = new Animal ("Sally", "Female", "02.03.2017", "English Bulldog", testType.GetId());
      secondAnimal.Save();

      List<Animal> testAnimalList = new List<Animal> {firstAnimal, secondAnimal};
      List<Animal> resultAnimalList = testType.GetAnimals();

      Assert.Equal(testAnimalList, resultAnimalList);
    }

    [Fact]
    public void Test_SortByType()
    {
      //Arrange
      Type testType1 = new Type("Dog");
      Type testType2 = new Type("Cat");
      Type testType3 = new Type("Bird");
      testType1.Save();
      testType2.Save();
      testType3.Save();

      //Act
      List<Type> result = Type.OrderByType();
      List<Type> testList = new List<Type>{testType3, testType2, testType1};
      Console.WriteLine(result);
      Console.WriteLine(testList);

      //Assert
      Assert.Equal(testList, result);
    }


    public void Dispose()
    {
      Animal.DeleteAll();
      Type.DeleteAll();
    }
  }
}
