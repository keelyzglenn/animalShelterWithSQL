using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace AnimalShelter
{
  public class Animal
  {
    private string _name;
    private string _gender;
    private string _date;
    private string _breed;
    private int _id;

    public Animal(string name, string gender, string date, string breed, int Id = 0)
    {
      _id = Id;
      _name = name;
      _gender = gender;
      _date = date;
      _breed = breed;
    }

    // public override bool Equals(System.Object otherAnimal)
    // {
    //   if (!(otherAnimal is Animal))
    //   {
    //     return false;
    //   }
    //   else
    //   {
    //     Animal newAnimal = (Animal) otherAnimal;
    //     bool descriptionEquality = (this.GetDescription() == newAnimal.GetDescription());
    //     return (descriptionEquality);
    //   }
    // }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }

    public string GetGender()
    {
      return _gender;
    }
    public void SetGender(string newGender)
    {
      _gender = newGender;
    }

    public string GetDate()
    {
      return _date;
    }
    public void SetDate(string newDate)
    {
      _date = newDate;
    }

    public string GetBreed()
    {
      return _breed;
    }
    public void SetBreed(string newBreed)
    {
      _breed = newBreed;
    }

// get method to return all animals
    public static List<Animal> GetAll()
    {
      List<Animal> allAnimals = new List<Animal>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand ("SELECT * FROM animals;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int animalId = rdr.GetInt32(0);
        string animalName = rdr.GetString(1);
        string animalGender = rdr.GetString(2);
        string animalDate = rdr.GetString(3);
        string animalBreed = rdr.GetString(4);
        Animal newAnimal = new Animal(animalName, animalGender, animalDate, animalBreed, animalId);
        allAnimals.Add(newAnimal);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allAnimals;
    }


    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM animals;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }
}
